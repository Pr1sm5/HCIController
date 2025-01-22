using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle;
    public float maxDistance = 50f;
    public bool isLayoutMode = false;  // Neuer Parameter für Layout-Modus
    
    private Vector2 _inputVector = Vector2.zero;
    private Vector2 _initialPosition;
    public bool _isDragging = false;
    private RectTransform _baseTransform;
    private Canvas _canvas;

    private void Awake()
    {
        _baseTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _initialPosition = handle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        if (!isLayoutMode)
        {
            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        if (isLayoutMode)
        {
            // Im Layout-Modus bewege den gesamten Joystick
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                _canvas.worldCamera,
                out localPoint))
            {
                _baseTransform.anchoredPosition = localPoint;
            }
            return;
        }

        // Normaler Joystick-Modus
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _baseTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            pos = Vector2.ClampMagnitude(pos, maxDistance);
            handle.anchoredPosition = _initialPosition + pos;
            _inputVector = pos / maxDistance;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isLayoutMode)
        {
            handle.anchoredPosition = _initialPosition;
            _inputVector = Vector2.zero;
        }
        _isDragging = false;
    }

    public Vector2 GetInput()
    {
        return _inputVector;
    }

    public void SetLayoutMode(bool layoutMode)
    {
        isLayoutMode = layoutMode;
    }
}