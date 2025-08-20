using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowlButton : MonoBehaviour
{
    public GameObject blueBtn;
    public GameObject greenBtn;
    public GameObject pinkBtn;
    public GameObject characterOne;
    public GameObject characterTwo;
    public GameObject characterThree;
    public GameObject textBox;

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

    public void HideCharacter()
    {
        characterOne.SetActive(false);
        characterTwo.SetActive(false);
        characterThree.SetActive(false);   
        textBox.SetActive(false);
    }

    public void ShowCharacter()
    {
        characterOne.SetActive(true);
        characterTwo.SetActive(true);
        characterThree.SetActive(true);
        textBox.SetActive(true);
    }
}
