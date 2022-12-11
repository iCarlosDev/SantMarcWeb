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
    public AudioManager AudioManager;
    
    [SerializeField] public CheckPoint_Tarea _checkPointTarea_M;
    [SerializeField] public CheckPoint_Tarea _checkPointTarea_T;
    [SerializeField] public CheckPoint_Tarea _checkPointTarea_P;
    
    [Header("--- MenuVariables ---")]
    [SerializeField]private int Score = 0;
    [SerializeField]public int ModelajeStasAmount = 0;
    [SerializeField]public int TexturizadoStasAmount = 0;
    [SerializeField]public int ProgramacionStasAmount = 0;
    
    public Image Star1;
    public Image Star2;
    public Image Star3;
    
    public GameObject MainMenuPanel;
    public GameObject GamePanel;
    public GameObject RetryMenuPanel;
    
    public GameObject Bttns1;
    public GameObject Bttns2;
    public GameObject Bttns3;
    
    public GameObject TitleText_M;
    public GameObject TitleText_T;
    public GameObject TitleText_P;
    
    public GameObject R_TitleText_M;
    public GameObject R_TitleText_T;
    public GameObject R_TitleText_P;
    
    [Header("--- SNAKE ---")] [Space(10)] 
    public Snake SnakeMenu;
    public Snake SnakeRety;
    public Snake SnakeGamePlay;

    private void Awake()
    {
        CMS = FindObjectOfType<CinemachineSwitcher>();
        AudioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RetryMenuPanel.activeSelf)
        {
            ResetStars();
        }
        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(false);
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
    public void RetryMenu(int puntuacion)
    {
        Cursor.visible = true;
        
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(true);
        
        if (RetryMenuPanel.activeSelf)
        {
            ResetStars();
        }
        
        if (puntuacion >= 9 && puntuacion <= 17)
        {
            Score = 1;
            var star1Color = Star1.color;
            star1Color.a = 255f;
            Star1.color = star1Color;
        }
        else if (puntuacion >= 18 && puntuacion <= 26)
        {
            Score = 2;
            var star1Color = Star1.color;
            star1Color.a = 255f;
            Star1.color = star1Color;
            
            var star2Color = Star2.color;
            star2Color.a = 255f;
            Star2.color = star2Color;
        }
        else if (puntuacion == 27)
        {
            Score = 3;
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
        
        if (puntuacion < 9)
        {
            Bttns1.SetActive(false);
            Bttns2.SetActive(true);
            Bttns3.SetActive(false);
        }
        else if (puntuacion >= 9 && puntuacion < 27)
        {
            Bttns1.SetActive(true);
            Bttns2.SetActive(false);
            Bttns3.SetActive(false);
        }
        else if (puntuacion == 27)
        {
            Bttns1.SetActive(false);
            Bttns2.SetActive(false);
            Bttns3.SetActive(true);
        }
    }
    public void PressRetryBttn()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        RetryMenuPanel.SetActive(false);
        SnakeRety.ResetGame();
        AudioManager.Play("SnakeClick");
    }
    public void PressStartBttn()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        RetryMenuPanel.SetActive(false);
        SnakeMenu.ResetGame();
        AudioManager.Play("SnakeClick");
    }
    public void PressContinueBttn()
    {
        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        RetryMenuPanel.SetActive(false);
        
        SnakeMenu.EnProcesoImagesActive = 0;
        SnakeRety.EnProcesoImagesActive = 0;
        SnakeGamePlay.EnProcesoImagesActive = 0;
        Debug.Log("HE ENTRADO EN CONTINUE");

        if (_checkPointTarea_M != null)
        {
            ModelajeStasAmount = Score;
            CMS.AudioManager.ModelajeStarsAmount = ModelajeStasAmount;
            Score = 0;
            Debug.Log("HE ENTRADO EN CheckPoint");
            ResetStars();
            CMS.DesactivarPCModelaje();
            _checkPointTarea_M.CompletarTarea(false);
            Debug.Log("HE SALIDOOOOO");
            TitleText_M.SetActive(false);
            R_TitleText_M.SetActive(false);
            
            TitleText_T.SetActive(true);
            R_TitleText_T.SetActive(true);
        }
        else if (_checkPointTarea_T != null)
        {
            TexturizadoStasAmount = Score;
            CMS.AudioManager.TexturizadoStarsAmount = TexturizadoStasAmount;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCTexturizado();
            _checkPointTarea_T.CompletarTarea(false);
            TitleText_T.SetActive(false);
            R_TitleText_T.SetActive(false);
            
            TitleText_P.SetActive(true);
            R_TitleText_P.SetActive(true);
        }
        else if (_checkPointTarea_P != null)
        {
            ProgramacionStasAmount = Score;
            CMS.AudioManager.ProgramacionStarsAmount = ProgramacionStasAmount;
            Score = 0;
            ResetStars();
            CMS.DesactivarPCProgramacion();
            _checkPointTarea_P.CompletarTarea(false);
        }
        AudioManager.Play("SnakeClick");
    }
}
