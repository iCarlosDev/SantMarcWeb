using System;
using Members.Carlos.Scripts.Compass;
using Members.Carlos.Scripts.Tasks;
using TMPro;
using UnityEngine;

namespace Members.Carlos.Scripts.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueObject dialogueObject;
        public TaskManager taskManager;
        public Compass.Compass compass;
        
        [SerializeField] private TareaDisplay _display;
        [SerializeField] private bool playerOnRange;

        private void Awake()
        {
            _display = FindObjectOfType<TareaDisplay>();
            compass = FindObjectOfType<Compass.Compass>();
        }

        private void Update()
        {
            if (playerOnRange)
            {
                if (Input.GetKeyDown(KeyCode.E) && !GameManager.instance.isDialogue)
                {
                    _display.checkpointUI.SetActive(false); 
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogueObject);

                    if (taskManager.firstDialogueCompleted && !taskManager.exit1Checked)
                    {
                        taskManager.exit1Checked = true;
                        Destroy(gameObject.GetComponent<Collider>());
                        Destroy(this);
                        
                        if (gameObject.CompareTag("Door1"))
                        {
                            Destroy(compass.QuestMarkers[0].Image.gameObject);
                            Destroy(compass.QuestMarkers[0].gameObject.GetComponent<Outline>());
                            compass.QuestMarkers.Remove(compass.QuestMarkers[0]);
                        }
                        else
                        {
                            Destroy(compass.QuestMarkers[1].Image.gameObject);
                            Destroy(compass.QuestMarkers[1].gameObject.GetComponent<Outline>());
                            compass.QuestMarkers.Remove(compass.QuestMarkers[1]);
                        }
                        return;
                    }

                    if (!taskManager.teacherFound1Vz)
                    {
                        foreach (QuestMarker compass_ in compass.QuestMarkers)
                        {
                            Destroy(compass_.gameObject.GetComponent<Outline>());
                            Destroy(compass_.Image.gameObject);
                        }
                    }
                    
                    if (taskManager.exit1Checked && !taskManager.exit2Checked)
                    {
                        taskManager.exit2Checked = true;
                        Destroy(gameObject.GetComponent<Collider>());
                        Destroy(this);
                        
                        compass.QuestMarkers.Clear();
                        compass.AddQuestMarker(compass.Teacher);
                        return;
                    }

                    if (taskManager.exit2Checked && !taskManager.teacherFound)
                    {
                        taskManager.teacherFound = true;
                        
                        compass.QuestMarkers.Clear();
                        compass.AddQuestMarker(compass.Modeling);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _display.checkpointUI.SetActive(true);
            playerOnRange = true;
            if(!taskManager.exit2Checked)
            {
                _display.checkpointUI.GetComponentInChildren<TextMeshProUGUI>().text = "Presiona E para " + taskManager.task[0].InteracionString;
            }
            else if (!taskManager.teacherFound1Vz)
            {
                _display.checkpointUI.GetComponentInChildren<TextMeshProUGUI>().text = "Presiona E para " + taskManager.task[1].InteracionString;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            { 
                _display.checkpointUI.SetActive(false); 
                playerOnRange = false;
            }
        }
    }
}
