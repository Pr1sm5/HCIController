using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle; // Der innere Kreis des Joysticks
    public float maxDistance = 50f; // Maximale Bewegung des Sticks (Radius)
    
    private Vector2 _inputVector = Vector2.zero; // Speichert die Joystick-Input-Werte
    private Vector2 _initialPosition; // Die ursprüngliche Position des Handles
    private bool _isDragging = false; // Flag, um sicherzustellen, dass Eingaben korrekt verarbeitet werden

    private void Start()
    {
        _initialPosition = handle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Der Stick wird gedrückt, setze Dragging auf true
        _isDragging = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        Vector2 pos;
        // Berechne die Position des Fingers auf dem Joystick
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(), 
            eventData.position, 
            eventData.pressEventCamera, 
            out pos))
        {
            // Begrenze die Bewegung auf den Radius des Joysticks
            pos = Vector2.ClampMagnitude(pos, maxDistance);

            // Setze die Position des Handles
            handle.anchoredPosition = _initialPosition + pos;

            // Berechne den Input-Wert (normalisiert zwischen -1 und 1)
            _inputVector = pos / maxDistance;

            // Debug-Ausgabe für die Eingabewerte
            Debug.Log($"Joystick Input: {_inputVector}");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Stick zurücksetzen
        handle.anchoredPosition = _initialPosition;
        _inputVector = Vector2.zero;
        _isDragging = false;
    }

    public Vector2 GetInput()
    {
        return _inputVector;
    }
}
