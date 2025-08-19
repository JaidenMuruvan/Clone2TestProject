using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlVisuals : MonoBehaviour
{
    [System.Serializable]
    public class IngredientVisual
    {
        public string ingredientName;
        public GameObject ingredientImage;
    }

    public List<IngredientVisual> ingredientVisuals = new List<IngredientVisual>();

    public void ShowIngredient(string ingredientName)
    {
        foreach (var iv in ingredientVisuals)
        {
            if (iv.ingredientName == ingredientName)
            {
                iv.ingredientImage.SetActive(true);
                Debug.Log($"Activated image for {ingredientName}");
            }
        }
    }

    public void ClearBowl()
    {
        foreach( var iv in ingredientVisuals)
        {
            iv.ingredientImage.SetActive(false);
        }
    }
}
