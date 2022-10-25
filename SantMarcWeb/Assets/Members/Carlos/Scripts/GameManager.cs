using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    [SerializeField] private PlaneController planeController;
    
    [SerializeField] private GameObject planeVehicle;
    [SerializeField] private GameObject planeCamera;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    
    public GameObject invocatePlaneVFX;
    
    public bool isChanged;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        invocatePlaneVFX.GetComponentInChildren<ParticleSystem>().Stop();
    }

    private void Update()
    {
        CambiarPersonaje();

        invocatePlaneVFX.transform.position = player.transform.position;
    }

    private void CambiarPersonaje()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isChanged = !isChanged;
            invocatePlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
        }
        
        if (isChanged)
        {
            player.SetActive(false);
            playerCamera.SetActive(false);
            
            planeVehicle.SetActive(true);
            planeCamera.SetActive(true);

            player.transform.position = planeVehicle.transform.position;
            player.transform.rotation = planeVehicle.transform.rotation;
        }
        else
        {
            player.SetActive(true);
            playerCamera.SetActive(true);
            
            planeVehicle.SetActive(false);
            planeCamera.SetActive(false);

            planeVehicle.transform.position = player.transform.position;
            planeVehicle.transform.rotation = player.transform.rotation;
            
            planeController.plane_Rigidbody.velocity *= 0f;
        }
    }
}
