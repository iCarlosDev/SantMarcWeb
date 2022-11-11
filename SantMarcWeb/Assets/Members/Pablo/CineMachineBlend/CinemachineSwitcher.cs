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
    public GameObject Player;
    
    public GameObject SnakeCanvas;
    public GameObject NormalCanvas;


    public void ActivarPCModelaje()
    {
        StartCoroutine("ActivarSnakeCanvas");
        PCModelaje1Camera.Priority = 11;
        Player.GetComponent<PlayerController>().enabled = false;
    }
    
    private IEnumerator ActivarSnakeCanvas()
    {
        yield return new WaitForSeconds(2);
        SnakeCanvas.SetActive(true);
        NormalCanvas.SetActive(false);
    }
}
