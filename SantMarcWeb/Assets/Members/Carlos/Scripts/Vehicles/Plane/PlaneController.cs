using System;
using UnityEngine;

namespace Members.Carlos.Scripts.Vehicles.Plane
{
    public class PlaneController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
    
        [SerializeField] private float speed;
        [SerializeField] private float verticalRotationSpeed;
        [SerializeField] private float horizontalRotationSpeed;
        [SerializeField] private float horizontalInput;
        [SerializeField] private float verticalInput;
    
        public Rigidbody planeRigidbody;

        private void Awake()
        {
            planeRigidbody = GetComponent<Rigidbody>();
            gameManager = FindObjectOfType<GameManager>();
        }

// Update is called once per frame
        void Update ()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            planeRigidbody.AddForce(transform.forward * (speed * Time.deltaTime), ForceMode.Impulse);

            transform.Rotate(Vector3.back * (horizontalRotationSpeed * Time.deltaTime * horizontalInput));
            transform.Rotate(Vector3.right * (verticalRotationSpeed * Time.deltaTime * -verticalInput));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (gameManager != null)
            {
                if (collision.gameObject.CompareTag("Floor"))
                {
                    gameManager.changeToPlane = false;
                    gameManager.spawnPlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
        }
    }
}
