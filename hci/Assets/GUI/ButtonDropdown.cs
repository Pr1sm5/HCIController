using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameObjectManager : MonoBehaviour
{
    public TMP_Dropdown selectionDropdown;
    public List<GameObject> managedObjects;

    private void Start()
    {
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        List<string> options = new List<string>();
        
        // Füge einen leeren Standardeintrag hinzu
        options.Add("Buttons");
        
        // Füge die eigentlichen Objekte hinzu
        foreach (var obj in managedObjects)
        {
            if (obj != null)
            {
                options.Add(obj.name);
            }
        }

        selectionDropdown.ClearOptions();
        selectionDropdown.AddOptions(options);
        selectionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Ignoriere den Standardeintrag (Index 0)
        if (index > 0 && index <= managedObjects.Count)
        {
            GameObject selectedObject = managedObjects[index - 1];
            if (selectedObject != null)
            {
                selectedObject.SetActive(!selectedObject.activeSelf);
                Debug.Log($"{selectedObject.name} wurde {(selectedObject.activeSelf ? "aktiviert" : "deaktiviert")}");
            }
        }
        
        // Zurück zum Standardeintrag
        selectionDropdown.value = 0;
    }
}