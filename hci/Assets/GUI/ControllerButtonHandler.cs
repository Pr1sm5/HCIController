using UnityEngine;

public class ControllerButtonHandler : MonoBehaviour
{
    private RectTransform rectTransform;
    private UniversalButtonHandler buttonHandler;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonHandler = FindAnyObjectByType<UniversalButtonHandler>();
    }


    // Methode zum Aktualisieren der Position basierend auf dem Layout
    public void UpdatePosition(Vector2 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
    }
}