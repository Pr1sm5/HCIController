using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButtonHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas; // Referenz zur Canvas
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Speichert die Originalposition, falls nötig (optional)
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out var localPoint))
        {
            rectTransform.anchoredPosition = localPoint; // Setzt die neue Position
        }
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition; // Zurücksetzen
    }
}