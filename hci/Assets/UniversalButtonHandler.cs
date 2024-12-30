using UnityEngine;

public class UniversalButtonHandler : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controls;
    public GameObject layoutPanel;// Verknüpfe das Panel im Inspector

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        controls.SetActive(false); // Settings-Panel anzeigen
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        controls.SetActive(true); // Controller-Panel anzeigen
    }
    
    public void OpenLayout()
    {
        layoutPanel.SetActive(true);
        settingsPanel.SetActive(false); // Layout-Panel anzeigen
    }

    public void CloseLayout()
    {
        settingsPanel.SetActive(true);
        layoutPanel.SetActive(false); // Settings-Panel anzeigen
    }
    
    public void OnButtonPress(string buttonName)
    {
        Debug.Log($"Button {buttonName} pressed");
        // Hier kannst du abhängig vom Button Namen unterschiedliche Aktionen ausführen
        switch (buttonName)
        {
            case "A":

                break;
            case "B":

                break;
            case "X":

                break;
            case "Y":

                break;
            case "LB":

                break;
            case "RB":

                break;
            case "Settings":
                if (settingsPanel.activeSelf == false) {
                    OpenSettings();
                } else {
                    CloseSettings();
                }
                break;
            case "Layout":
                if (layoutPanel.activeSelf == false) {
                    OpenLayout();
                } else {
                    CloseLayout();
                }
                break;
            case "Start":

                break;
            case "Select":

                break;
            default:
                Debug.Log($"No specific action for {buttonName}");
                break;
        }
    }

}

