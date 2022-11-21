using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    private RectTransform _rectTransform;

    public bool Preview;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        RandomziePosition();
    }

    private void RandomziePosition()
    {

        float x = Random.Range(5, -8);
        float y = Random.Range(4, -4);

        int Xdecimal = Random.Range(0, 2);
        int Ydecimal = Random.Range(0, 2);
        
        Debug.Log("Xdecimal = " + Xdecimal);
        Debug.Log("Ydecimal = " + Ydecimal);

        Mathf.Round(x);
        Mathf.Round(y);

        #region FoodPositionCalc

        if (y < -3 || y > 3)
        {
            
        }
        else
        {
            if (Ydecimal == 1)
            {
                if (y >= 0)
                {
                    Ydecimal = 50;
                }
                else
                {
                    Ydecimal = -50;
                }
            }
        }
        
        if (Xdecimal == 1)
        {
            if (x >= 0)
            {
                Xdecimal = 50;
            }
            else
            {
                Xdecimal = -50;
            }
            
        }

        #endregion
        
        _rectTransform.localPosition = new Vector3(x * 50 + Xdecimal, y * 50 + Ydecimal, 0.0f);
        
        Debug.Log("X = " + x);
        Debug.Log("Y = " + y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RandomziePosition();
        }
        if (other.CompareTag("Obstacle"))
        {
            RandomziePosition();
        }
    }
}
