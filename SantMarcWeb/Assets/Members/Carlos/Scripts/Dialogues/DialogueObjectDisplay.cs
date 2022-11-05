using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueObjectDisplay : MonoBehaviour
{
    public DialogueObject dialogueObject;
    
    [SerializeField] private TextMeshProUGUI TMP_Name;
    public TextMeshProUGUI TMP_Description;
    
    [SerializeField] private Image Avatar_IMG;
    
    // Start is called before the first frame update
    void Start()
    {
        TMP_Name.text = dialogueObject.name_TXT;
        Avatar_IMG.sprite = dialogueObject.sprite_IMG;
    }
}
