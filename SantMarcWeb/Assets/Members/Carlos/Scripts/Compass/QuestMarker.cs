using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Members.Carlos.Scripts.Compass
{
    public class QuestMarker : MonoBehaviour
    {
        //Variables
        [SerializeField] private Sprite icon;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI distance_TMP;

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
        
        public TextMeshProUGUI DistanceTMP
        {
            get => distance_TMP;
            set => distance_TMP = value;
        }
    }
}
