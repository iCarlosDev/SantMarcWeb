using Members.Carlos.Scripts.Tasks;
using UnityEngine;

namespace Members.Carlos.Scripts.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueObject dialogueObject;
        public TaskManager taskManager;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueObject);
            
            if (taskManager.firstDialogueCompleted && !taskManager.exit1Checked)
            {
                taskManager.exit1Checked = true;
                Destroy(gameObject.GetComponent<Collider>());
                return;
            }
            
            if (taskManager.exit1Checked && !taskManager.exit2Checked)
            {
                taskManager.exit2Checked = true;
                Destroy(gameObject.GetComponent<Collider>());
                return;
            }

            if (taskManager.exit2Checked)
            {
                taskManager.teacherFound = true;
            }
        }
    }
}
