using System;
using Members.Carlos.Scripts.Tasks;
using TMPro;
using UnityEngine;

namespace Members.Carlos.Scripts.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueObject dialogueObject;
        public TaskManager taskManager;
        
        [SerializeField] private TareaDisplay _display;
        [SerializeField] private bool playerOnRange;

        private void Awake()
        {
            _display = FindObjectOfType<TareaDisplay>();
        }

        private void Update()
        {
            if (playerOnRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _display.checkpointUI.SetActive(false); 
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogueObject);

                    if (taskManager.firstDialogueCompleted && !taskManager.exit1Checked)
                    {
                        taskManager.exit1Checked = true;
                        Destroy(gameObject.GetComponent<Collider>());
                        Destroy(this);
                        return;
                    }
            
                    if (taskManager.exit1Checked && !taskManager.exit2Checked)
                    {
                        taskManager.exit2Checked = true;
                        Destroy(gameObject.GetComponent<Collider>());
                        Destroy(this);
                        return;
                    }

                    if (taskManager.exit2Checked)
                    {
                        taskManager.teacherFound = true;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _display.checkpointUI.SetActive(true);
            playerOnRange = true;
            if(taskManager.exit2Checked == false)
            {
                _display.checkpointUI.GetComponentInChildren<TextMeshProUGUI>().text = "Presiona E para " + taskManager.task[0].InteracionString;
            }
            else
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
