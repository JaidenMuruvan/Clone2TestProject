using UnityEngine;
using UnityEngine.EventSystems;

public class StockIngredients : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject draggablePrefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Spawn a fresh draggable ingredient
        GameObject clone = Instantiate(draggablePrefab, transform.position, Quaternion.identity, canvas.transform);
    }
}
