using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject chickenUI;
    public GameObject vegUI;
    public GameObject eggUI;
    public GameObject canvas;
    public GameObject closeButton;
    private bool menuVisible = false;


    public void Start()
    {
        chickenUI.SetActive(false);
        vegUI.SetActive(false);
        eggUI.SetActive(false);
        closeButton.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menuVisible = !menuVisible;
            chickenUI.SetActive(menuVisible);
        }
    }

    public void openMenu()
    {
        chickenUI.SetActive(true);
        canvas.SetActive(false);
        closeButton?.SetActive(true);
    }
    //right button on the chicken menu 
    public void rightchickenMenu()
    {
        chickenUI.SetActive(false);
        vegUI.SetActive(true) ;
    }
    //left button on the chicken menu 
    public void leftChickenMenu()
    {
        chickenUI.SetActive(false);
        eggUI.SetActive(true);
    }
    public void leftVegMenu()
    {
        vegUI.SetActive(false); 
        chickenUI.SetActive(true );
    }
    public void rightVegMenu()
    {
        vegUI.SetActive(false);
        eggUI.SetActive(true);
    }
    public void rightEggMenu()
    {
        eggUI.SetActive(false) ;
        chickenUI.SetActive(true);
    }
    public void leftEggMenu()
    {
        eggUI.SetActive(false);
        vegUI.SetActive(true);
    }
    public void closeMenu()
    {
        chickenUI.SetActive(false);
        eggUI.SetActive(false);
        vegUI.SetActive(false);
        canvas.SetActive(true);
        closeButton?.SetActive(false);
    }
}
