using System;
using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Vector3 movement;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    
    [SerializeField] private Rigidbody plane_Rigidbody;
    
// Use this for initialization
    void Start ()
    {
        plane_Rigidbody = GetComponent<Rigidbody>();
    }

// Update is called once per frame
    void Update ()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        plane_Rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);

        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime * horizontalInput);
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);
    }

    private void FixedUpdate()
    {
    }
}
