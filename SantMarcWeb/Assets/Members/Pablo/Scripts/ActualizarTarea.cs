using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarTarea : MonoBehaviour
{

    public TareaDisplay TareaDisplay;
    public Tarea tarea_a_Mostrar;

    public bool primeraTarea;


    private void Awake()
    {
        
    }

    private void Start()
    {
        TareaDisplay = FindObjectOfType<TareaDisplay>();
        if (primeraTarea)
        {
            TareaDisplay.UpdateTarea(tarea_a_Mostrar);
            TareaDisplay.resetearTemporizador();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TareaDisplay.UpdateTarea(tarea_a_Mostrar);
            TareaDisplay.resetearTemporizador();
            Destroy(this.gameObject);
        }
    }
}
