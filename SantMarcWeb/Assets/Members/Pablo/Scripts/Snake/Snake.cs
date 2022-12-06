using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [Header("--- PREVIEW ---")]
    public bool Preview;
    public int stepCounter = 0;

    [Header("--- STATS ---")]
    [Space(10)]
    public float fixedDeltaTime;
    public float fixedDeltaTimeAgument;
    public int initialSize = 4;
    
    [Header("--- VARIABLES ---")]
    [Space(10)]
    private Vector2 _direction = Vector2.left;
    private RectTransform _rectTransform;
    public GameObject SnakeCanvasParent;
    public List<RectTransform> _segments = new List<RectTransform>();
    public RectTransform segmentPrefab;

    [Header("--- SCORE ---")]
    [Space(10)]
    public TextMeshProUGUI ScoreTMP;
    private int Score = 0;
    public Image[] QuarterImages;
    private int QuaretersActive = 0;
    public Image[] FinalImage;
    private int FinalImageActive = 0;
    private Vector3 Position;
    private bool canChangeDirection;

    private Menu_Snake _menuSnake;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Time.fixedDeltaTime = fixedDeltaTime;
        _menuSnake = FindObjectOfType<Menu_Snake>();
    }

    private void Start()
    {
        ResetGame();
        actualizarPreview(Vector2.down);
        Debug.Log("STARTED");
    }

    private void Update()
    {
        if (!Preview)
        {
            if (canChangeDirection)
            {
                ChangeDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count -1; i > 0; i--)
        {
            _segments[i].localPosition = _segments[i - 1].localPosition;
        }
        
        _rectTransform.localPosition = new Vector3(
            Mathf.Round(_rectTransform.localPosition.x)  + _direction.x * 50,
            Mathf.Round(_rectTransform.localPosition.y) + _direction.y * 50,
            0.0f);

        if (Preview)
        {
            stepCounter++;
        }
        canChangeDirection = true;
        
        if (stepCounter == 17)
        {
            if (Preview)
            {
                stepCounter = 0;
                if (_direction == Vector2.down)
                {
                    actualizarPreview(Vector2.right);
                }
                else if (_direction == Vector2.right)
                {
                    actualizarPreview(Vector2.up);
                }
                else if (_direction == Vector2.up)
                {
                    actualizarPreview(Vector2.left);
                }
                else if (_direction == Vector2.left)
                {
                    actualizarPreview(Vector2.down);
                }
            }
        }
    }

    private void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_direction != Vector2.down)
            {
                _direction = Vector2.up;
                canChangeDirection = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_direction != Vector2.up)
            {
                _direction = Vector2.down;
                canChangeDirection = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_direction != Vector2.right)
            {
                _direction = Vector2.left;
                canChangeDirection = false;
            }
        
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_direction != Vector2.left)
            {
                _direction = Vector2.right;
                canChangeDirection = false;
            }
        }
    }

    private void actualizarPreview(Vector2 direccion)
    {
        _direction = direccion;
        Debug.Log(_direction);
    }
    
    private void Grow()
    {
        RectTransform segment = Instantiate(segmentPrefab, SnakeCanvasParent.transform);
        segment.localPosition = _segments[_segments.Count - 1].localPosition;
        
        _segments.Add(segment);

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (!Preview)
        {
            Score++;
            ScoreTMP.text = "SCORE : " + Score;
        }
        
        if (QuaretersActive < 3)
        {
            QuaretersActive++;
            for (int i = 0; i < QuaretersActive; i++)
            {
                QuarterImages[i].enabled = true;
            }
        }
        else
        {
            //Cuarto Completado
            QuaretersActive = 0;
            FinalImageActive++;
            fixedDeltaTime -= 0.005f;
            Time.fixedDeltaTime = fixedDeltaTime;
            
            for (int i = 0; i < 4; i++)
            {
                QuarterImages[i].enabled = false;
            }
            
            for (int i = 0; i < FinalImageActive; i++)
            {
                FinalImage[i].enabled = true;
            }
        }
        
    }
    
    public void ResetGame()
    {
        if (!Preview)
        {
            this._rectTransform.localPosition = Vector3.zero;
        }

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        
        _segments.Clear();
        _segments.Add(this._rectTransform);
        
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab, SnakeCanvasParent.transform));
        }
        
        for (int i = 0; i < 4; i++)
        {
            QuarterImages[i].enabled = false;
        }
        for (int i = 0; i < 9; i++)
        {
            FinalImage[i].enabled = false;
        }
        
        Score = 0;
        ScoreTMP.text = "SCORE : " + Score;
        FinalImageActive = 0;
        QuaretersActive = 0;
        fixedDeltaTime = 0.1f;
        Time.fixedDeltaTime = fixedDeltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }
        
        if (other.CompareTag("Obstacle"))
        {
            if (!Preview)
            {
                _menuSnake.RetryMenu(Score);
                Debug.Log("HAS PERDIDO FR:" + Score);
            }
            ResetGame();
        }
    }
}
