using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts;
using Members.Carlos.Scripts.Dialogues;
using Members.Carlos.Scripts.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn_FadeOut_Manager : MonoBehaviour
{
   public static FadeIn_FadeOut_Manager instance;
   
   [SerializeField] private Animator animator;
   
   private static readonly int FadeOut = Animator.StringToHash("FadeOut");
   private static readonly int FadeIn = Animator.StringToHash("FadeIn");

   public Animator Animator
   {
      get => animator;
      set => animator = value;
   }

   private void Awake()
   {
      instance = this;
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      StartCoroutine(WaitToFade());
   }

   public IEnumerator WaitToChangeFinalLevel()
   {
      yield return new WaitForSeconds(10);
      animator.SetBool(FadeIn, true);
   }

   private IEnumerator WaitToFade()
   {
      yield return new WaitForSeconds(2);
      animator.SetBool(FadeOut, true);
   }

   public void DialogueStart()
   {
      if (SceneManager.GetActiveScene().buildIndex == 0)
      {
         GameManager.instance.CanNotMove = false;
         DialogueManager.instance.StartDialogue(DialogueManager.instance.FirstDialogueObject);
      }
      else if (SceneManager.GetActiveScene().buildIndex == 2)
      {
         TaskManager.instance.StartCoroutine(TaskManager.instance.TaskAnimatorTransition());
         TaskManager.instance.tmpTaskSentence.text = TaskManager.instance.task[0].descriptionTaskTxt;
      }
   }

   public void ChangeFinalScene()
   {
      if (SceneManager.GetActiveScene().buildIndex == 0)
      {
         SceneManager.LoadScene(1);
      }
      else
      {
         DialogueManager.instance.StartDialogue(DialogueManager.instance.FirstDialogueObject);
      }
   }

   public void CanOpenControlsMenu()
   {
      GameManager.instance.canOpenControls = true;
   }

   public void CanNotOpenControlsMenu()
   {
      GameManager.instance.canOpenControls = false;
      GameManager.instance.CloseControlsMenu();
   }
}
