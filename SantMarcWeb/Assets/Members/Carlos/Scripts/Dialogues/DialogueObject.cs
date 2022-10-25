using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueObject : ScriptableObject
{
    public string name_TXT;
    public string description_TXT;

    public Sprite sprite_IMG;
}
