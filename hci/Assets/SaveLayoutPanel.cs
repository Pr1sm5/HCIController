using System.IO;
using UnityEngine;
using TMPro;

public class SaveLayoutPanel : MonoBehaviour
{
    public GameObject savePanel; // Panel für das Speichern
    public TMP_InputField layoutNameInput; // TMP InputField für den Layout-Namen
    public LayoutManager layoutManager; // Verweis auf den LayoutManager

    // Öffnet das Save-Panel
    public void OpenSavePanel()
    {
        if (savePanel == null)
        {
            Debug.LogError("SavePanel ist null! Bitte prüfen, ob es im Inspektor zugewiesen wurde.");
            return;
        }
        if (layoutNameInput == null)
        {
            Debug.LogError("InputField ist null! Überprüfe die Zuweisung im Inspektor.");
            return;
        }
        savePanel.SetActive(true); // Zeigt das Save-Panel an
        layoutNameInput.text = ""; // Leert das Input-Feld
    }

    // Speichert das Layout mit dem eingegebenen Namen
    public void SaveLayout()
    {
        string layoutName = layoutNameInput.text; // Holt den Text aus dem TMP InputField

        if (string.IsNullOrEmpty(layoutName))
        {
            Debug.LogWarning("Layout-Name darf nicht leer sein.");
        }

        layoutManager.SaveCurrentLayout(layoutName); // Speichert das Layout über den Layout-Manager
        SaveLayoutsToFile();
        savePanel.SetActive(false); // Schließt das Save-Panel
    }
    public void SaveLayoutsToFile()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/layouts.json", json);
        Debug.Log("Layouts gespeichert!");
    }
}