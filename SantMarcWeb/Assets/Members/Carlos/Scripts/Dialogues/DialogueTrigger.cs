using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueObject dialogueObject;
    public DialogueManager _dialogueManager;

    [SerializeField] private bool isPlayerTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueObject);
            isPlayerTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTriggered = false;
        }

        if (!isPlayerTriggered)
        {
            if (!_dialogueManager.isAct1_Dialogue_1)
            {
                _dialogueManager.isAct1_Dialogue_1 = true;
                _dialogueManager.taskAnimator.SetBool("TaskIsOn", false);
                StartCoroutine(TaskAnimatorTransition());
            }
        }
    }

    private IEnumerator TaskAnimatorTransition()
    {
        yield return new WaitForSeconds(2);
        _dialogueManager.TMP_TaskSentence.text = _dialogueManager.task[1].DescriptionTask_TXT  ;
        _dialogueManager.taskAnimator.SetBool("TaskIsOn", true);
    }
}
