using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
[System.Serializable]
public class DialogueObject : ScriptableObject
{
    public string name_TXT;
    public string[] sentence_TXT;

    public Sprite sprite_IMG;
}
