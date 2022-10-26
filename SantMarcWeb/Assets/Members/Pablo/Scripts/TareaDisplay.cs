using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TareaDisplay : MonoBehaviour
{

    public Tarea tarea;

    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI descripcionText;

    [SerializeField] private GameObject recompensaImageParent;
    public Image recompensaImage;

    [SerializeField] private GameObject tiempoParent;
    public TextMeshProUGUI tiempoParaCompletar;

    [SerializeField] public int segundos, minutos;

    public bool Recompensa;
    public bool Tiempo;

    //private bool timerStarted = false;
    [SerializeField] private bool TareaCompletada = true;


    private void Start()
    {
        recompensaImage.enabled = false;
        recompensaImageParent = recompensaImage.gameObject.transform.parent.gameObject;
        tiempoParent = tiempoParaCompletar.gameObject.transform.parent.gameObject;
    }

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

    public void UpdateTarea(Tarea nuevaTarea)
    {
        nombreText.text = nuevaTarea.name;
        descripcionText.text = nuevaTarea.descripcion;

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
}
