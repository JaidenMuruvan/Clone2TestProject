using UnityEngine;
using UnityEngine.EventSystems;

public class StockIngredients : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject draggableIngredient;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Spawn clone
        GameObject clone = Instantiate(draggableIngredient, transform.position, Quaternion.identity, canvas.transform);
    }
}
