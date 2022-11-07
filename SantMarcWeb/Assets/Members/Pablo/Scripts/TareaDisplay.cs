using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts;
using Members.Carlos.Scripts.Dialogues;
using Members.Carlos.Scripts.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TareaDisplay : MonoBehaviour
{

    public GameManager gameManager;
    
    public Tarea tarea;
    
    public Tarea[] ArrayDeTareas;

    public int TareasCompletadas;

    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI descripcionText;
    
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
    private static readonly int TaskIsOn = Animator.StringToHash("TaskIsOn");

    private void Awake()
    {
        tiempoParent = tiempoParaCompletar.gameObject.transform.parent.gameObject;
    }


    private void Start()
    {
        tiempoParent.SetActive(false);
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
        //nombreText.text = nuevaTarea.name;
        descripcionText.text = nuevaTarea.descripcion;
        checkpointUI.GetComponentInChildren<TextMeshProUGUI>().text = "Presiona E para " + tarea.interactionString;
        
        checkpoint1 = nuevaTarea.checkpoint1;
        checkpoint2 = nuevaTarea.checkpoint2;
        checkpoint3 = nuevaTarea.checkpoint3;

        checkpoint1_C = false;
        checkpoint2_C = false;
        checkpoint3_C = false;
        
        Tiempo = nuevaTarea.Tiempo;
        
        if (Tiempo)
        {
            empezarTemporizador();
            tiempoParent.SetActive(true);
        }
        else
        {
            resetearTemporizador();
            tiempoParent.SetActive(false);
        }
    }

    public void CambiarTarea(int ArrayTareas_Index)
    {
        gameManager.instance.taskManager.SwapTaskAnimation();
        StartCoroutine(Delay(ArrayTareas_Index));
    }

    private IEnumerator Delay(int ArrayTareas_Index)
    {
        yield return new WaitForSeconds(1);
        UpdateTarea(ArrayDeTareas[ArrayTareas_Index]);
        resetearTemporizador();
    }

    public void CompletarCheckpoint()
    {
        if (checkpoint1 && !checkpoint1_C)
        {
            checkpoint1_C = true;
            if (tarea.dialogueObjects.Length > 0)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(tarea.dialogueObjects[0]);
            }
            
            if (!checkpoint2 && !checkpoint3)
            {
                TareasCompletadas++;
                CambiarTarea(TareasCompletadas);
            }
        }
        else if (checkpoint2 && !checkpoint2_C)
        {
            checkpoint2_C = true;
            
            if (tarea.dialogueObjects.Length > 0)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(tarea.dialogueObjects[1]);
            }
            
            if (!checkpoint3)
            {
                TareasCompletadas++;
                CambiarTarea(TareasCompletadas);
            }
        }
        else if (checkpoint3 && !checkpoint3_C)
        {
            checkpoint3_C = true;
            
            if (tarea.dialogueObjects.Length > 0)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(tarea.dialogueObjects[2]);
            }
            
            TareasCompletadas++;
            CambiarTarea(TareasCompletadas);
        }
    }
}
