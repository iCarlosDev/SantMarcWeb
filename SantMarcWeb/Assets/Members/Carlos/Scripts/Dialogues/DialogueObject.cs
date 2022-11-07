using UnityEngine;

namespace Members.Carlos.Scripts.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
    [System.Serializable]
    public class DialogueObject : ScriptableObject
    {
        public string nameTxt;
        public string[] sentenceTxt;

        public Sprite spriteImg;
    }
}
