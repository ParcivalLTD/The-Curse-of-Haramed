using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 offset;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position - offset;
        position /= canvas.scaleFactor;
        rectTransform.anchoredPosition = position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Do any additional code you want to run when the user releases the panel.
    }
}
