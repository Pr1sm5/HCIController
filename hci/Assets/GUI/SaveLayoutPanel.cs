using UnityEngine;
using TMPro;

public class SaveLayoutPanel : MonoBehaviour
{
    public GameObject savePanel;
    public TMP_InputField layoutNameInput;
    public LayoutManager layoutManager;
    public LayoutDropdown layoutDropdown; // Neue Referenz

    public void OpenSavePanel()
    {
        if (savePanel != null)
        {
            savePanel.SetActive(true);
            layoutNameInput.text = "";
        }
    }

    public void SaveLayout()
    {
        string layoutName = layoutNameInput.text;

        if (string.IsNullOrEmpty(layoutName))
        {
            Debug.LogWarning("Layout-Name darf nicht leer sein.");
            return;
        }

        layoutManager.SaveCurrentLayout(layoutName);
        
        // Aktualisiere das Dropdown-Menü
        if (layoutDropdown != null)
        {
            layoutDropdown.PopulateDropdown();
        }
        
        savePanel.SetActive(false);
    }
}