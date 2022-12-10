using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Members.Carlos.Scripts;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{

    public CinemachineFreeLook ThirdPersonCamera;
    public CinemachineFreeLook PCModelaje1Camera;
    public CinemachineFreeLook PCTexturizado1Camera;
    public CinemachineFreeLook PCProgramacion1Camera;
    public GameManager GM;
    public AudioManager AudioManager;
    
    public GameObject SnakeCanvas;
    public GameObject NormalCanvas;


    private void Awake()
    {
        AudioManager = FindObjectOfType<AudioManager>();
    }

    public void ActivarPCModelaje()
    {
        GM.isDialogue = true;
        GM.snakeOpen = true;
        ThirdPersonCamera.m_XAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_YAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_XAxis.m_InputAxisValue = 0;
        ThirdPersonCamera.m_YAxis.m_InputAxisValue = 0;
        PCModelaje1Camera.Priority = 11;
        StartCoroutine("ActivarSnakeCanvas");
    }
    
    public void DesactivarPCModelaje()
    {
        StartCoroutine("DesactivarSnakeCanvas");
        PCModelaje1Camera.Priority = 1;
        SnakeCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GM.isDialogue = false;
    }
    
    public void ActivarPCTexturizado()
    {
        GM.isDialogue = true;
        GM.snakeOpen = true;
        ThirdPersonCamera.m_XAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_YAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_XAxis.m_InputAxisValue = 0;
        ThirdPersonCamera.m_YAxis.m_InputAxisValue = 0;
        PCTexturizado1Camera.Priority = 11;
        StartCoroutine("ActivarSnakeCanvas");
    }
    
    public void DesactivarPCTexturizado()
    {
        StartCoroutine("DesactivarSnakeCanvas");
        PCTexturizado1Camera.Priority = 1;        
        SnakeCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GM.isDialogue = false;
    }
    
    public void ActivarPCProgramacion()
    {
        GM.isDialogue = true;
        GM.snakeOpen = true;
        ThirdPersonCamera.m_XAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_YAxis.m_InputAxisName = string.Empty;
        ThirdPersonCamera.m_XAxis.m_InputAxisValue = 0;
        ThirdPersonCamera.m_YAxis.m_InputAxisValue = 0;
        PCProgramacion1Camera.Priority = 11;
        StartCoroutine("ActivarSnakeCanvas");
    }
    
    public void DesactivarPCProgramacion()
    {
        StartCoroutine("DesactivarSnakeCanvas");
        PCProgramacion1Camera.Priority = 1;
        SnakeCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GM.isDialogue = false;
    }
    
    private IEnumerator ActivarSnakeCanvas()
    {
        yield return new WaitForSeconds(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SnakeCanvas.SetActive(true);
        NormalCanvas.SetActive(false);
    }
    
    private IEnumerator DesactivarSnakeCanvas()
    {
        yield return new WaitForSeconds(2);
        GM.isDialogue = false;
        GM.snakeOpen = false;
        ThirdPersonCamera.m_XAxis.m_InputAxisName = "Mouse X";
        ThirdPersonCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        ThirdPersonCamera.m_XAxis.m_InputAxisValue = 0;
        ThirdPersonCamera.m_YAxis.m_InputAxisValue = 0;
    }
}
