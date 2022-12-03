using System;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Members.Carlos.Scripts.Compass
{
    public class Compass : MonoBehaviour
    {
        //Variables
        public static Compass instance;
        
        [SerializeField] private GameObject iconPrefab;
        [SerializeField] private List<QuestMarker> questMarkers = new List<QuestMarker>();
        
        [SerializeField] private RawImage compassImage;
        [SerializeField] private Transform camera;
        [SerializeField] private Transform player;

        [SerializeField] private float compassUnit;

        [Header("---- QUEST MARKERS ----")]
        [Space(10)]
        [SerializeField] private QuestMarker door1;
        [SerializeField] private QuestMarker door2;
        [SerializeField] private QuestMarker teacher;
        [SerializeField] private QuestMarker modeling;
        [SerializeField] private QuestMarker texture;
        [SerializeField] private QuestMarker programming;
        [SerializeField] private QuestMarker playVR;
        [SerializeField] private QuestMarker Exit;

        public List<QuestMarker> QuestMarkers
        {
            get => questMarkers;
            set => questMarkers = value;
        }

        public QuestMarker Teacher => teacher;
        public QuestMarker Modeling => modeling;
        public QuestMarker Texture => texture;
        public QuestMarker Programming => programming;
        public QuestMarker PlayVR => playVR;


        private void Awake()
        {
            instance = this;
            player = FindObjectOfType<PlayerController>().gameObject.transform;
            camera = FindObjectOfType<CinemachineBrain>().gameObject.transform;
        }

        private void Start()
        {
            compassUnit = compassImage.rectTransform.rect.width / 360f;
            

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                AddQuestMarker(door1);
                AddQuestMarker(door2);
            }
            else
            {
                AddQuestMarker(Exit);
            }
        }

        private void Update()
        {
            compassImage.uvRect = new Rect(camera.localEulerAngles.y / 360f, 0f, 1f, 1f);
            
            foreach (QuestMarker marker in questMarkers)
            {
                var distanceVector = marker.gameObject.transform.position - player.position;
                var distance = distanceVector.sqrMagnitude;

                marker.Image.rectTransform.anchoredPosition = getPosOnCompass(marker);
                marker.DistanceTMP.text = (int)distance + " m";
            }
            
        }

        public void AddQuestMarker(QuestMarker marker)
        {
            GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
            marker.Image = newMarker.GetComponent<Image>();
            marker.Image.sprite = marker.Icon;
            marker.DistanceTMP = newMarker.GetComponentInChildren<TextMeshProUGUI>();

            marker.gameObject.AddComponent<Outline>();
            marker.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
            marker.gameObject.GetComponent<Outline>().OutlineWidth = 2;
            
            questMarkers.Add(marker);
        }

        Vector2 getPosOnCompass(QuestMarker marker)
        {
            Vector2 playerPos = new Vector2(camera.transform.position.x, camera.transform.position.z);
            Vector2 playerFwd = new Vector2(camera.transform.forward.x, camera.transform.forward.z);

            float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

            return new Vector2(compassUnit * angle, 0f);
        }
    }
}
