using System.Collections;
using Members.Carlos.Scripts.Tasks;
using UnityEngine;

namespace Members.Carlos.Scripts
{
    public class GameManager : MonoBehaviour
    {
        //Variables
    
        [SerializeField] private PlaneController planeController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private TaskManager taskManager;

        [Header("--- PLANE ---")]
        [SerializeField] private GameObject planeVehicle;
        [SerializeField] private GameObject planeVirtualCam;
        [SerializeField] private GameObject planeFreeLookCam;
        [SerializeField] private bool canResetPlaneCam;

        [Header("--- PLAYER ---")]
        [Space(10)]
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;

        [Header("--- CAR ---")] 
        [Space(10)] 
        [SerializeField] private GameObject carVehicle;
        [SerializeField] private GameObject carVirtualCam;
        [SerializeField] private GameObject carFreeLookCam;
        [SerializeField] private bool canResetCarCam;

        [Header("--- DIALOGUE MODE ---")] 
        [Space(10)]
        public bool isDialogue;

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

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Stop();
        }

        private void Update()
        {
            ChangeCharacter();
        
            spawnPlaneVFX.transform.position = player.transform.position;

            if (isDialogue)
            {
                playerController.enabled = false;
                playerController.horizontal = 0;
                playerController.vertical = 0;
                playerController.playerAnimator.SetFloat(X, 0);
                playerController.playerAnimator.SetFloat(Y, 0);
            }
            else
            {
                playerController.enabled = true;
            }

            if (taskManager.exit2Checked)
            {
                teachersDoor.transform.localEulerAngles = new Vector3(0, 210, 0);
                studentsDoor.transform.localEulerAngles = new Vector3(0, 105, 0);
            }
        }

        private void ChangeCharacter()
        {
            if (Input.GetKeyDown(KeyCode.F) && !playerController.isGrounded)
            {
                changeToPlane = !changeToPlane;
                spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.F) && playerController.isGrounded)
            {
                changeToCar = !changeToCar;
                spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
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
            }
        }

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
                StopCoroutine(nameof(WaitResetPlaneCamera));
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

            player.transform.position = carVehicle.transform.position;
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
                StopCoroutine(nameof(WaitResetCarCamera));
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

            planeVehicle.transform.position = player.transform.position;
            planeVehicle.transform.rotation = player.transform.rotation;

            carVehicle.transform.position = planeVehicle.transform.position;
            carVehicle.transform.rotation = planeVehicle.transform.rotation;
            
            planeController.planeRigidbody.velocity *= 0f;
        }
        #endregion
    }
}
