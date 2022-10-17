using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarTarea : MonoBehaviour
{

    public TareaDisplay TareaDisplay;


    private void Awake()
    {
        TareaDisplay = FindObjectOfType<TareaDisplay>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TareaDisplay.UpdateTarea();
        }
    }
}
