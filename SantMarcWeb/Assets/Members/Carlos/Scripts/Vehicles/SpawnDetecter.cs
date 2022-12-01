using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDetecter : MonoBehaviour
{
    //Variables
    [SerializeField] private bool canTransform;

    public bool CanTransform => canTransform;

    private void Awake()
    {
        canTransform = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            canTransform = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            canTransform = true;
        }
    }
}
