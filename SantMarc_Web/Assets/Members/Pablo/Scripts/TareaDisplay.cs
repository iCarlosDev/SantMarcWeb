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

    public TextMeshProUGUI minParaCompletar;
    public TextMeshProUGUI segParaCompletar;

    public bool Recompensa;
    public bool Tiempo;
    
    void Start()
    {/*
        tarea.Print();

        nombreText.text = tarea.name;
        descripcionText.text = tarea.descripcion;
        
        recompensaImage.sprite = tarea.recompensa;

        minParaCompletar.text = tarea.minParaCompletarla;
        segParaCompletar.text = tarea.segParaCompletarla;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTarea()
    {
        nombreText.text = tarea.name;
        descripcionText.text = tarea.descripcion;
        
        

        if (Recompensa)
        {
            recompensaImage.sprite = tarea.recompensa;
        }
        else
        {
            recompensaImageParent = recompensaImage.GetComponentInParent(gameObject);
        }
        
        if (Tiempo)
        {
            minParaCompletar.text = tarea.minParaCompletarla;
            segParaCompletar.text = tarea.segParaCompletarla;
        }
        else
        {
            minParaCompletar.gameObject.SetActive(false);
            segParaCompletar.gameObject.SetActive(false);
        }


    }
}
