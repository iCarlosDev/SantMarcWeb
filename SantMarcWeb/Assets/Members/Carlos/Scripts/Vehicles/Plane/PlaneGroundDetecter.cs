using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGroundDetecter : MonoBehaviour
{
    public static PlaneGroundDetecter instance;
    
    [SerializeField] private bool changeSpawnPos;

    public bool ChangeSpawnPos
    {
        get => changeSpawnPos;
        set => changeSpawnPos = value;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        changeSpawnPos = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            changeSpawnPos = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            changeSpawnPos = false;
        }
    }
}
