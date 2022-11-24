using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Members.Carlos.Scripts.Compass
{
    public class QuestMarker : MonoBehaviour
    {
        //Variables
        [SerializeField] private Sprite icon;
        [SerializeField] private Image image;

        public Vector2 position
        {
            get { return new Vector2(transform.position.x, transform.position.z); }
        }

        public Sprite Icon
        {
            get => icon;
            set => icon = value;
        }
        
        public Image Image
        {
            get => image;
            set => image = value;
        }
    }
}
