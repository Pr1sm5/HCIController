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
    public bool isActive;
}

[System.Serializable]
public class StickData
{
    public string stickName;
    public Vector2 position;
    public float scale;
    public bool isActive;
}

public class LayoutManager : MonoBehaviour
{
    public List<ButtonLayout> savedLayouts = new List<ButtonLayout>();
    public List<GameObject> layoutButtons;
    public List<GameObject> controllerButtons;
    public List<GameObject> layoutSticks;
    public List<GameObject> controllerSticks;
    private string _layoutPath;

    void Awake()
    {
        _layoutPath = Path.Combine(Application.persistentDataPath, "layouts.json");
        LoadLayoutsFromFile();
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
            GameObject controllerButton = controllerButtons.Find(b => b.name == buttonData.buttonName);
            if (controllerButton != null)
            {
                RectTransform rectTransform = controllerButton.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = buttonData.position;
                rectTransform.localScale = Vector3.one * buttonData.scale;
                controllerButton.SetActive(buttonData.isActive);
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
                stick.SetActive(stickData.isActive);
            }
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
            }
            else
            {
                CreateDefaultLayout();
                SaveLayoutsToFile();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Fehler beim Laden der Layouts: {e.Message}");
            CreateDefaultLayout();
        }
    }

    public void SaveCurrentLayout(string layoutName)
    {
        ButtonLayout newLayout = new ButtonLayout { layoutName = layoutName };

        foreach (GameObject layoutButton in layoutButtons)
        {
            if (layoutButton == null) continue;
            RectTransform rectTransform = layoutButton.GetComponent<RectTransform>();
            ButtonData buttonData = new ButtonData
            {
                buttonName = layoutButton.name,
                position = rectTransform.anchoredPosition,
                scale = rectTransform.localScale.x,
                isActive = layoutButton.activeSelf
            };
            newLayout.buttons.Add(buttonData);
        }

        foreach (GameObject stick in layoutSticks)
        {
            if (stick == null) continue;
            RectTransform rectTransform = stick.GetComponent<RectTransform>();
            StickData stickData = new StickData
            {
                stickName = stick.name,
                position = rectTransform.anchoredPosition,
                scale = rectTransform.localScale.x,
                isActive = stick.activeSelf
            };
            newLayout.sticks.Add(stickData);
        }

        int existingLayoutIndex = savedLayouts.FindIndex(l => l.layoutName == layoutName);
        if (existingLayoutIndex >= 0)
        {
            savedLayouts[existingLayoutIndex] = newLayout;
        }
        else
        {
            savedLayouts.Add(newLayout);
        }
        SaveLayoutsToFile();
    }

    private void CreateDefaultLayout()
    {
        ButtonLayout defaultLayout = new ButtonLayout
        {
            layoutName = "Standard",
            buttons = new List<ButtonData>
            {
                new ButtonData { buttonName = "A", position = new Vector2(670, -154), scale = 0.4f, isActive = true },
                new ButtonData { buttonName = "B", position = new Vector2(831, 13), scale = 0.4f, isActive = true },
                new ButtonData { buttonName = "X", position = new Vector2(509, 13), scale = 0.4f, isActive = true },
                new ButtonData { buttonName = "Y", position = new Vector2(670, 174), scale = 0.4f, isActive = true }
            },
            sticks = new List<StickData>
            {
                new StickData { stickName = "FloatingLeft", position = new Vector2(-580, -208), scale = 1f, isActive = true },
                new StickData { stickName = "FloatingRight", position = new Vector2(292, -222), scale = 1f, isActive = true }
            }
        };
        savedLayouts.Clear();
        savedLayouts.Add(defaultLayout);
    }

    private void SaveLayoutsToFile()
    {
        try
        {
            SerializableLayoutList listToSave = new SerializableLayoutList { layouts = savedLayouts };
            string json = JsonUtility.ToJson(listToSave, true);
            File.WriteAllText(_layoutPath, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Fehler beim Speichern der Layouts: {e.Message}");
        }
    }
}

[System.Serializable]
public class SerializableLayoutList
{
    public List<ButtonLayout> layouts = new List<ButtonLayout>();
}
