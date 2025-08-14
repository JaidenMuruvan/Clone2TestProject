using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScreen : MonoBehaviour
{
    public GameObject ButtonOne;

    public GameObject Station;
    public GameObject Station2;

    public void OpenStation()
    {
        Station.SetActive(true);
    }
    public void CloseStation()
    {
        Station.SetActive(false);
    }

    public void NextStation()
    {
       Station2.SetActive(true);
        Station.SetActive(false);
    }
     public void ButtonDisable()
    {
        ButtonOne.SetActive(false);
    }
    public void ButtonEnable()
    {
        ButtonOne.SetActive(true);
    }




}
