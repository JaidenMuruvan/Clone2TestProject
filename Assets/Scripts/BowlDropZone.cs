using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BowlDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager;
    public Text currentBowlText; 
    private List<GameObject> ingredientsInBowl = new List<GameObject>();

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDrop ingredient = eventData.pointerDrag.GetComponent<DragDrop>();

           
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;

            
            ingredientsInBowl.Add(eventData.pointerDrag);

            
            orderManager.AddIngredient(ingredient.category, ingredient.ingredientName);

            
            UpdateBowlDisplay();
        }
    }

    public void RemoveLastIngredient()
    {
        if (ingredientsInBowl.Count > 0)
        {
            GameObject lastIngredient = ingredientsInBowl[ingredientsInBowl.Count - 1];

            if (lastIngredient != null)
                Destroy(lastIngredient);

            ingredientsInBowl.RemoveAt(ingredientsInBowl.Count - 1);

            
            orderManager.playerBowl = new RamenOrder();
            foreach (var ingredientObj in ingredientsInBowl)
            {
                if (ingredientObj != null)
                {
                    DragDrop data = ingredientObj.GetComponent<DragDrop>();
                    if (data != null)
                        orderManager.AddIngredient(data.category, data.ingredientName);
                }
            }

            
            UpdateBowlDisplay();
        }
    }

    private void UpdateBowlDisplay()
    {
        string display = "Last added to tray:\n";
        foreach (var ingredientObj in ingredientsInBowl)
        {
            if (ingredientObj != null)
            {
                DragDrop data = ingredientObj.GetComponent<DragDrop>();
                if (data != null)
                    display += $"{data.ingredientName} ({data.category})\n";
            }
        }

        currentBowlText.text = display;
    }
}
