using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuUI; 
    private bool menuVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menuVisible = !menuVisible;
            menuUI.SetActive(menuVisible);
        }
    }
}
