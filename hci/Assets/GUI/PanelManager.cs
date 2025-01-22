using UnityEngine;
using UnityEngine.EventSystems;

public class PanelManager : MonoBehaviour
{
    public GameObject LayoutSettingsPanel;
    public GameObject ControllerPanelLayout;
    public GameObject currentOpenPanel;

    private void Start()
    {
        if (ControllerPanelLayout != null)
        {
            EventTrigger trigger = ControllerPanelLayout.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = ControllerPanelLayout.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnControllerPanelClicked(); });
            trigger.triggers.Add(entry);
        }
    }

    private void OnControllerPanelClicked()
    {
        if (LayoutSettingsPanel != null && LayoutSettingsPanel.activeSelf)
        {
            LayoutSettingsPanel.SetActive(false);
        }
    }

    public void OpenPanelByName(string panelName)
    {
        Transform panelTransform = FindInActiveObjectByName(panelName);

        if (panelTransform != null)
        {
            GameObject newPanel = panelTransform.gameObject;

            // Spezielle Behandlung für LayoutSettingsPanel
            if (panelName == "LayoutSettingsPanel")
            {
                // Aktiviere einfach das LayoutSettingsPanel, ohne andere Panels zu beeinflussen
                newPanel.SetActive(true);
                Debug.Log($"LayoutSettingsPanel wurde geöffnet.");
                return; // Früher Return, um die restliche Logik zu überspringen
            }

            // Normale Behandlung für alle anderen Panels
            foreach (GameObject panel in GameObject.FindGameObjectsWithTag("Panel"))
            {
                if (panel != newPanel && panel.name != "LayoutSettingsPanel")
                {
                    panel.SetActive(false);
                }
            }

            newPanel.SetActive(true);
            Debug.Log($"Panel '{panelName}' wurde geöffnet.");
            currentOpenPanel = newPanel;
        }
        else
        {
            Debug.LogError($"Kein Panel mit dem Namen '{panelName}' gefunden.");
        }
    }

    private Transform FindInActiveObjectByName(string name)
    {
        Transform[] allObjects = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform obj in allObjects)
        {
            if (obj.name == name && obj.hideFlags == HideFlags.None)
            {
                return obj;
            }
        }
        return null;
    }
}