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

    private GameObject recompensaImageParent;
    public Image recompensaImage;

    private GameObject tiempoParent;
    public TextMeshProUGUI tiempoParaCompletar;

    [SerializeField] private int segundos, minutos;

    public bool Recompensa;
    public bool Tiempo;
    
    public void empezarTemporizador()
    {
        escribirTiempo(minutos, segundos);
        Invoke("actualizarTemporizador", 1f);
    }

    private void actualizarTemporizador()
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

    public void UpdateTarea()
    {
        nombreText.text = tarea.name;
        descripcionText.text = tarea.descripcion;

        Recompensa = tarea.recompensa;
        Tiempo = tarea.Tiempo;
        
        if (Recompensa)
        {
            recompensaImage.sprite = tarea.recompensa;
        }
        else
        {
            recompensaImageParent = recompensaImage.gameObject.transform.parent.gameObject;
            recompensaImageParent.SetActive(false);
        }
        
        if (Tiempo)
        {
            empezarTemporizador();
        }
        else
        {
            tiempoParent = tiempoParaCompletar.gameObject.transform.parent.gameObject;
            tiempoParent.SetActive(false);
        }


    }
}
