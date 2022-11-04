using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TareaDisplay : MonoBehaviour
{

    public Tarea tarea;
    
    public Tarea[] ArrayDeTareas;

    public int TareasCompletadas;

    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI descripcionText;

    [SerializeField] private GameObject recompensaImageParent;
    public Image recompensaImage;

    [SerializeField] private GameObject tiempoParent;
    public TextMeshProUGUI tiempoParaCompletar;

    [SerializeField] public int segundos, minutos;

    public bool Recompensa;
    public bool Tiempo;
    
    public bool checkpoint1;
    public bool checkpoint2;
    public bool checkpoint3;
    
    public bool checkpoint1_C;
    public bool checkpoint2_C;
    public bool checkpoint3_C;
    
    public GameObject checkpointUI;
    
    //private bool timerStarted = false;
    [SerializeField] private bool TareaCompletada = true;

    private void Awake()
    {
        recompensaImage.enabled = false;
        recompensaImageParent = recompensaImage.gameObject.transform.parent.gameObject;
        tiempoParent = tiempoParaCompletar.gameObject.transform.parent.gameObject;
        
        checkpoint1 = tarea.checkpoint1;
        checkpoint2 = tarea.checkpoint2;
        checkpoint3 = tarea.checkpoint3;
    }


    private void Start()
    {
        CambiarTarea(TareasCompletadas);
    }

    #region Temporizador

    public void empezarTemporizador()
    {
        minutos = int.Parse(tarea.minParaCompletarla);
        segundos = int.Parse(tarea.segParaCompletarla);
        escribirTiempo(minutos, segundos);
        //if (!timerStarted)
        {
            Invoke("actualizarTemporizador", 1f);
        }
        //timerStarted = true;
    }

    public void resetearTemporizador()
    {
        escribirTiempo(03, 00);
    }

    private void actualizarTemporizador()
    {
        if(TareaCompletada == false)
        {
            if (Tiempo)
            {
                segundos--;
                if (segundos < 0)
                {
                    if (minutos == 0)
                    {
                        //Tiempo Agotado
                        Debug.Log("TIEMPO AGOTADO");
                    }
                    else
                    {
                        minutos--;
                        segundos = 59;
                    }
                }
                escribirTiempo(minutos, segundos);
                Invoke("actualizarTemporizador", 1f);
            }
        }
    }

    private void escribirTiempo(int minutos, int segundos)
    {
        if (segundos < 10)
        {
            tiempoParaCompletar.text = minutos.ToString() + ":0" + segundos.ToString();
        }
        else
        {
            tiempoParaCompletar.text = minutos.ToString() + ":" + segundos.ToString();
        }
        
    }

    #endregion
   
    public void UpdateTarea(Tarea nuevaTarea)
    {
        tarea = nuevaTarea;
        nombreText.text = nuevaTarea.name;
        descripcionText.text = nuevaTarea.descripcion;
        
        checkpoint1 = nuevaTarea.checkpoint1;
        checkpoint2 = nuevaTarea.checkpoint2;
        checkpoint3 = nuevaTarea.checkpoint3;

        checkpoint1_C = false;
        checkpoint2_C = false;
        checkpoint3_C = false;

        Recompensa = nuevaTarea.recompensa;
        Tiempo = nuevaTarea.Tiempo;
        
        if (Recompensa)
        {
            recompensaImage.enabled = true;
            recompensaImage.sprite = nuevaTarea.recompensa;
            recompensaImageParent.SetActive(true);
        }
        else
        {
            recompensaImageParent.SetActive(false);
        }
        
        if (Tiempo)
        {
            empezarTemporizador();
            tiempoParent.SetActive(true);
        }
        else
        {
            resetearTemporizador();
        }
    }

    public void CambiarTarea(int ArrayTareas_Index)
    {
        UpdateTarea(ArrayDeTareas[ArrayTareas_Index]);
        resetearTemporizador();
    }

    public void CompletarCheckpoint()
    {
        if (checkpoint1 && !checkpoint1_C)
        {
            checkpoint1_C = true;
            
            if (!checkpoint2 && !checkpoint3)
            {
                TareasCompletadas++;
                CambiarTarea(TareasCompletadas);
            }
        }
        else if (checkpoint2 && !checkpoint2_C)
        {
            checkpoint2_C = true;
            if (!checkpoint3)
            {
                TareasCompletadas++;
                CambiarTarea(TareasCompletadas);
            }
        }
        else if (checkpoint3 && !checkpoint3_C)
        {
            checkpoint3_C = true;
            TareasCompletadas++;
            CambiarTarea(TareasCompletadas);
        }
    }
}
