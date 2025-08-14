using System.Collections;
using System.Collections.Generic;
using Unity.Content;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]private Canvas canvas;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    
    private CanvasGroup canvasGroup;
    public string category; // "Bowl", "Noodle", "Protein", "Vegetable"
    public string ingredientName;

    private GameObject draggedInstance;
    public GameObject ingredientPrefab;
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        draggedInstance = Instantiate(ingredientPrefab, transform.position, Quaternion.identity, canvas.transform);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        Debug.Log("OnEndDrag");
        if (draggedInstance != null) 
        {
            Destroy(draggedInstance);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }


}
