using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Members.Carlos.Scripts.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint_Tarea : MonoBehaviour
{
    public GameObject SnakeCanvas;
    
    private TareaDisplay _display;

    private CinemachineSwitcher _cinemachine;

    public Tarea tarea;
    public bool checkpoint;

    private bool playerOnRange;
    
    private void Awake()
    {
        _display = FindObjectOfType<TareaDisplay>();
        _cinemachine = FindObjectOfType<CinemachineSwitcher>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnRange)
        {
            if (tarea == _display.tarea)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (_display.tarea == _display.ArrayDeTareas[0])
                    {
                        _display.checkpointUI.SetActive(false);
                        _cinemachine.ActivarPCModelaje();
                    }
                    else
                    {
                        _display.CompletarCheckpoint();
                        checkpoint = true; 
                        _display.checkpointUI.SetActive(false); 
                        Destroy(this);
                    }
                    
                    if (!TaskManager.instance.task1Done)
                    {
                        TaskManager.instance.task1Done = true;

                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                        Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                        Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.Texture);
                        return;
                    }

                    if (TaskManager.instance.task1Done && !TaskManager.instance.task2Done)
                    {
                        TaskManager.instance.task2Done = true;
                        
                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                        Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                        Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.Programming);
                        return;
                    }

                    if (TaskManager.instance.task2Done && !TaskManager.instance.task3Done)
                    {
                        TaskManager.instance.task3Done = true;
                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                        Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                        Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                        Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.PlayVR);
                        return;
                    }

                    if (tarea == _display.ArrayDeTareas[3])
                    {
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (tarea == _display.tarea)
        {
            if (other.tag == "Player")
            {
                _display.checkpointUI.SetActive(true);
                playerOnRange = true;
                _display.checkpointUI.GetComponentInChildren<TextMeshProUGUI>().text = "Presiona E para " + _display.tarea.interactionString;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (tarea == _display.tarea)
        {
            if (other.tag == "Player")
            {
                _display.checkpointUI.SetActive(false);
                playerOnRange = false;
            }
        }
    }
}
