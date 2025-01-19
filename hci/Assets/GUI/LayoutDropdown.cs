using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LayoutDropdown : MonoBehaviour
{
    public TMP_Dropdown layoutDropdown;
    public LayoutManager layoutManager;

    private void Start()
    {
        Debug.Log("Dropdown wird initialisiert...");
        if (layoutManager.savedLayouts == null || layoutManager.savedLayouts.Count == 0)
        {
            Debug.LogError("Keine Layouts in LayoutManager verfügbar!");
            return;
        }

        PopulateDropdown();
        
        // Direktes Registrieren der Methode, die aufgerufen wird, wenn der Wert des Dropdowns geändert wird
        layoutDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    public void PopulateDropdown()
    {
        // Alle Optionen löschen
        layoutDropdown.ClearOptions();
        List<string> options = new List<string>();

        // Layout-Namen hinzufügen
        foreach (ButtonLayout layout in layoutManager.savedLayouts)
        {
            options.Add(layout.layoutName);
        }

        layoutDropdown.AddOptions(options);

        // Standard-Layout als Default auswählen
        layoutDropdown.value = 0;
        layoutDropdown.RefreshShownValue();
    }

    public void OnDropdownValueChanged(int index)
    {
        // Index wird automatisch durch Unity übergeben
        string selectedLayoutName = layoutDropdown.options[index].text;
        Debug.Log($"Layout ausgewählt: {selectedLayoutName}");
        layoutManager.LoadLayout(selectedLayoutName);
    }
}