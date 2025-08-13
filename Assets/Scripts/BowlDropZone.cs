using UnityEngine;
using UnityEngine.EventSystems;

public class BowlDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager; 

    public void OnDrop(PointerEventData eventData)
    {
        DragDrop ingredient = eventData.pointerDrag.GetComponent<DragDrop>();
        if (ingredient != null)
        {
            orderManager.AddIngredient(ingredient.category, ingredient.ingredientName);
            Debug.Log($"Added {ingredient.ingredientName} to bowl as {ingredient.category}");
        }
    }
}
