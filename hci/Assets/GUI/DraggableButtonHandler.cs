using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButtonHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Vector2 canvasScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        canvasScale = new Vector2(canvas.scaleFactor, canvas.scaleFactor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Optional: Hier könnte man eine Snap-Funktion oder Validierung implementieren
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
}