using System;
using System.Collections;
using Cinemachine;
using EasyUI.Toast;
using Members.Carlos.Scripts.Dialogues;
using Members.Carlos.Scripts.Tasks;
using Members.Carlos.Scripts.Vehicles.Plane;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EasyUI.Toast.Toast;

namespace Members.Carlos.Scripts
{
    public class GameManager : MonoBehaviour
    {
        //Variables
        public static GameManager instance;
        
        [SerializeField] private PlaneController planeController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private DialogueManager dialogueManager;
        public TaskManager taskManager;

        [Header("--- PLANE ---")]
        [SerializeField] private Transform playerSpawnPlane;
        [SerializeField] private GameObject planeVehicle;
        [SerializeField] private GameObject planeVirtualCam;
        [SerializeField] private GameObject planeFreeLookCam;
        [SerializeField] private bool canResetPlaneCam;

        [Header("--- PLAYER ---")]
        [Space(10)]
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private bool canNotMove;

        [Header("--- CAR ---")] 
        [Space(10)] 
        [SerializeField] private GameObject carVehicle;
        [SerializeField] private GameObject carSpawner;
        [SerializeField] private GameObject carVirtualCam;
        [SerializeField] private GameObject carFreeLookCam;
        [SerializeField] private bool canResetCarCam;

        [Header("--- DIALOGUE MODE ---")] 
        [Space(10)]
        public bool isDialogue;
        [SerializeField] private GameObject dialogueCanvas;

        [Header("--- CONTROLS MENU ---")] 
        [Space(10)] 
        public bool controlsMenuOpen;
        public bool snakeOpen;
        public bool canOpenControls;

        [SerializeField] private TextMeshProUGUI titleControlsMenu;
        
        [SerializeField] private GameObject controlsMenu;
        [SerializeField] private GameObject characterMenu;
        [SerializeField] private GameObject carMenu;
        [SerializeField] private GameObject planeMenu;
        
        [SerializeField] private Animator characterMenu_Animator;
        [SerializeField] private Animator carMenu_Animator;
        [SerializeField] private Animator planeMenu_Animator;
        [SerializeField] private Animator exitMenu_Animator;

        [Header("--- OTHER ---")]
        [Space(10)]
        public GameObject spawnPlaneVFX;
        public GameObject teachersDoor;
        public GameObject studentsDoor;
        [SerializeField] private GameObject characters;
    
        public bool changeToPlane;
        public bool changeToCar ;

        public float mouseX, mouseY;
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int DialogueIsOn = Animator.StringToHash("DialogueIsOn");

        public bool CanNotMove
        {
            get => canNotMove;
            set => canNotMove = value;
        }

        public GameObject Player
        {
            get => player;
            set => player = value;
        }

        public GameObject Characters
        {
            get => characters;
            set => characters = value;
        }

        private void Awake()
        {
            instance = this;
            characters = GameObject.Find("PlayerCharacters");
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Stop();
            
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ElegirCoche();
                ElegirTexturaCoche();
                ElegirAvion();
                ElegirTexturaAvion();
            }
        }

        private void Update()
        {
            if (canNotMove)
            {
                playerController.enabled = false;
                playerController.horizontal = 0;
                playerController.vertical = 0;
                playerController.playerAnimator.SetFloat(X, 0);
                playerController.playerAnimator.SetFloat(Y, 0);
                playerController.Speed = 1;
                playerController.playerAnimator.SetFloat("WalkToSprint", 0.3f);
                playerController.playerAnimator.SetBool("IsSprinting", false);
            }
            
            ButtonsAnimations();
            
            //////////////////////////////////////////////////////
            
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ChangeCharacter();
            }

            if (Input.GetKeyDown(KeyCode.Q) && !isDialogue && canOpenControls)
            {
                PlayerControlsMenu();
            }

            spawnPlaneVFX.transform.position = player.transform.position;

            if (isDialogue)
            {
                playerController.enabled = false;
                playerController.horizontal = 0;
                playerController.vertical = 0;
                playerController.playerAnimator.SetFloat(X, 0);
                playerController.playerAnimator.SetFloat(Y, 0);
                playerController.Speed = 1;
                playerController.playerAnimator.SetFloat("WalkToSprint", 0.3f);
                playerController.playerAnimator.SetBool("IsSprinting", false);
            }
            else if (!controlsMenuOpen)
            {
                playerController.enabled = true; 
            }

            if (taskManager != null)
            {
                if (taskManager.exit2Checked)
                {
                    teachersDoor.transform.localEulerAngles = new Vector3(0, 75, 0);
                    studentsDoor.transform.localEulerAngles = new Vector3(0, -150, 0);
                }
            }
            
        }
        
        private void ChangeCharacter()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                var spawnDetecter = FindObjectOfType<SpawnDetecter>();
                
                if (spawnDetecter != null)
                {
                    if (spawnDetecter.CanTransform)
                    {
                        Transform();
                    }
                    else
                    {
                        Toast.Show("No hay espacio para transformarse");
                    }
                }
                else
                {
                    Transform();
                }
            }

            if (changeToPlane)
            {
                PlaneActive();
                PlaneCameraMovement();
            }
            else if (changeToCar)
            {
                CarActive();
                CarCameraMovement();
            }
            else
            {
                PlayerActive();
                FindObjectOfType<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            }
        }
        
        private void Transform()
        {
            if(!playerController.isGrounded)
            {
                FindObjectOfType<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
                changeToPlane = !changeToPlane;
                spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
                canResetPlaneCam = true;
            }
            else if (playerController.isGrounded)
            {
                FindObjectOfType<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
                changeToCar = !changeToCar;
                spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
                canResetCarCam = true;
            }
               
            StopAllCoroutines(); 
        }

        #region - CONTROLS MENU -

        private void PlayerControlsMenu()
        {
            controlsMenuOpen = !controlsMenuOpen;
            
            if (controlsMenuOpen)
            {
                OpenControlsMenu();
                if (isDialogue)
                {
                    dialogueCanvas.SetActive(false);   
                }
            }
            else
            {
                CloseControlsMenu();
                if (isDialogue)
                {
                    dialogueCanvas.SetActive(true);
                    dialogueManager.dialogueAnimator.SetBool(DialogueIsOn, true);
                }
            }
        }

        #region - MENU -
        
        private void OpenControlsMenu()
        {
            controlsMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
                
            playerController.enabled = false;
            playerController.horizontal = 0;
            playerController.vertical = 0;
            playerController.playerAnimator.SetFloat(X, 0);
            playerController.playerAnimator.SetFloat(Y, 0);
            playerController.Speed = 1;
            playerController.playerAnimator.SetFloat("WalkToSprint", 0.3f);
            playerController.playerAnimator.SetBool("IsSprinting", false);
            
            playerCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = string.Empty;
            playerCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = string.Empty;
            playerCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0;
            playerCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0;
        }
        
        public void CloseControlsMenu()
        {
            controlsMenuOpen = false;
            controlsMenu.SetActive(false);
            ShowCharacterControls();
                
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
                
            playerController.enabled = true;

            playerCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
            playerCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
        }

        private void ButtonsAnimations()
        {
            if (characterMenu.activeSelf)
            {
                titleControlsMenu.text = characterMenu.name + " CONTROLS";
                
                characterMenu_Animator.SetTrigger("Selected");
                carMenu_Animator.SetTrigger("Normal");
                planeMenu_Animator.SetTrigger("Normal");
                exitMenu_Animator.SetTrigger("Normal");
            }
            else if (carMenu.activeSelf)
            {
                titleControlsMenu.text = carMenu.name + " CONTROLS";
                
                characterMenu_Animator.SetTrigger("Normal");
                carMenu_Animator.SetTrigger("Selected");
                planeMenu_Animator.SetTrigger("Normal");
            }
            else if (planeMenu.activeSelf)
            { 
                titleControlsMenu.text = planeMenu.name + " CONTROLS";
                
                characterMenu_Animator.SetTrigger("Normal");
                carMenu_Animator.SetTrigger("Normal");
                planeMenu_Animator.SetTrigger("Selected");
            }
        }
        
        #endregion

        #region - CHARACTER MENU -

        public void ShowCharacterControls()
        {
            characterMenu.SetActive(true);
            carMenu.SetActive(false);
            planeMenu.SetActive(false);
        }

        #endregion

        #region - CAR MENU -

        public void ShowCarControls()
        {
            characterMenu.SetActive(false);
            carMenu.SetActive(true);
            planeMenu.SetActive(false);
        }

        #endregion

        #region - PLANE MENU -

        public void ShowPlaneControls()
        {
            characterMenu.SetActive(false);
            carMenu.SetActive(false);
            planeMenu.SetActive(true);
        }

        #endregion

        #endregion

        #region - PLANE -
    
        private void PlaneActive()
        {
            player.SetActive(false);
            playerCamera.SetActive(false);
            
            planeVehicle.SetActive(true);

            if (!PlaneGroundDetecter.instance.ChangeSpawnPos)
            {
                player.transform.position = playerSpawnPlane.transform.position;
                player.transform.rotation = playerSpawnPlane.transform.rotation;
            }
            else
            {
                player.transform.position = new Vector3(playerSpawnPlane.transform.position.x, playerSpawnPlane.transform.position.y + 3f, playerSpawnPlane.transform.position.z + 2f);
                player.transform.rotation = playerSpawnPlane.transform.rotation;
            }
            
        }
    
        private void PlaneCameraMovement()
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            if (canResetPlaneCam)
            { 
                planeVirtualCam.SetActive(true);
            }
        
            if (canResetPlaneCam && (mouseX != 0 || mouseY != 0))
            {
                canResetPlaneCam = false;
                planeVirtualCam.SetActive(false);
                planeFreeLookCam.SetActive(true);
                StopAllCoroutines();
                //StopCoroutine(nameof(WaitResetPlaneCamera));
            }
            else if (!canResetPlaneCam && (mouseX == 0 && mouseY == 0))
            {
                StartCoroutine(nameof(WaitResetPlaneCamera));
            }
        }

        private IEnumerator WaitResetPlaneCamera()
        {
            yield return new WaitForSeconds(3);
            planeFreeLookCam.SetActive(false);
            planeVirtualCam.SetActive(true);
            canResetPlaneCam = true;
        }
    
        #endregion

        #region - CAR -

        private void CarActive()
        {
            player.SetActive(false);
            playerCamera.SetActive(false);
        
            carVehicle.SetActive(true);

            var carPosition = carVehicle.transform.position;
            player.transform.position = new Vector3(carPosition.x, carPosition.y + 1.5f, carPosition.z);
            player.transform.rotation = carVehicle.transform.rotation;
        }
    
        private void CarCameraMovement()
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            if (canResetCarCam)
            { 
                carVirtualCam.SetActive(true);
            }
        
            if (canResetCarCam && (mouseX != 0 || mouseY != 0))
            {
                canResetCarCam = false;
                carVirtualCam.SetActive(false);
                carFreeLookCam.SetActive(true);
                StopAllCoroutines();
                //StopCoroutine(nameof(WaitResetCarCamera));
            }
            else if (!canResetCarCam && (mouseX == 0 && mouseY == 0))
            {
                StartCoroutine(nameof(WaitResetCarCamera));
            }
        }

        private IEnumerator WaitResetCarCamera()
        {
            yield return new WaitForSeconds(3);
            carFreeLookCam.SetActive(false);
            carVirtualCam.SetActive(true);
            canResetCarCam = true;
        }

        #endregion

        #region - PLAYER -
    
        public void PlayerActive()
        {
            player.SetActive(true);
            playerCamera.SetActive(true);
            
            planeVehicle.SetActive(false);
            planeVirtualCam.SetActive(false);
            planeFreeLookCam.SetActive(false);
            PlaneGroundDetecter.instance.ChangeSpawnPos = false;
        
            carVehicle.SetActive(false);
            carVirtualCam.SetActive(false);
            carFreeLookCam.SetActive(false);

            var position = player.transform.position;
            var rotation = player.transform.rotation;

            planeVehicle.transform.position = position;
            planeVehicle.transform.rotation = rotation;

            carVehicle.transform.position = new Vector3(position.x, position.y + 1f, position.z);
            carVehicle.transform.rotation = rotation;
            
            planeVehicle.GetComponent<PlaneController>().planeRigidbody.velocity *= 0f;
        }
        #endregion

        private void ElegirCoche()
        {
            if (AudioManager.instance.ModelajeStarsAmount.Equals(1))
            {
                GameObject CarPocho = Instantiate(AudioManager.instance.cochePochoM, characters.transform);
                AudioManager.instance.currentCar = CarPocho;
            }
            else if (AudioManager.instance.ModelajeStarsAmount.Equals(2))
            {
                GameObject CarNormal = Instantiate(AudioManager.instance.cocheNormalM, characters.transform);
                AudioManager.instance.currentCar = CarNormal;
            }
            else
            {
                GameObject CarTocho = Instantiate(AudioManager.instance.cocheTochoM, characters.transform);
                AudioManager.instance.currentCar = CarTocho;
            }
            
            carVehicle = AudioManager.instance.currentCar;
            carSpawner = AudioManager.instance.currentCar.GetComponentInChildren<GroundDetecter>().gameObject;
            carVirtualCam.GetComponent<CinemachineVirtualCamera>().Follow = AudioManager.instance.currentCar.transform.GetChild(3);
            carVirtualCam.GetComponent<CinemachineVirtualCamera>().LookAt = AudioManager.instance.currentCar.transform.GetChild(4);
            carFreeLookCam.GetComponent<CinemachineFreeLook>().Follow = AudioManager.instance.currentCar.transform.GetChild(3);
            carFreeLookCam.GetComponent<CinemachineFreeLook>().LookAt = AudioManager.instance.currentCar.transform.GetChild(4);
        }

        private void ElegirAvion()
        {
            if (AudioManager.instance.ModelajeStarsAmount.Equals(1))
            {
                GameObject AvionPocho = Instantiate(AudioManager.instance.avionPochoM, characters.transform);
                AudioManager.instance.currentAvion = AvionPocho;
            }
            else if (AudioManager.instance.ModelajeStarsAmount.Equals(2))
            {
                GameObject AvionNormal = Instantiate(AudioManager.instance.avionNormalM, characters.transform);
                AudioManager.instance.currentAvion = AvionNormal;
            }
            else
            {
                GameObject AvionTocho = Instantiate(AudioManager.instance.avionTochoM, characters.transform);
                AudioManager.instance.currentAvion = AvionTocho;
            }
            
            planeVehicle = AudioManager.instance.currentAvion;
            planeController = AudioManager.instance.currentAvion.GetComponent<PlaneController>();
            planeVirtualCam.GetComponent<CinemachineVirtualCamera>().Follow = AudioManager.instance.currentAvion.transform;
            planeVirtualCam.GetComponent<CinemachineVirtualCamera>().LookAt = AudioManager.instance.currentAvion.transform.GetChild(0);
            planeFreeLookCam.GetComponent<CinemachineFreeLook>().Follow = AudioManager.instance.currentAvion.transform;
            planeFreeLookCam.GetComponent<CinemachineFreeLook>().LookAt = AudioManager.instance.currentAvion.transform.GetChild(0);
            playerSpawnPlane = AudioManager.instance.currentAvion.transform.GetChild(9);
        }

        private void ElegirTexturaAvion()
        {
            if (AudioManager.instance.ModelajeStarsAmount.Equals(1))
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionPocho[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionPocho[1];
                    }
                }
                else
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionPocho[2];
                    }
                }
            }
            else if (AudioManager.instance.ModelajeStarsAmount.Equals(2))
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionNormal[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionNormal[1];
                    }
                }
                else
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionNormal[2];
                    }
                }
            }
            else
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionTocho[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionTocho[1];
                    }
                }
                else
                {
                    foreach (var material in AudioManager.instance.currentAvion.GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material =  AudioManager.instance.TexturasAvionTocho[2];
                    }
                }
            }
        }

        private void ElegirTexturaCoche()
        {
            if (AudioManager.instance.ModelajeStarsAmount.Equals(1))
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCochePocho[0];

                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaPocha[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCochePocho[1];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaPocha[1];
                    }
                }
                else
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCochePocho[2];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaPocha[2];
                    }
                }
            }
            else if (AudioManager.instance.ModelajeStarsAmount.Equals(2))
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheNormal[0];

                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaNormal[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheNormal[1];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaNormal[1];
                    }
                }
                else
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheNormal[2];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaNormal[2];
                    }
                }
            }
            else
            {
                if (AudioManager.instance.TexturizadoStarsAmount.Equals(1))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheTocho[0];

                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaTocha[0];
                    }
                }
                else if (AudioManager.instance.TexturizadoStarsAmount.Equals(2))
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheTocho[1];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaTocha[1];
                    }
                }
                else
                {
                    AudioManager.instance.currentCar.transform.GetChild(0).GetComponent<MeshRenderer>().material = AudioManager.instance.TexturasCocheTocho[2];
                    
                    foreach (var material in AudioManager.instance.currentCar.transform.GetChild(1).GetChild(0).GetComponentsInChildren<MeshRenderer>())
                    {
                        material.material = AudioManager.instance.TexturasRuedaTocha[2];
                    }
                }
            }
        }
    }
}
