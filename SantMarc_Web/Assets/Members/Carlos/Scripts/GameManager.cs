using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject planeVehicle;
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    private void Start()
    {
        planeVehicle = GameObject.FindWithTag("Plane");
        player = GameObject.FindWithTag("Player");
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();

        cinemachineCamera.Follow = player.transform;
        cinemachineCamera.LookAt = player.transform;
    }

    private void Update()
    {
        
    }

    private void CambiarPersonaje()
    {
        
    }
}
