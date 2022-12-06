using System;
using System.Collections;
using System.Collections.Generic;
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

    public void CollectablesCompleted()
    {
        if (_spawnCollectables_L.Count.Equals(0))
        {
            //LO Q SEA;
            Debug.Log("Todos los coleccionables copmletados");
            SceneManager.LoadScene(2);
        }
    }
    
    
}
