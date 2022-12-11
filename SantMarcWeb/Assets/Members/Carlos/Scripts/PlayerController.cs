using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Members.Carlos.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        //Variables
        [SerializeField] private CharacterController controller;
        [SerializeField] private Vector3 Velocity0;
        [SerializeField] private Transform cam;
    
        [Header("--- PLAYER BASIC VALUES ---")] 
        [Space(10)] 
        [SerializeField] private float speed = 6f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float turnSmoothVelocity;
        public float horizontal;
        public float vertical;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        [Header("--- PLAYER GROUND VALUES ---")] 
        [Space(10)]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform groundCheckAnimator;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private float groundDistanceAnimator = 0.4f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Vector3 velocity;
        public bool isGrounded;
        public bool isGroundedAnimator;

        [Header("--- ANIMATIONS ---")] 
        [Space(10)] 
        public Animator playerAnimator;

        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int WalkToSprint = Animator.StringToHash("WalkToSprint");
        private static readonly int IsSprinting = Animator.StringToHash("IsSprinting");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int Grounded = Animator.StringToHash("Grounded");

        [Header("--- VFX ---")] [Space(10)] 
        public GameObject CompletarTareaVFX;
        private GameObject TEMP_CompletarTareaVFX;
        
        private void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                playerAnimator.SetBool(Grounded,true);
            }
        }

        private void Update()
        {
            PlayerControl();
            AnimationsController();
        }

        private void PlayerControl()
        {
            gameObject.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            if (groundCheckAnimator != null)
            {
                isGroundedAnimator = Physics.CheckSphere(groundCheckAnimator.position, groundDistanceAnimator, groundMask);
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * (speed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded && vertical >= 1)
            {
                if (speed >= 3)
                {
                    speed = 3;
                }
                
                playerAnimator.SetBool(IsSprinting, true);
                speed += Time.deltaTime * 3;
            }
            else
            {
                if (speed <= 1)
                {
                    speed = 1;
                }

                if (controller.velocity.Equals(Velocity0))
                {
                    playerAnimator.SetBool(IsSprinting, false);
                    speed -= Time.deltaTime * 5;
                }
                else
                {
                    playerAnimator.SetBool(IsSprinting, false);
                    speed -= Time.deltaTime * 3; 
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && SceneManager.GetActiveScene().buildIndex == 1)
            {
                playerAnimator.SetBool(IsJumping, true);
            }
            
            if (isGroundedAnimator)
            {
                playerAnimator.SetBool(Grounded,true);
            }
        }

        public void VFXCompletarTarea()
        {
            TEMP_CompletarTareaVFX = Instantiate(CompletarTareaVFX, this.transform);
            StartCoroutine(DestroyVFX());
        }
        public IEnumerator DestroyVFX()
        {
            yield return new WaitForSeconds(2);
            Destroy(TEMP_CompletarTareaVFX);
        }

        public void Jump()
        {
            playerAnimator.SetBool(IsJumping, true); 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        public void IsNotJumping()
        {
            playerAnimator.SetBool(IsJumping, false);
        }

        public void isNotGrounded()
        {
            playerAnimator.SetBool(Grounded, false);
        }

        private void AnimationsController()
        {
            playerAnimator.SetFloat(X, horizontal);
            playerAnimator.SetFloat(Y, vertical);
            playerAnimator.SetFloat(WalkToSprint, speed);
        }
    }
}