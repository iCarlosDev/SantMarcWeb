using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{

	public RectTransform DescripcionTarea_RectTransform;

	public bool descIsShowed = true;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    if (descIsShowed)
		    {
			    DescripcionTarea_RectTransform.anchoredPosition = new Vector3(0, 350, 0);
			    descIsShowed = !descIsShowed;
		    }
		    else
		    {
			    DescripcionTarea_RectTransform.anchoredPosition = new Vector3(0, 535, 0);
			    descIsShowed = !descIsShowed;
		    }
	    }
    }
    
    
}
