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
        [SerializeField] private GameObject planeVehicle;
        [SerializeField] private GameObject planeVirtualCam;
        [SerializeField] private GameObject planeFreeLookCam;
        [SerializeField] private bool canResetPlaneCam;

        [Header("--- PLAYER ---")]
        [Space(10)]
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private bool canNotMove;
        public int ModelajeStarsAmount = 0;
        public int TexturizadoStarsAmount = 0;
        public int ProgramacionStarsAmount = 0;

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

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Stop();
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

            if (Input.GetKeyDown(KeyCode.Q))
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

            player.transform.position = planeVehicle.transform.position;
            player.transform.rotation = planeVehicle.transform.rotation;
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
    
        private void PlayerActive()
        {
            player.SetActive(true);
            playerCamera.SetActive(true);
            
            planeVehicle.SetActive(false);
            planeVirtualCam.SetActive(false);
            planeFreeLookCam.SetActive(false);
        
            carVehicle.SetActive(false);
            carVirtualCam.SetActive(false);
            carFreeLookCam.SetActive(false);

            var position = player.transform.position;
            var rotation = player.transform.rotation;

            planeVehicle.transform.position = position;
            planeVehicle.transform.rotation = rotation;

            carVehicle.transform.position = new Vector3(position.x, position.y + 1f, position.z);
            carVehicle.transform.rotation = rotation;
            
            planeController.planeRigidbody.velocity *= 0f;
        }
        #endregion
    }
}
