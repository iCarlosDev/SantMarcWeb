using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueObjectDisplay _dialogueObjectDisplay;
    [SerializeField] private GameManager _gameManager;
    private Queue<string> sentences;

    [SerializeField] private GameObject DialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        _gameManager.isDialogue = true;
        
        DialogueBox.SetActive(true);
        sentences.Clear();
        
        foreach (string sentence in dialogueObject.sentence_TXT)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueObjectDisplay.TMP_Description.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueObjectDisplay.TMP_Description.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        _gameManager.isDialogue = false;
        DialogueBox.SetActive(false);
    }
}
