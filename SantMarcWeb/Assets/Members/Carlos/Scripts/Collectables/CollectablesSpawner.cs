using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawner : MonoBehaviour
{
    //Variables
    //[SerializeField] private GameObject collectablePrefab_GO;
    [Header("--- SPAWNER ---")]
    [SerializeField] private GameObject spawnCollectables_GO;
    [SerializeField] private List<GameObject> _carSpawnCollectables_L;
    [SerializeField] private List<GameObject> _planeSpawnCollectables_L;

    public List<GameObject> CarSpawnCollectablesL => _carSpawnCollectables_L;
    public List<GameObject> PlaneSpawnCollectablesL => _planeSpawnCollectables_L;

    private void Awake()
    {
        spawnCollectables_GO = gameObject;
    }

    private void Start()
    {
        foreach (Transform child in spawnCollectables_GO.transform)
        {
            if (child.CompareTag("CarToken"))
            {
                _carSpawnCollectables_L.Add(child.gameObject);
            }
        }

        foreach (Transform child in spawnCollectables_GO.transform)
        {
            if (child.CompareTag("PlaneToken"))
            {
                _planeSpawnCollectables_L.Add(child.gameObject);
            }
        }
    }

    public void CollectablesCompleted()
    {
        if (_carSpawnCollectables_L.Count.Equals(0))
        {
            //LO Q SEA;
            Debug.Log("Todos los coleccionables de coche copmletados");
        }
        else if (_planeSpawnCollectables_L.Count.Equals(0))
        {
            //LO Q SEA;
            Debug.Log("Todos los coleccionables de avion copmletados");
        }
    }
    
    
}
