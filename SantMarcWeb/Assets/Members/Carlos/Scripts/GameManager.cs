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
    [SerializeField] private GameObject generalCamera;
    
    [Header("--- PLANE ---")]
    [SerializeField] private GameObject planeVehicle;
    [SerializeField] private GameObject planeCamera;
    [SerializeField] private CinemachineFreeLook freeLookCam;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private bool canResetCam;
    
    [Header("--- PLAYER ---")]
    [Space(10)]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    
    public GameObject invocatePlaneVFX;
    
    public bool isChanged;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            isChanged = !isChanged;
            invocatePlaneVFX.GetComponentInChildren<ParticleSystem>().Play();
        }
        
        if (isChanged)
        {
            PlaneActive();
            PlaneCameraMovement();
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
        planeCamera.SetActive(true);

        player.transform.position = planeVehicle.transform.position;
        player.transform.rotation = planeVehicle.transform.rotation;
    }
    
    private void PlaneCameraMovement()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (canResetCam && (mouseX != 0 || mouseY != 0))
        {
            canResetCam = false;
            virtualCam.enabled = false;
            freeLookCam.enabled = true;
            freeLookCam.Priority = 10;
            virtualCam.Priority = 0;
            StartCoroutine(WaitResetCamera());
        }
    }

    private IEnumerator WaitResetCamera()
    {
        yield return new WaitForSeconds(3);
        freeLookCam.enabled = false;
        virtualCam.enabled = true;
        freeLookCam.Priority = 0;
        virtualCam.Priority = 10;
        canResetCam = true;
    }
    
    #endregion

    #region - PLAYER -
    
    private void PlayerActive()
    {
        player.SetActive(true);
        playerCamera.SetActive(true);
            
        planeVehicle.SetActive(false);
        planeCamera.SetActive(false);

        planeVehicle.transform.position = player.transform.position;
        planeVehicle.transform.rotation = player.transform.rotation;
            
        planeController.plane_Rigidbody.velocity *= 0f;
    }
    
    #endregion
}
