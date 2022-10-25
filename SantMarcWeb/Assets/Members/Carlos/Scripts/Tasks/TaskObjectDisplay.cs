using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskObjectDisplay : MonoBehaviour
{
    [SerializeField] private TaskObject task;
    
    [SerializeField] private TextMeshProUGUI TMP_Description;

    // Start is called before the first frame update
    void Start()
    {
        TMP_Description.text = task.DescriptionTask_TXT;
    }
}
