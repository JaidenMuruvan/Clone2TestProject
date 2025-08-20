using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlSelector : MonoBehaviour
{
    public GameObject bowlGreen;
    public GameObject bowlBlue;
    public GameObject bowlPink;

    public OrderManager orderManager;

    private GameObject activeBowl;

    public void SelectBowl(string bowlName)
    {
        bowlGreen.SetActive(false);
        bowlBlue.SetActive(false);
        bowlPink.SetActive(false);

        switch (bowlName)
        {
            case "Green Bowl": activeBowl = bowlGreen; break;
            case "Blue Bowl": activeBowl = bowlBlue; break;
            case "Pink Bowl": activeBowl= bowlPink; break;
        }

        if (activeBowl != null)
        {
            activeBowl.SetActive(true);
            orderManager.AddIngredient("Bowl",bowlName);
            Debug.Log($"Bowl selected: {bowlName}");
        }
    }
}
