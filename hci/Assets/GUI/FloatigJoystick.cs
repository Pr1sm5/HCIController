using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ControllerEmulation;
using UnityEngine.UIElements;
[System.Serializable]
public enum JoystickDirection {Horizontal, Vertical, Both}
public class FloatigJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IJoystick
{
    public JoystickDirection JoystickDirection = JoystickDirection.Both;
    public RectTransform background;
    public RectTransform handle;
    [Range(0, 2f)] public float handleLimit = 1f;
    private Vector2 _input = Vector2.zero;
    public bool isDragging { get; set; }

    //Output
    public float Vertical
    {
        get { return _input.y; }
    }
    
    public float Horizontal
    {
        get { return _input.x; }
    }

    private Vector2 JoyPosition = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        OnDrag(eventData);
        JoyPosition = eventData.position;
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        isDragging = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JoyDirection = eventData.position - JoyPosition;
        _input = (JoyDirection.magnitude > background.sizeDelta.x / 2f)
            ? JoyDirection.normalized
            : JoyDirection / (background.sizeDelta.x / 2f);
        if (JoystickDirection == JoystickDirection.Horizontal)
            _input = new Vector2(_input.x, 0f);
        if (JoystickDirection == JoystickDirection.Vertical)
            _input = new Vector2(_input.y, 0f);
        handle.anchoredPosition = (_input * background.sizeDelta.x / 2f) * handleLimit;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        background.gameObject.SetActive(false);
        isDragging = false;
    }

    public Vector2 GetInput()
    {
        return _input;
    }
    
}
