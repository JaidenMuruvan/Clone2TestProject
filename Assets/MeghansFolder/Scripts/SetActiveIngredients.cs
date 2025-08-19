using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveIngredients : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject items;

    public void Activate()
    {
        items.SetActive(true);
    }
    public void Deactivate()
    {
        items.SetActive(false);
    }
}
