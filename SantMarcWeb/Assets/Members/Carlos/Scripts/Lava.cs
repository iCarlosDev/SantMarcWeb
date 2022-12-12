using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //Variables
    [Header("--- POINTS ---")] 
    [SerializeField] private GameObject characters;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private bool canTp;

    private void Awake()
    {
        characters = GameObject.Find("PlayerCharacters");
    }

    private void Update()
    {
        GameObject playerPosition = GameManager.instance.Player;

        float Point1Dist = Vector3.Distance(playerPosition.transform.position, point1.position);
        float Point2Dist = Vector3.Distance(playerPosition.transform.position, point2.position);

        if (canTp)
        {
            Debug.Log($"Distancia 1: {Point1Dist} \n Distancia 2: {Point2Dist}");
            
            if (Point1Dist < Point2Dist)
            {
               characters.SetActive(false);
               playerPosition.transform.position = point1.position;
               characters.SetActive(true);
               GameManager.instance.PlayerActive();
               canTp = false;
               FindObjectOfType<SpawnDetecter>().CanTransform = true;
            }
            else
            {
                characters.SetActive(false);
                playerPosition.transform.position = point2.position;
                characters.SetActive(true);
                GameManager.instance.PlayerActive();
                canTp = false;
                FindObjectOfType<SpawnDetecter>().CanTransform = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Plane") || other.CompareTag("Car"))
        {
            canTp = true;
        }
    }
}
