using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public bool Preview;
    public float timeRemaining = 5;
    
    private Vector2 _direction = Vector2.right;
    private RectTransform _rectTransform;
    public GameObject SnakeCanvasParent;

    public List<RectTransform> _segments = new List<RectTransform>();
    public RectTransform segmentPrefab;

    public TextMeshProUGUI ScoreTMP;
    private int Score = 0;
    public Image[] QuarterImages;
    private int QuaretersActive = 0;
    public Image[] FinalImage;
    private int FinalImageActive = 0;

    public float fixedDeltaTime;

    public int initialSize = 4;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Time.fixedDeltaTime = fixedDeltaTime;
    }

    private void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (!Preview)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_direction != Vector2.down)
                {
                    _direction = Vector2.up;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (_direction != Vector2.up)
                {
                    _direction = Vector2.down;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (_direction != Vector2.right)
                {
                    _direction = Vector2.left;
                }
            
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (_direction != Vector2.left)
                {
                    _direction = Vector2.right;
                }
            }
        }
        else
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 10;
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
        Score++;
        ScoreTMP.text = "SCORE : " + Score;

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
    
    private void ResetGame()
    {
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

        this._rectTransform.localPosition = Vector3.zero;
        
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
            ResetGame();
        }
    }
}
