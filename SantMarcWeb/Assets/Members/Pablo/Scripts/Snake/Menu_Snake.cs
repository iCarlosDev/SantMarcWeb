using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu_Snake : MonoBehaviour
{
    private CinemachineSwitcher CMS;
    
    [Header("--- MenuVariables ---")]
    [SerializeField]private int Score = 0;
    [SerializeField]private int ModelajeScore = 0;
    [SerializeField]private int TexturizadoScore = 0;
    [SerializeField]private int ProgramacionScore = 0;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    public Image Star4;
    public GameObject MainMenuPanel;
    public GameObject GamePanel;
    public GameObject RetryMenuPanel;

    [Header("--- PRUEBA ---")] [Space(10)] 
    public bool Modelaje;
    public bool Texturizado;
    public bool Programación;


    private void Awake()
    {
        CMS = FindObjectOfType<CinemachineSwitcher>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetStars();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var randomScore = Random.Range(0, 36);
            RetryMenu(randomScore);
            Debug.Log(""+ randomScore);
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
        
        var star4Color = Star4.color;
        star4Color.a = 0f;
        Star4.color = star4Color;
    }
    public void RetryMenu(int puntuacion)
    {
        ResetStars();
        Score = puntuacion;
        
        if (puntuacion >= 9 && puntuacion <= 17)
        {
            var star1Color = Star1.color;
            star1Color.a = 255f;
            Star1.color = star1Color;
        }
        else if (puntuacion >= 18 && puntuacion <= 26)
        {
            var star1Color = Star1.color;
            star1Color.a = 255f;
            Star1.color = star1Color;
            
            var star2Color = Star2.color;
            star2Color.a = 255f;
            Star2.color = star2Color;
        }
        else if (puntuacion >= 27 && puntuacion <= 35)
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
        else if (puntuacion > 35)
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
            
            var star4Color = Star4.color;
            star4Color.a = 255f;
            Star4.color = star4Color;
        }
    }

    public void PressRetryBttn()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        RetryMenuPanel.SetActive(false);
    }
    
    public void PressContinueBttn()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(false);

        if (Modelaje)
        {
            ModelajeScore = Score;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCModelaje();
        }
    }
    
}
