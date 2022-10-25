using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueObjectDisplay : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private TextMeshProUGUI TMP_Description;
    
    [SerializeField] private Image Avatar_IMG;
    
    // Start is called before the first frame update
    void Start()
    {
        TMP_Name.text = dialogueObject.name_TXT;
        TMP_Description.text = dialogueObject.description_TXT;

        Avatar_IMG.sprite = dialogueObject.sprite_IMG;
    }
}
