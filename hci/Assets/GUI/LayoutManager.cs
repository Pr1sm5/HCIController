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
}

[System.Serializable]
public class StickData
{
    public string stickName;
    public Vector2 position;
}

public class LayoutManager : MonoBehaviour
{
    public List<ButtonLayout> savedLayouts = new List<ButtonLayout>();
    public List<GameObject> layoutButtons; // Buttons im Layout-Panel
    public List<GameObject> controllerButtons; // Buttons im Controller-Panel
    public List<GameObject> sticks; // Sticks im Layout
    private string layoutPath;

    void Awake()
    {
        layoutPath = Path.Combine(Application.persistentDataPath, "layouts.json");
        Debug.Log($"Layout-Dateipfad: {layoutPath}");
        LoadLayoutsFromFile();
    }

    public void SaveCurrentLayout(string layoutName)
    {
        Debug.Log("=== SAVE LAYOUT START ===");
        Debug.Log($"Layout-Name zum Speichern: {layoutName}");

        // Überprüfe auf existierendes Layout
        int existingLayoutIndex = savedLayouts.FindIndex(l => l.layoutName == layoutName);
        ButtonLayout newLayout = new ButtonLayout { layoutName = layoutName };

        Debug.Log($"Anzahl der Layout-Buttons: {layoutButtons.Count}");

        // Überprüfe, ob die Liste korrekt initialisiert ist
        if (layoutButtons == null || layoutButtons.Count == 0)
        {
            Debug.LogError("Layout-Buttons Liste ist leer oder null!");
            return;
        }

        // Speichere die Button-Daten
        foreach (GameObject layoutButton in layoutButtons)
        {
            if (layoutButton == null)
            {
                Debug.LogError("Ein Button in der Layout-Buttons Liste ist null!");
                continue;
            }

            RectTransform rectTransform = layoutButton.GetComponent<RectTransform>();
            Vector2 currentPos = rectTransform.anchoredPosition;

            ButtonData buttonData = new ButtonData
            {
                buttonName = layoutButton.name,
                position = currentPos
            };

            newLayout.buttons.Add(buttonData);
            Debug.Log($"Speichere Button '{layoutButton.name}' an Position: {currentPos}");
        }

        // Update oder füge neues Layout hinzu
        if (existingLayoutIndex >= 0)
        {
            savedLayouts[existingLayoutIndex] = newLayout;
            Debug.Log($"Layout '{layoutName}' aktualisiert mit {newLayout.buttons.Count} Buttons");
        }
        else
        {
            savedLayouts.Add(newLayout);
            Debug.Log($"Neues Layout '{layoutName}' erstellt mit {newLayout.buttons.Count} Buttons");
        }

        SaveLayoutsToFile();
        Debug.Log("=== SAVE LAYOUT END ===");
    }

    public void LoadLayout(string layoutName)
    {
        Debug.Log("=== LOAD LAYOUT START ===");
        Debug.Log($"Versuche Layout zu laden: {layoutName}");

        // Ausgabe aller verfügbaren Controller-Buttons
        Debug.Log($"controllerButtons enthält {controllerButtons.Count} Elemente:");
        foreach (GameObject button in controllerButtons)
        {
            Debug.Log($"Controller-Button: {button?.name}");
        }

        // Suche das Layout
        ButtonLayout layoutToLoad = savedLayouts.Find(layout => layout.layoutName == layoutName);
        if (layoutToLoad == null)
        {
            Debug.LogError($"Layout '{layoutName}' nicht gefunden!");
            return;
        }

        Debug.Log($"Layout gefunden mit {layoutToLoad.buttons.Count} Buttons");

        // Bearbeite nur die Controller-Buttons
        foreach (ButtonData buttonData in layoutToLoad.buttons)
        {
            Debug.Log($"Versuche Controller-Button zuzuordnen: {buttonData.buttonName}");

            GameObject controllerButton = controllerButtons.Find(b => b.name == buttonData.buttonName);
            if (controllerButton != null)
            {
                RectTransform rectTransform = controllerButton.GetComponent<RectTransform>();
                Vector2 oldPos = rectTransform.anchoredPosition;
                rectTransform.anchoredPosition = buttonData.position;
                Debug.Log($"Controller-Button '{buttonData.buttonName}' Position: {oldPos} -> {buttonData.position}");
            }
            else
            {
                Debug.LogError($"Controller-Button '{buttonData.buttonName}' nicht in controllerButtons gefunden!");
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
            File.WriteAllText(layoutPath, json);
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
        if (File.Exists(layoutPath))
        {
            string json = File.ReadAllText(layoutPath);
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
                new ButtonData { buttonName = "A", position = new Vector2(670, -154) },
                new ButtonData { buttonName = "B", position = new Vector2(831, 13) },
                new ButtonData { buttonName = "X", position = new Vector2(509, 13) },
                new ButtonData { buttonName = "Y", position = new Vector2(670, 174) },
                new ButtonData { buttonName = "Start", position = new Vector2(41, 201) },
                new ButtonData { buttonName = "Back", position = new Vector2(-215, 201) },
                new ButtonData { buttonName = "LB", position = new Vector2(-580, 302) },
                new ButtonData { buttonName = "RB", position = new Vector2(420, 284) }, 
                new ButtonData { buttonName = "Settings", position = new Vector2(-71, 90) }
            },
            sticks = new List<StickData>
            {
                new StickData { stickName = "Left-Stick", position = new Vector2(-650, -89) },
                new StickData { stickName = "Right-Stick", position = new Vector2(268, -201) }  
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