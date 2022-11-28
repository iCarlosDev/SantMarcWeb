using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Members.Carlos.Scripts.Tasks
{
    public class TaskManager : MonoBehaviour
    {
        //Variables
        public static TaskManager instance;
        
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
    
        [Header("--- ACT 3 ---")]
        [Space(10)]
        public bool teacherFound;
        public bool teacherFound1Vz;
    
        [Header("--- ACT 4 ---")]
        [Space(10)]
        public bool task1Done;
        public bool task2Done;
        public bool task3Done;
        private static readonly int TaskIsOn = Animator.StringToHash("TaskIsOn");

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            tmpTaskSentence.text = task[0].descriptionTaskTxt;
        }
    
        public void TasksBoolCheck()
        {
            if (!firstDialogueCompleted)
            {
                firstDialogueCompleted = true;
                taskAnimator.SetBool(TaskIsOn, false);
                tmpTaskSentence.text = task[0].descriptionTaskTxt;
            }
        
            if (exit1Checked && exit2Checked && !teacherFound)
            {
                taskAnimator.SetBool(TaskIsOn, false);
                tmpTaskSentence.text = task[1].descriptionTaskTxt;
            }

            if (teacherFound && !teacherFound1Vz)
            {
                taskAnimator.SetBool(TaskIsOn, false);
                tmpTaskSentence.text = task[2].descriptionTaskTxt;
            }
        
            StartCoroutine(TaskAnimatorTransition());
        }

        public void SwapTaskAnimation()
        {
            taskAnimator.SetBool(TaskIsOn, false);
            StartCoroutine(TaskAnimatorTransition());
        }
    
        private IEnumerator TaskAnimatorTransition()
        {
            yield return new WaitForSeconds(2);
            taskAnimator.SetBool(TaskIsOn, true);
        }
    }
}
