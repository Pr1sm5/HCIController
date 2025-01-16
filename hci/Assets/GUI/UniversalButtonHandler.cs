using UnityEngine;

public class UniversalButtonHandler : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controllerPanel;
    public GameObject layoutPanel;
    public JoystickHandler[] joysticks; // Referenzen zu allen Joysticks

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        controllerPanel.SetActive(false);
        layoutPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        controllerPanel.SetActive(true);
        layoutPanel.SetActive(false);
    }
    
    public void OpenLayout()
    {
        layoutPanel.SetActive(true);
        settingsPanel.SetActive(false);
        controllerPanel.SetActive(false);
        
        // Aktiviere Layout-Modus für alle Joysticks im Layout-Panel
        foreach (JoystickHandler joystick in joysticks)
        {
            if (joystick.transform.parent == layoutPanel.transform)
            {
                joystick.SetLayoutMode(true);
            }
        }
    }

    public void CloseLayout()
    {
        settingsPanel.SetActive(true);
        layoutPanel.SetActive(false);
        controllerPanel.SetActive(false);
        
        // Deaktiviere Layout-Modus für alle Joysticks
        foreach (JoystickHandler joystick in joysticks)
        {
            joystick.SetLayoutMode(false);
        }
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

