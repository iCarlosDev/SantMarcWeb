using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint_Tarea : MonoBehaviour
{

    private TareaDisplay _display;

    public Tarea tarea;
    public bool checkpoint;

    private bool playerOnRange;
    
    private void Awake()
    {
        _display = FindObjectOfType<TareaDisplay>();
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
                    _display.CompletarCheckpoint();
                    checkpoint = true; 
                    _display.checkpointUI.SetActive(false); 
                    Destroy(this);

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
