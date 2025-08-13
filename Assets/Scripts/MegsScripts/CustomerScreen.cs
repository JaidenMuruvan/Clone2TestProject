using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerScreen : MonoBehaviour
{
    public GameObject CustomerCanvas;
    
    void Start()
    {
        CustomerCanvas.SetActive(true);
        
    }

    public void OKButton()
    {
        CustomerCanvas.SetActive(false);
    }
    public void DoneButton() 
    { 
        CustomerCanvas.SetActive(true); 
    }

   
}
