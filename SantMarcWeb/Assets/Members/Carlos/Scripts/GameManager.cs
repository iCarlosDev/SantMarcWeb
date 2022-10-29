using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    //Variables
    
    [SerializeField] private PlaneController planeController;
    [SerializeField] private PlayerController playerController;
    
    [Header("--- PLANE ---")]
    [SerializeField] private GameObject planeVehicle;
    [SerializeField] private GameObject planeVirtualCam;
    [SerializeField] private GameObject planeFreeLookCam;
    [SerializeField] private bool canResetPlaneCam;
    private Coroutine WaitPlaneCamera;
    
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
    private Coroutine WaitCarCamera;
    
    
    [Header("--- OTHER ---")]
    [Space(10)]
    public GameObject invocatePlaneVFX;
    
    public bool changeToPlane;
    public bool changeToCar ;

    public float mouseX, mouseY;

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
        if (Input.GetKeyDown(KeyCode.F) && !playerController.isGrounded)
        {
            changeToPlane = !changeToPlane;
            invocatePlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.F) && playerController.isGrounded)
        {
            changeToCar = !changeToCar;
            invocatePlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
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
            StopCoroutine("WaitResetPlaneCamera");
        }
        else if (!canResetPlaneCam && (mouseX == 0 && mouseY == 0))
        {
            StartCoroutine("WaitResetPlaneCamera");
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
            StopCoroutine("WaitResetCarCamera");
        }
        else if (!canResetCarCam && (mouseX == 0 && mouseY == 0))
        {
           StartCoroutine("WaitResetCarCamera");
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

        carVehicle.transform.position = player.transform.position;
        carVehicle.transform.rotation = player.transform.rotation;
            
        planeController.plane_Rigidbody.velocity *= 0f;
    }
    
    #endregion
}
