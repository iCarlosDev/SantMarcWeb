using System;
using System.Collections;
using Members.Carlos.Scripts.Compass;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Members.Carlos.Scripts.Tasks
{
    public class TaskManager : MonoBehaviour
    {
        //Variables
        public static TaskManager instance;
        public PlayerController _Player;
        
        [Header("-------- TASKS --------")]
        [Space(10)]
        public Animator taskAnimator;
        public TaskObject[] task;
        public TextMeshProUGUI tmpTaskSentence;
    
        [Header("--- ACT 1 ---")]
        [Space(10)]
        public bool firstDialogueCompleted;
    
        [Header("--- ACT 2 ---")]
        [Space(10)]
        public bool exit1Checked;
        public bool exit2Checked;
        public bool exitsCompleted;
    
        [Header("--- ACT 3 ---")]
        [Space(10)]
        public bool teacherFound;
        public bool teacherFound1Vz;

        [Header("--- GAME ACT ---")] 
        [Space(10)] 
        [SerializeField] private bool coinsCollected;
        
        private static readonly int TaskIsOn = Animator.StringToHash("TaskIsOn");

        public bool CoinsCollected
        {
            get => coinsCollected;
            set => coinsCollected = value;
        }

        private void Awake()
        {
            instance = this;
            _Player = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            tmpTaskSentence.text = task[0].descriptionTaskTxt;

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                StartCoroutine(TaskAnimatorTransition());
                tmpTaskSentence.text = task[3].descriptionTaskTxt;
            }
        }
    
        public void TasksBoolCheck()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!firstDialogueCompleted)
                {
                    firstDialogueCompleted = true;
                    taskAnimator.SetBool(TaskIsOn, false);
                    tmpTaskSentence.text = task[0].descriptionTaskTxt;
                }

                if (exit1Checked && exit2Checked && !teacherFound && !exitsCompleted)
                {
                    exitsCompleted = true;
                    taskAnimator.SetBool(TaskIsOn, false);
                    tmpTaskSentence.text = task[1].descriptionTaskTxt;
                }

                if (teacherFound && !teacherFound1Vz)
                {
                    taskAnimator.SetBool(TaskIsOn, false);
                    tmpTaskSentence.text = task[2].descriptionTaskTxt;
                }
            }
            else
            {
                if (coinsCollected)
                {
                    taskAnimator.SetBool(TaskIsOn, false);
                    tmpTaskSentence.text = task[4].descriptionTaskTxt;
                    Compass.Compass.instance.AddQuestMarker(Compass.Compass.instance.Exit);
                }
            }

            StartCoroutine(TaskAnimatorTransition());
        }

        public void SwapTaskAnimation()
        {
            taskAnimator.SetBool(TaskIsOn, false);
            StartCoroutine(TaskAnimatorTransition());
        }
    
        public IEnumerator TaskAnimatorTransition()
        {
            yield return new WaitForSeconds(2);
            taskAnimator.SetBool(TaskIsOn, true);
        }
    }
}
