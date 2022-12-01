using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Members.Carlos.Scripts;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{

    public CinemachineFreeLook ThirdPersonCamera;
    public CinemachineVirtualCamera PCModelaje1Camera;
    public CinemachineVirtualCamera PCTexturizado1Camera;
    public CinemachineVirtualCamera PCProgramacion1Camera;
    public GameManager GM;
    
    public GameObject SnakeCanvas;
    public GameObject NormalCanvas;


    public void ActivarPCModelaje()
    {
        StartCoroutine("ActivarSnakeCanvas");
        PCModelaje1Camera.Priority = 11;
        GM.isDialogue = true;
    }
    
    public void DesactivarPCModelaje()
    {
        StartCoroutine("DesactivarSnakeCanvas");
        PCModelaje1Camera.Priority = 1;
    }
    
    private IEnumerator ActivarSnakeCanvas()
    {
        yield return new WaitForSeconds(2);
        SnakeCanvas.SetActive(true);
        NormalCanvas.SetActive(false);
    }
    
    private IEnumerator DesactivarSnakeCanvas()
    {
        yield return new WaitForSeconds(2);
        SnakeCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
        GM.isDialogue = false;
    }
}
