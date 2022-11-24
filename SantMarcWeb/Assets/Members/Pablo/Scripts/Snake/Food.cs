using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Snake _snake;

    public bool Preview;

    private Vector3 Position;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _snake = FindObjectOfType<Snake>();
    }

    private void Start()
    {
        if (!Preview)
        {
            RandomziePosition();
        }
        else
        {
            Position = _rectTransform.localPosition;
            PreviewPosition();
        }
        
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

        foreach (RectTransform _snake in _snake._segments)
        {
            if (_rectTransform.localPosition == _snake.localPosition)
            {
                RandomziePosition();
            }
        }
    }

    private void PreviewPosition()
    {
        if (Position == new Vector3(-425.0f, 425.0f, 0.0f))
        {
            Position = new Vector3(-425.0f, -425.0f, 0.0f);
            _rectTransform.localPosition = Position;
        }
        else if (Position == new Vector3(-425.0f, -425.0f, 0.0f))
        {
            Position = new Vector3(425.0f, -425.0f, 0.0f);
            _rectTransform.localPosition = Position;
        }
        else if (Position == new Vector3(425.0f, -425.0f, 0.0f))
        {
            Position = new Vector3(425.0f, 425.0f, 0.0f);
            _rectTransform.localPosition = Position;
        }
        else if (Position == new Vector3(425.0f, 425.0f, 0.0f))
        {
            Position = new Vector3(-425.0f, 425.0f, 0.0f);
            _rectTransform.localPosition = Position;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Preview)
            {
                RandomziePosition();
            }
            else
            {
                PreviewPosition();
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            if (!Preview)
            {
                RandomziePosition();
            }
        }
    }
}
