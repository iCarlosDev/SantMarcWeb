using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private RectTransform _rectTransform;
    public GameObject SnakeCanvasParent;

    public List<RectTransform> _segments = new List<RectTransform>();
    public RectTransform segmentPrefab;

    public int initialSize = 4;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ResetGame();
    }

    void Update()
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
