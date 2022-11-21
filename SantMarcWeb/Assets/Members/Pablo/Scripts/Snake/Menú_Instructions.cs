using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Menú_Instructions : MonoBehaviour
{
    
    private int Score = 0;
    public Image[] QuarterImages;
    private int QuaretersActive = 0;
    public Image[] FinalImage;
    private int FinalImageActive = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowPreview");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator ShowPreview()
    {
        yield return new WaitForSeconds(2f);
    }
}
