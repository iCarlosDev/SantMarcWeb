using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu_Snake : MonoBehaviour
{
    public CinemachineSwitcher CMS;
    [SerializeField] private CheckPoint_Tarea _checkPointTarea_M;
    [SerializeField] private CheckPoint_Tarea _checkPointTarea_T;
    [SerializeField] private CheckPoint_Tarea _checkPointTarea_P;
    
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
    
    [Header("--- SNAKE ---")] [Space(10)] 
    public Snake SnakeMenu;
    public Snake SnakeRety;
    public Snake SnakeGamePlay;

    private void Awake()
    {
        CMS = FindObjectOfType<CinemachineSwitcher>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RetryMenuPanel.activeSelf)
        {
            ResetStars();
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
        Score = puntuacion;
        Cursor.visible = true;
        
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(true);
        
        if (RetryMenuPanel.activeSelf)
        {
            ResetStars();
        }
        
        if (puntuacion >= 0 && puntuacion <= 17)
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
        SnakeRety.ResetGame();
    }
    public void PressStartBttn()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        RetryMenuPanel.SetActive(false);
        SnakeMenu.ResetGame();
    }
    public void PressContinueBttn()
    {
        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(false);

        if (_checkPointTarea_M != null)
        {
            ModelajeScore = Score;
            CMS.GM.ModelajeScore = ModelajeScore;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCModelaje();
            _checkPointTarea_M.CompletarTarea(false);
        }
        else if (_checkPointTarea_T != null)
        {
            TexturizadoScore = Score;
            CMS.GM.TexturizadoScore = TexturizadoScore;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCTexturizado();
            _checkPointTarea_T.CompletarTarea(false);
        }
        else if (_checkPointTarea_P != null)
        {
            ProgramacionScore = Score;
            CMS.GM.ProgramacionScore = ProgramacionScore;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCProgramacion();
            _checkPointTarea_P.CompletarTarea(false);
        }
    }
}
