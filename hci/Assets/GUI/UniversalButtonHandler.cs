using System;
using ControllerEmulation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

public class UniversalButtonHandler : MonoBehaviour
{
    public ControllerInputData InputData;
    private IJoystick[] joysticks = new IJoystick[2]; // Referenzen zu allen Joysticks
    [SerializeField] private MonoBehaviour joystickLeft;
    [SerializeField] private MonoBehaviour joystickRight;
    private short _leftStickY;
    private short _leftStickX;
    private short _rightStickY;
    private short _rightStickX;

    private float _lastUpdateTime = 0f;
    private float _joystickUpdateInterval = 0.01f;
    
    private void Start()
    {
        var startClient = transform.GetComponentInParent<StartClient>();
        if (startClient != null)
        {
            InputData = startClient._input;
        }
        else
        {
            Debug.LogError("StartClient component not found in parent!");
        }
        joysticks[0] = joystickLeft as IJoystick;
        joysticks[1] = joystickRight as IJoystick;
    }

    private void Update()
    {
        float currentTime = Time.time;
        if (joysticks[0].isDragging || joysticks[1].isDragging) UpdateJoysticks();
        else if (currentTime - _lastUpdateTime >= _joystickUpdateInterval)
        {
            UpdateJoysticks();
        }
    }
    
    //Updates the Joystick positions
    private void UpdateJoysticks()
    {
        foreach (var joystick in joysticks)
        {
            UnityEngine.Vector2 input = joystick.GetInput();

            if (joystick == joysticks[0])
            {
                _leftStickX = (short)((input.x * short.MaxValue) );
                _leftStickY = (short)((input.y * short.MaxValue) );
            }
            else
            {
                _rightStickX = (short)((input.x * short.MaxValue) );
                _rightStickY = (short)((input.y * short.MaxValue) );
            }
        }
        InputData.LeftThumbX = _leftStickX;
        InputData.LeftThumbY = _leftStickY;
        InputData.RightThumbX = _rightStickX;
        InputData.RightThumbY = _rightStickY;
    }
    
    public void OnButtonPress(string buttonName)
        {
            if (InputData == null) return;

            // Log the button press for debugging
            Debug.Log($"Button {buttonName} pressed");

            // Handle button presses and update ControllerInputData
            switch (buttonName)
            {
                case "A":
                    InputData.ButtonA = true;
                    break;
                case "B":
                    InputData.ButtonB = true;
                    break;
                case "X":
                    InputData.ButtonX = true;
                    break;
                case "Y":
                    InputData.ButtonY = true;
                    break;
                case "LB":
                    InputData.ButtonL1 = true;
                    break;
                case "RB":
                    InputData.ButtonR1 = true;
                    break;
                case "RT":
                    InputData.RightTrigger = 255;
                    break;
                case "LT":
                    InputData.LeftTrigger = 255;
                    break;
                case "Start":
                    InputData.ButtonStart = true;
                    break;
                case "Back":
                    InputData.ButtonBack = true;
                    break;
                case "Up":
                    InputData.UpArrow = true;
                    break;
                case "Down":
                    InputData.DownArrow = true;
                    break;
                case "Left":
                    InputData.LeftArrow = true;
                    break;
                case "Right":
                    InputData.RightArrow = true;
                    break;
                case "Left-Stick" :
                    break;
                case "Right-Stick" :
                    break;
                case "Settings":
                    break;
                case "Layout":
                    break;
                default:
                    Debug.LogWarning($"Unrecognized button: {buttonName}");
                    break;
            }
            Debug.Log(InputData);
        }

        public void OnButtonRelease(string buttonName)
        {
            if (InputData == null) return;

            // Reset the button state on release
            Debug.Log($"Button {buttonName} released");

            switch (buttonName)
            {
                case "A":
                    InputData.ButtonA = false;
                    break;
                case "B":
                    InputData.ButtonB = false;
                    break;
                case "X":
                    InputData.ButtonX = false;
                    break;
                case "Y":
                    InputData.ButtonY = false;
                    break;
                case "LB":
                    InputData.ButtonL1 = false;
                    break;
                case "RB":
                    InputData.ButtonR1 = false;
                    break;
                case "RT":
                    InputData.RightTrigger = 0;
                    break;
                case "LT":
                    InputData.LeftTrigger = 0;
                    break;
                case "Start":
                    InputData.ButtonStart = false;
                    break;
                case "Back":
                    InputData.ButtonBack = false;
                    break;
                case "Up":
                    InputData.UpArrow = false;
                    break;
                case "Down":
                    InputData.DownArrow = false;
                    break;
                case "Left":
                    InputData.LeftArrow = false;
                    break;
                case "Right":
                    InputData.RightArrow = false;
                    break;
                default:
                    Debug.LogWarning($"Unrecognized button: {buttonName}");
                    break;
            }
            
            Debug.Log(InputData);
        }

    
    public void OnButtonPressOld(string buttonName)
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
                break;
            case "Layout":
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

