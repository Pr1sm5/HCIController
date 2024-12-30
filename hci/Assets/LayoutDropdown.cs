using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LayoutDropdown : MonoBehaviour
{
    public TMP_Dropdown layoutDropdown;
    public LayoutManager layoutManager;

    private void Start()
    {
        PopulateDropdown();
    }

    public void PopulateDropdown()
    {
        layoutDropdown.ClearOptions();
        List<string> options = new List<string>();
    
        // Füge "Standard" einmal am Anfang hinzu, wenn gewünscht
        options.Add("Standard"); // Beachten Sie auch die korrekte Schreibweise
    
        // Füge dann die gespeicherten Layouts hinzu
        foreach (ButtonLayout layout in layoutManager.savedLayouts)
        {
            options.Add(layout.layoutName);
        }

        layoutDropdown.AddOptions(options);
    }

    public void OnDropdownValueChanged(int index)
    {
        string selectedLayoutName = layoutDropdown.options[index].text;
        layoutManager.LoadLayout(selectedLayoutName);
    }
}