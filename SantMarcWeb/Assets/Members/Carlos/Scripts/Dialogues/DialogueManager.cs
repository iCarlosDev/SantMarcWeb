using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Animator dialogueAnimator;
    
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private TextMeshProUGUI TMP_Sentence;
    [SerializeField] private Image Avatar_IMG;
    
    [Header("-------- TASKS --------")]
    [Space(10)]
    public Animator taskAnimator;
    public TaskObject[] task;
    public TextMeshProUGUI TMP_TaskSentence;

    [Header("--- ACT 1 BOOLS ---")] 
    [Space(10)] 
    public bool isAct1_Dialogue_1;
    
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        TMP_TaskSentence.text = task[0].DescriptionTask_TXT;
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
     
        dialogueAnimator.SetBool("DialogueIsOn", true);

        TMP_Name.text = dialogueObject.name_TXT;
        Avatar_IMG.sprite = dialogueObject.sprite_IMG;
        
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
        TMP_Sentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            TMP_Sentence.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        _gameManager.isDialogue = false;
        dialogueAnimator.SetBool("DialogueIsOn", false);
        
        ACT1_Bools_Manager();
    }

    private void ACT1_Bools_Manager()
    {
        if (!isAct1_Dialogue_1)
        {
            taskAnimator.SetBool("TaskIsOn", true);
        }
    }
}
