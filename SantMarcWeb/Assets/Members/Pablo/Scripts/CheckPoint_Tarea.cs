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
    private TareaDisplay _display;
    private CinemachineSwitcher _cinemachine;
    public Tarea tarea;
   
    public bool checkpoint;
    
    [Header("--- PRUEBA ---")] [Space(10)]
    private bool playerOnRange;
    
    private void Awake()
    {
        _display = FindObjectOfType<TareaDisplay>();
        _cinemachine = FindObjectOfType<CinemachineSwitcher>();
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
                    CompletarTarea(true);
                }
            }
        }
    }

    public void CompletarTarea(bool starting)
    {
        if (starting)
        {
            if (_display.tarea == _display.ArrayDeTareas[0])
            {
                _display.checkpointUI.SetActive(false);
                _cinemachine.ActivarPCModelaje();
            }
            else if (_display.tarea == _display.ArrayDeTareas[1])
            {
                _display.checkpointUI.SetActive(false);
                _cinemachine.ActivarPCTexturizado();
            }
            else if (_display.tarea == _display.ArrayDeTareas[2])
            {
                _display.checkpointUI.SetActive(false);
                _cinemachine.ActivarPCProgramacion();
            }
        }
        else
        {
            _display.CompletarCheckpoint();
            checkpoint = true; 
            _display.checkpointUI.SetActive(false); 
            
            if (_display.tarea == _display.ArrayDeTareas[0])
            {
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.Texture);
            }
            else if (_display.tarea == _display.ArrayDeTareas[1])
            {
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.Programming);
            }
            else if (_display.tarea == _display.ArrayDeTareas[2])
            {
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].gameObject.GetComponent<Outline>());
                Destroy(Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers[0].Image.gameObject);
                Members.Carlos.Scripts.Compass.Compass.instance.QuestMarkers.Clear();
                Members.Carlos.Scripts.Compass.Compass.instance.AddQuestMarker(Members.Carlos.Scripts.Compass.Compass.instance.PlayVR);

                Time.fixedDeltaTime = 0.02f;
            }
            
            Destroy(this);
        }

        if (tarea == _display.ArrayDeTareas[3])
        {
            SceneManager.LoadScene(1);
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
