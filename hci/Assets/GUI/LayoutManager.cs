using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ButtonLayout
{
    public string layoutName;
    public List<ButtonData> buttons = new List<ButtonData>();
    public List<StickData> sticks = new List<StickData>();
}

[System.Serializable]
public class ButtonData
{
    public string buttonName;
    public Vector2 position;
    public float scale;
    public bool isActive; // Aktivierungsstatus
}

[System.Serializable]
public class StickData
{
    public string stickName;
    public Vector2 position;
    public float scale;
    public bool isActive; // Aktivierungsstatus
}

public class LayoutManager : MonoBehaviour
{
    public List<ButtonLayout> savedLayouts = new List<ButtonLayout>();
    public List<GameObject> layoutButtons; // Buttons im Layout-Panel
    public List<GameObject> controllerButtons; // Buttons im Controller-Panel
    public List<GameObject> layoutSticks; // Sticks im Layout
    public List<GameObject> controllerSticks; // Sticks im Layout
    private string _layoutPath;

    void Awake()
    {
        _layoutPath = Path.Combine(Application.persistentDataPath, "layouts.json");
        Debug.Log($"Layout-Dateipfad: {_layoutPath}");
        LoadLayoutsFromFile();
    }

    public void SaveCurrentLayout(string layoutName)
    {
        Debug.Log("=== SAVE LAYOUT START ===");
        Debug.Log($"Layout-Name zum Speichern: {layoutName}");

        int existingLayoutIndex = savedLayouts.FindIndex(l => l.layoutName == layoutName);
        ButtonLayout newLayout = new ButtonLayout { layoutName = layoutName };

        foreach (GameObject layoutButton in layoutButtons)
        {
            if (layoutButton == null) continue;

            RectTransform rectTransform = layoutButton.GetComponent<RectTransform>();
            Vector2 currentPos = rectTransform.anchoredPosition;
            float currentScale = rectTransform.localScale.x;

            ButtonData buttonData = new ButtonData
            {
                buttonName = layoutButton.name,
                position = currentPos,
                scale = currentScale,
                isActive = layoutButton.activeSelf // Aktivierungsstatus speichern
            };

            newLayout.buttons.Add(buttonData);
        }

        foreach (GameObject stick in layoutSticks)
        {
            if (stick == null) continue;

            RectTransform rectTransform = stick.GetComponent<RectTransform>();
            Vector2 currentPos = rectTransform.anchoredPosition;
            float currentScale = rectTransform.localScale.x;

            StickData stickData = new StickData
            {
                stickName = stick.name,
                position = currentPos,
                scale = currentScale,
                isActive = stick.activeSelf // Aktivierungsstatus speichern
            };

            newLayout.sticks.Add(stickData);
        }

        if (existingLayoutIndex >= 0)
        {
            savedLayouts[existingLayoutIndex] = newLayout;
        }
        else
        {
            savedLayouts.Add(newLayout);
        }

        SaveLayoutsToFile();
        Debug.Log("=== SAVE LAYOUT END ===");
    }

    public void LoadLayout(string layoutName)
    {
        Debug.Log("=== LOAD LAYOUT START ===");
        Debug.Log($"Versuche Layout zu laden: {layoutName}");

        ButtonLayout layoutToLoad = savedLayouts.Find(layout => layout.layoutName == layoutName);
        if (layoutToLoad == null)
        {
            Debug.LogError($"Layout '{layoutName}' nicht gefunden!");
            return;
        }

        foreach (ButtonData buttonData in layoutToLoad.buttons)
        {
            GameObject controllerButton = controllerButtons.Find(b => b.name == buttonData.buttonName);
            if (controllerButton != null)
            {
                RectTransform rectTransform = controllerButton.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = buttonData.position;
                rectTransform.localScale = Vector3.one * buttonData.scale;
                controllerButton.SetActive(buttonData.isActive); // Aktivierungsstatus laden
            }
        }

        foreach (StickData stickData in layoutToLoad.sticks)
        {
            GameObject stick = controllerSticks.Find(s => s.name == stickData.stickName);
            if (stick != null)
            {
                RectTransform rectTransform = stick.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = stickData.position;
                rectTransform.localScale = Vector3.one * stickData.scale;
                stick.SetActive(stickData.isActive); // Aktivierungsstatus laden
            }
        }

        Debug.Log("=== LOAD LAYOUT END ===");
    }
    private void SaveLayoutsToFile()
    {
        try
        {
            SerializableLayoutList listToSave = new SerializableLayoutList { layouts = savedLayouts };
            string json = JsonUtility.ToJson(listToSave, true);
            File.WriteAllText(_layoutPath, json);
            Debug.Log($"Layouts erfolgreich gespeichert: {json}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Fehler beim Speichern der Layouts: {e.Message}");
        }
    }

    private void LoadLayoutsFromFile()
{
    try
    {
        if (File.Exists(_layoutPath))
        {
            string json = File.ReadAllText(_layoutPath);
            SerializableLayoutList loadedLayouts = JsonUtility.FromJson<SerializableLayoutList>(json);
            savedLayouts = loadedLayouts.layouts;
            Debug.Log($"Layouts erfolgreich geladen: {json}");
        }
        else
        {
            Debug.LogWarning("Keine Layout-Datei gefunden. Erstelle Standard-Layout.");
            CreateDefaultLayout();
            SaveLayoutsToFile();
        }
    }
    catch (System.Exception e)
    {
        Debug.LogError($"Fehler beim Laden der Layouts: {e.Message}");
    }
}

    private void CreateDefaultLayout()
    {
        ButtonLayout defaultLayout = new ButtonLayout
        {
            layoutName = "Standard",
            buttons = new List<ButtonData>
            {
                new ButtonData { buttonName = "A", position = new Vector2(670, -154), scale = 0.4f },
                new ButtonData { buttonName = "B", position = new Vector2(831, 13), scale = 0.4f },
                new ButtonData { buttonName = "X", position = new Vector2(509, 13), scale = 0.4f },
                new ButtonData { buttonName = "Y", position = new Vector2(670, 174), scale = 0.4f },
                new ButtonData { buttonName = "Start", position = new Vector2(41, 201), scale = 0.25f },
                new ButtonData { buttonName = "Back", position = new Vector2(-215, 201), scale = 0.25f },
                new ButtonData { buttonName = "LB", position = new Vector2(-580, 302), scale = 0.5f },
                new ButtonData { buttonName = "RB", position = new Vector2(420, 284), scale = 0.5f }, 
                new ButtonData { buttonName = "Settings", position = new Vector2(-71, 90), scale = 0.3f }
            },
            sticks = new List<StickData>
            {
                new StickData { stickName = "Left-Stick", position = new Vector2(-650, -89), scale = 0.75f },
                new StickData { stickName = "Right-Stick", position = new Vector2(268, -201), scale = 0.75f }  
            }
        };


        savedLayouts.Add(defaultLayout);
        Debug.Log("Standard-Layout erstellt.");
    }
    
}

[System.Serializable]
public class SerializableLayoutList
{
    public List<ButtonLayout> layouts = new List<ButtonLayout>();
}