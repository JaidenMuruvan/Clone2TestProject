using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowlButton : MonoBehaviour
{
    public GameObject blueBtn;
    public GameObject greenBtn;
    public GameObject pinkBtn;

    public void ShowBtn()
    {
        blueBtn.SetActive(true);
        greenBtn.SetActive(true);
        pinkBtn.SetActive(true);

    }

    public void HideBtn()
    {
        blueBtn.SetActive(false);
        greenBtn.SetActive(false);
        pinkBtn.SetActive(false);
    }
}
