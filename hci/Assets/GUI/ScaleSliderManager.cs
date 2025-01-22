using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScaleSliderManager : MonoBehaviour
{
    public TMP_Dropdown dropdownMenu; // Das TMP Dropdown-Menü
    public Slider scaleSlider; // Der Skalierungs-Slider
    public Text sliderValueText; // (Optional) Text zur Anzeige des aktuellen Werts
    public List<GameObject> targetObjects; // Liste aller Buttons oder Sticks

    private RectTransform currentTarget; // Das aktuell ausgewählte Zielobjekt

    void Start()
    {
        // Dropdown initialisieren
        PopulateDropdown();

        // Standardmäßig erstes Ziel auswählen
        if (targetObjects.Count > 0)
        {
            SetTarget(targetObjects[0]);
        }

        // Dropdown-Event abonnieren
        dropdownMenu.onValueChanged.AddListener(OnDropdownValueChanged);

        // Slider-Event abonnieren
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Fülle das Dropdown-Menü mit den Namen der Zielobjekte
    private void PopulateDropdown()
    {
        dropdownMenu.ClearOptions();

        List<string> options = new List<string>();
        foreach (GameObject obj in targetObjects)
        {
            options.Add(obj.name);
        }

        dropdownMenu.AddOptions(options);
        dropdownMenu.RefreshShownValue();
    }

    // Wird aufgerufen, wenn das Dropdown geändert wird
    public void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < targetObjects.Count)
        {
            SetTarget(targetObjects[index]);
        }
    }

    // Setzt das aktuelle Zielobjekt
    private void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            currentTarget = newTarget.GetComponent<RectTransform>();
            scaleSlider.value = currentTarget.localScale.x; // Slider auf aktuelle Skalierung setzen
        }
    }

    // Wird aufgerufen, wenn der Slider-Wert geändert wird
    public void OnSliderValueChanged(float newValue)
    {
        if (currentTarget != null)
        {
            currentTarget.localScale = Vector3.one * newValue;

            // Optional: Den aktuellen Wert im Text anzeigen
            if (sliderValueText != null)
            {
                sliderValueText.text = $"Scale: {newValue:F2}";
            }
        }
    }
}
