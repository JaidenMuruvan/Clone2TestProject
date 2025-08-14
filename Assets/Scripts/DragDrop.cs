using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public GameObject ingredientPrefab;
    public string ingredientName;
    public string category;
    private GameObject draggedInstance;
    private RectTransform draggedRectTransform;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Spawn a clone when dragging starts
        draggedInstance = Instantiate(ingredientPrefab, transform.position, Quaternion.identity, canvas.transform);
        draggedRectTransform = draggedInstance.GetComponent<RectTransform>();

        // Make the clone semi-transparent
        CanvasGroup cloneCanvasGroup = draggedInstance.GetComponent<CanvasGroup>();
        if (cloneCanvasGroup != null)
        {
            cloneCanvasGroup.alpha = 0.6f;
            cloneCanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedRectTransform != null)
        {
            draggedRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedInstance != null)
        {
            // Check if it's over a valid drop zone
            if (!eventData.pointerEnter || !eventData.pointerEnter.CompareTag("Bowl"))
            {
                Destroy(draggedInstance); // Destroy if not dropped in bowl
            }
            else
            {
                // If dropped in bowl, make it solid again
                CanvasGroup cloneCanvasGroup = draggedInstance.GetComponent<CanvasGroup>();
                if (cloneCanvasGroup != null)
                {
                    cloneCanvasGroup.alpha = 1f;
                    cloneCanvasGroup.blocksRaycasts = true;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) { }
}
