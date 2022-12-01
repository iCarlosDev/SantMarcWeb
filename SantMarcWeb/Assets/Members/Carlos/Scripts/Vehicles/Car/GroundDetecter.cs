using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetecter : MonoBehaviour
{
    public static GroundDetecter instance;
    [SerializeField] private bool canSpawn;

    public bool CanSpawn
    {
        get => canSpawn;
        set => canSpawn = value;
    }

    private void Awake()
    {
        instance = this;
        canSpawn = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            canSpawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            canSpawn = true;
        }
    }
}
