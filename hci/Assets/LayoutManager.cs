using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ButtonLayout
{
    public string layoutName; // Name des Layouts
    public List<ButtonData> buttons = new List<ButtonData>(); // Liste der Buttons und deren Daten
}

[System.Serializable]
public class ButtonData
{
    public string buttonName; // Name des Buttons (z. B. "A", "B", "Start")
    public Vector2 position; // Position des Buttons
}

public class LayoutManager : MonoBehaviour
{
    public List<ButtonLayout> savedLayouts = new List<ButtonLayout>(); // Liste der gespeicherten Layouts
    public List<GameObject> buttons; // Referenzen zu den Buttons im UI
    public void Start()
    {
        LoadLayoutsFromFile();
    }

    public void SaveCurrentLayout(string layoutName)
    {
        ButtonLayout newLayout = new ButtonLayout { layoutName = layoutName };

        foreach (GameObject button in buttons)
        {
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            ButtonData buttonData = new ButtonData
            {
                buttonName = button.name,
                position = rectTransform.anchoredPosition
            };
            newLayout.buttons.Add(buttonData);
        }

        savedLayouts.Add(newLayout);
        Debug.Log($"Layout '{layoutName}' gespeichert!");
    }
    public void LoadLayout(string layoutName)
    {
        ButtonLayout layoutToLoad = savedLayouts.Find(layout => layout.layoutName == layoutName);

        if (layoutToLoad == null)
        {
            Debug.LogError($"Layout '{layoutName}' nicht gefunden!");
            return;
        }

        foreach (ButtonData buttonData in layoutToLoad.buttons)
        {
            GameObject button = buttons.Find(b => b.name == buttonData.buttonName);
            if (button != null)
            {
                RectTransform rectTransform = button.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = buttonData.position;
            }
        }

        Debug.Log($"Layout '{layoutName}' geladen!");
    }
    
    
    public void LoadLayoutsFromFile()
    {
        string path = Application.persistentDataPath + "/layouts.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log("Layouts geladen!");
        }
        else
        {
            Debug.LogWarning("Keine gespeicherten Layouts gefunden.");
        }
    }

}