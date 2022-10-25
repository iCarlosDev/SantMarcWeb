using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    [SerializeField] private Transform Player;

    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private int z;
    
    
    void Update()
    {
        transform.LookAt(Player);
        transform.Rotate(x, y, z);
    }
}
