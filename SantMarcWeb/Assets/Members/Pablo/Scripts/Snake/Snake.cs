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
    public float fixedDeltaTimeDecrease;
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
    private int Score = 0;
    private int TempScore = 0;
    
    public Image Star1;
    public Image Star2;
    public Image Star3;
    
    public Image[] EnProcesoImages;
    public Image[] EnProcesoImages_T_Pocho;
    public Image[] EnProcesoImages_T_Mid;
    public Image[] EnProcesoImages_T_Top;
    public Image[] EnProcesoImages_P;
    public int EnProcesoImagesActive = 0;
    public Image[] FinalImage;
    public Image[] FinalImage_T_Pocho;
    public Image[] FinalImage_T_Mid;
    public Image[] FinalImage_T_Top;
    public Image[] FinalImage_P;
    private int FinalImageActive = 0;
    private Vector3 Position;
    private bool canChangeDirection;

    private Menu_Snake _menuSnake;
    private AudioManager audioManager;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Time.fixedDeltaTime = fixedDeltaTime;
        _menuSnake = FindObjectOfType<Menu_Snake>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        ResetGame();
        actualizarPreview(Vector2.left);
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
    
    public void ResetStars()
    {
        var star1Color = Star1.color;
        star1Color.a = 0f;
        Star1.color = star1Color;
        
        var star2Color = Star2.color;
        star2Color.a = 0f;
        Star2.color = star2Color;
        
        var star3Color = Star3.color;
        star3Color.a = 0f;
        Star3.color = star3Color;
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
            if (Score >= 9 && Score <= 17)
            {
                var star1Color = Star1.color;
                star1Color.a = 255f;
                Star1.color = star1Color;
            }
            else if (Score >= 18 && Score <= 26)
            {
                var star1Color = Star1.color;
                star1Color.a = 255f;
                Star1.color = star1Color;
            
                var star2Color = Star2.color;
                star2Color.a = 255f;
                Star2.color = star2Color;
            }
            else if (Score == 27)
            {
                var star1Color = Star1.color;
                star1Color.a = 255f;
                Star1.color = star1Color;
            
                var star2Color = Star2.color;
                star2Color.a = 255f;
                Star2.color = star2Color;

                var star3Color = Star3.color;
                star3Color.a = 255f;
                Star3.color = star3Color;
            }
        }

        if (_menuSnake._checkPointTarea_M != null)
        {
            if (EnProcesoImagesActive < 26)
            {
                EnProcesoImagesActive++;
                for (int i = 0; i < EnProcesoImagesActive; i++)
                {
                    EnProcesoImages[i].enabled = true;
                }

                if (EnProcesoImagesActive == 9 || EnProcesoImagesActive == 18 || EnProcesoImagesActive == 27)
                {
                    //Cuarto Completado
                    FinalImageActive++;
                    fixedDeltaTime -= fixedDeltaTimeDecrease;
                    Time.fixedDeltaTime = fixedDeltaTime;
                
                    for (int i = 0; i < EnProcesoImagesActive; i++)
                    {
                        EnProcesoImages[i].color = new Color(EnProcesoImages[i].color.r, EnProcesoImages[i].color.g, EnProcesoImages[i].color.b, 0.6f);
                        Debug.Log("" + EnProcesoImages[i].color.a);
                    }
                
                    for (int i = 0; i < FinalImageActive; i++)
                    {
                        FinalImage[i].enabled = true;
                    }
                    audioManager.Play("SnakeStar");
                }
            }
            else
            {
                if (Preview)
                {
                    ResetGame();
                }
                else
                {
                    ResetGame();
                    _menuSnake.RetryMenu(TempScore);
                    TempScore = 0;
                }
                //WIN
            
            }
        }
        else if (_menuSnake._checkPointTarea_T != null)
        {
            Debug.Log("ENTRO EN _checkPointTarea_T != NULL");
            if (_menuSnake.ModelajeStasAmount == 1)
            {
                Debug.Log("ENTRO EN _menuSnake.ModelajeStasAmount == 1");
                if (EnProcesoImagesActive < 26)
                {
                    EnProcesoImagesActive++;
                    for (int i = 0; i < EnProcesoImagesActive; i++)
                    {
                        EnProcesoImages_T_Pocho[i].enabled = true;
                    }

                    if (EnProcesoImagesActive == 9 || EnProcesoImagesActive == 18 || EnProcesoImagesActive == 27)
                    {
                        //Cuarto Completado
                        FinalImageActive++;
                        fixedDeltaTime -= fixedDeltaTimeDecrease;
                        Time.fixedDeltaTime = fixedDeltaTime;
                
                        for (int i = 0; i < EnProcesoImagesActive; i++)
                        {
                            EnProcesoImages_T_Pocho[i].color = new Color(EnProcesoImages_T_Pocho[i].color.r, EnProcesoImages_T_Pocho[i].color.g, EnProcesoImages_T_Pocho[i].color.b, 0.6f);
                        }
                
                        for (int i = 0; i < FinalImageActive; i++)
                        {
                            FinalImage_T_Pocho[i].enabled = true;
                        }
                        audioManager.Play("SnakeStar");
                    }
                }
                else
                {
                    if (Preview)
                    {
                        ResetGame();
                    }
                    else
                    {
                        //WIN
                        ResetGame();
                        _menuSnake.RetryMenu(TempScore);
                        TempScore = 0;
                    }
                }
            }
            else if (_menuSnake.ModelajeStasAmount == 2)
            {
                if (EnProcesoImagesActive < 26)
                {
                    EnProcesoImagesActive++;
                    for (int i = 0; i < EnProcesoImagesActive; i++)
                    {
                        EnProcesoImages_T_Mid[i].enabled = true;
                    }

                    if (EnProcesoImagesActive == 9 || EnProcesoImagesActive == 18 || EnProcesoImagesActive == 27)
                    {
                        //Cuarto Completado
                        FinalImageActive++;
                        fixedDeltaTime -= fixedDeltaTimeDecrease;
                        Time.fixedDeltaTime = fixedDeltaTime;
                
                        for (int i = 0; i < EnProcesoImagesActive; i++)
                        {
                            EnProcesoImages_T_Mid[i].color = new Color(EnProcesoImages_T_Mid[i].color.r, EnProcesoImages_T_Mid[i].color.g, EnProcesoImages_T_Mid[i].color.b, 0.6f);
                        }
                
                        for (int i = 0; i < FinalImageActive; i++)
                        {
                            FinalImage_T_Mid[i].enabled = true;
                        }
                        audioManager.Play("SnakeStar");
                    }
                }
                else
                {
                    if (Preview)
                    {
                        ResetGame();
                    }
                    else
                    {
                        //WIN
                        ResetGame();
                        _menuSnake.RetryMenu(TempScore);
                        TempScore = 0;
                    }
                }
            }
            else if (_menuSnake.ModelajeStasAmount == 3)
            {
                if (EnProcesoImagesActive < 26)
                {
                    EnProcesoImagesActive++;
                    for (int i = 0; i < EnProcesoImagesActive; i++)
                    {
                        EnProcesoImages_T_Top[i].enabled = true;
                    }

                    if (EnProcesoImagesActive == 9 || EnProcesoImagesActive == 18 || EnProcesoImagesActive == 27)
                    {
                        //Cuarto Completado
                        FinalImageActive++;
                        fixedDeltaTime -= fixedDeltaTimeDecrease;
                        Time.fixedDeltaTime = fixedDeltaTime;
                
                        for (int i = 0; i < EnProcesoImagesActive; i++)
                        {
                            EnProcesoImages_T_Top[i].color = new Color(EnProcesoImages_T_Top[i].color.r, EnProcesoImages_T_Top[i].color.g, EnProcesoImages_T_Top[i].color.b, 0.6f);
                        }
                
                        for (int i = 0; i < FinalImageActive; i++)
                        {
                            FinalImage_T_Top[i].enabled = true;
                        }
                        audioManager.Play("SnakeStar");
                    }
                }
                else
                {
                    if (Preview)
                    {
                        ResetGame();
                    }
                    else
                    {
                        //WIN
                        ResetGame();
                        _menuSnake.RetryMenu(TempScore);
                        TempScore = 0;
                        
                    }
                }
            }
        }
        else if (_menuSnake._checkPointTarea_P != null)
        {
            if (EnProcesoImagesActive < 26)
            {
                EnProcesoImagesActive++;
                for (int i = 0; i < EnProcesoImagesActive; i++)
                {
                    EnProcesoImages_P[i].enabled = true;
                }

                if (EnProcesoImagesActive == 9 || EnProcesoImagesActive == 18 || EnProcesoImagesActive == 27)
                {
                    //Cuarto Completado
                    FinalImageActive++;
                    fixedDeltaTime -= fixedDeltaTimeDecrease;
                    Time.fixedDeltaTime = fixedDeltaTime;
            
                    for (int i = 0; i < EnProcesoImagesActive; i++)
                    {
                        EnProcesoImages_P[i].color = new Color(EnProcesoImages_P[i].color.r, EnProcesoImages_P[i].color.g, EnProcesoImages_P[i].color.b, 0.6f);
                    }
            
                    for (int i = 0; i < FinalImageActive; i++)
                    {
                        FinalImage_P[i].enabled = true;
                    }
                    audioManager.Play("SnakeStar");
                }
            }
            else
            {
                if (Preview)
                {
                    ResetGame();
                }
                else
                {
                    //WIN
                    ResetGame();
                    _menuSnake.RetryMenu(TempScore);
                    TempScore = 0;
                }
            }
        }
        audioManager.Play("SnakeGrow");
    }
    
    public void ResetGame()
    {
        if (!Preview)
        {
            this._rectTransform.localPosition = Vector3.zero;
            ResetStars();
            TempScore = Score;
        }
        
        for (int i = 0; i < EnProcesoImagesActive; i++)
        {
            EnProcesoImages[i].color = new Color(EnProcesoImages[i].color.r, EnProcesoImages[i].color.g, EnProcesoImages[i].color.b, 1f);
            EnProcesoImages_P[i].color = new Color(EnProcesoImages_P[i].color.r, EnProcesoImages_P[i].color.g, EnProcesoImages_P[i].color.b, 1f);
            EnProcesoImages_T_Pocho[i].color = new Color(EnProcesoImages_T_Pocho[i].color.r, EnProcesoImages_T_Pocho[i].color.g, EnProcesoImages_T_Pocho[i].color.b, 1f);
            EnProcesoImages_T_Mid[i].color = new Color(EnProcesoImages_T_Mid[i].color.r, EnProcesoImages_T_Mid[i].color.g, EnProcesoImages_T_Mid[i].color.b, 1f);
            EnProcesoImages_T_Top[i].color = new Color(EnProcesoImages_T_Top[i].color.r, EnProcesoImages_T_Top[i].color.g, EnProcesoImages_T_Top[i].color.b, 1f);
            Debug.Log("" + EnProcesoImages[i].color.a);
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
        
        for (int i = 0; i < EnProcesoImages.Length; i++)
        {
            EnProcesoImages[i].enabled = false;
            EnProcesoImages_P[i].enabled = false;
            EnProcesoImages_T_Pocho[i].enabled = false;
            EnProcesoImages_T_Mid[i].enabled = false;
            EnProcesoImages_T_Top[i].enabled = false;
        }
        for (int i = 0; i < FinalImage.Length; i++)
        {
            FinalImage[i].enabled = false;
            FinalImage_P[i].enabled = false;
            FinalImage_T_Pocho[i].enabled = false;
            FinalImage_T_Mid[i].enabled = false;
            FinalImage_T_Top[i].enabled = false;
        }
        
        Score = 0;
        FinalImageActive = 0;
        EnProcesoImagesActive = 0;
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
            if (!Preview)
            {
                audioManager.Play("SnakeDie");
                _menuSnake.RetryMenu(TempScore);
                TempScore = 0;
            }
        }
    }
}
