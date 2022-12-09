using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectablesSpawner : MonoBehaviour
{
    //Variables
    //[SerializeField] private GameObject collectablePrefab_GO;
    [Header("--- SPAWNER ---")]
    [SerializeField] private GameObject spawnCollectables_GO;
    [SerializeField] private List<GameObject> _spawnCollectables_L;

    public List<GameObject> SpawnCollectablesL => _spawnCollectables_L;

    private void Awake()
    {
        spawnCollectables_GO = gameObject;
    }

    private void Start()
    {
        foreach (Transform child in spawnCollectables_GO.transform)
        {
            _spawnCollectables_L.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (_spawnCollectables_L != null)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                foreach (var coins in _spawnCollectables_L)
                {
                    coins.GetComponent<Outline>().enabled = true;
                }
            }
            else
            {
                foreach (var coins in _spawnCollectables_L)
                {
                    coins.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }

    public void CollectablesCompleted()
    {
        if (_spawnCollectables_L.Count.Equals(0))
        {
            //LO Q SEA;
            Debug.Log("Todos los coleccionables copmletados");
            TaskManager.instance.CoinsCollected = true;
            TaskManager.instance.TasksBoolCheck();
            FadeIn_FadeOut_Manager.instance.StartCoroutine(FadeIn_FadeOut_Manager.instance.WaitToChangeFinalLevel());
        }
    }
    
    
}
