using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSound : MonoBehaviour
{
    //Variables
    [SerializeField] private AudioSource planeSound;

    private void Start()
    {
        InvokeRepeating("PlaneSounds", 0 ,0.1f);   
    }

    private void PlaneSounds()
    {
        if (planeSound != null)
        {
            if (!planeSound.isPlaying)
            {
                planeSound.Play();   
            }
        }
    }
}
