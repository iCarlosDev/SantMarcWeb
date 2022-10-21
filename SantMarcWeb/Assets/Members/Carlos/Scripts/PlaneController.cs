using System;
using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private float speed;
    [SerializeField] private float verticalRotationSpeed;
    [SerializeField] private float horizontalRotationSpeed;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    
    [SerializeField] private Rigidbody plane_Rigidbody;
    
// Use this for initialization
    void Start ()
    {
        plane_Rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

// Update is called once per frame
    void Update ()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        plane_Rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);

        transform.Rotate(Vector3.back * horizontalRotationSpeed * Time.deltaTime * horizontalInput);
        transform.Rotate(Vector3.right * verticalRotationSpeed * Time.deltaTime * verticalInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            gameManager.isChanged = false;
        }
    }
}
