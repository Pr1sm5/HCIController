using System;
using System.IO.Compression;
using System.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace ControllerEmulation
{
    class ControllerEmu
    {
        private ViGEmClient _client;
        private IXbox360Controller _controller;
        private short _upDownValueL = 0;
        private short _leftRightValueL = 0;
        private short _upDownValueR = 0;
        private short _leftRightValueR = 0;

        public ControllerEmu()
        {
            // Create a ViGEm client
            _client = new ViGEmClient();
            // Create a virtual Xbox 360 controller
            _controller = _client.CreateXbox360Controller();
            
            _controller.Connect();
            
            Console.WriteLine("New Controller added");
        }

        public void SetControllerInputs(ControllerInputData input)
        {
            if (input == null)
            {
                Console.WriteLine("Input data is null.");
                return; // Exit early if the input is invalid.
            }

            try
            {
                // Debugging the inputs
                Console.WriteLine($"Button A: {input.ButtonA}, Button B: {input.ButtonB}");
                Console.WriteLine($"LeftThumbX: {input.LeftThumbX}, LeftThumbY: {input.LeftThumbY}");
                Console.WriteLine($"RightThumbX: {input.RightThumbX}, RightThumbY: {input.RightThumbY}");
                Console.WriteLine($"LeftTrigger: {input.LeftTrigger}, RightTrigger: {input.RightTrigger}");

                // Set the button states
                _controller.SetButtonState(Xbox360Button.A, input.ButtonA);
                _controller.SetButtonState(Xbox360Button.B, input.ButtonB);
                _controller.SetButtonState(Xbox360Button.X, input.ButtonX);
                _controller.SetButtonState(Xbox360Button.Y, input.ButtonY);
                _controller.SetButtonState(Xbox360Button.Start, input.ButtonStart);
                _controller.SetButtonState(Xbox360Button.Back, input.ButtonBack);
                _controller.SetButtonState(Xbox360Button.LeftShoulder, input.ButtonL1);
                _controller.SetButtonState(Xbox360Button.RightShoulder, input.ButtonR1);
                _controller.SetButtonState(Xbox360Button.LeftThumb, input.ButtonL3);
                _controller.SetButtonState(Xbox360Button.RightThumb, input.ButtonR3);
                _controller.SetButtonState(Xbox360Button.Up, input.UpArrow);
                _controller.SetButtonState(Xbox360Button.Down, input.DownArrow);
                _controller.SetButtonState(Xbox360Button.Left, input.LeftArrow);
                _controller.SetButtonState(Xbox360Button.Right, input.RightArrow);
                
                // Set the thumbstick values
                _controller.SetAxisValue(Xbox360Axis.LeftThumbX, input.LeftThumbX);
                _controller.SetAxisValue(Xbox360Axis.LeftThumbY, input.LeftThumbY);
                _controller.SetAxisValue(Xbox360Axis.RightThumbX, input.RightThumbX);
                _controller.SetAxisValue(Xbox360Axis.RightThumbY, input.RightThumbY);
                
                // Set the trigger values
                _controller.SetSliderValue(Xbox360Slider.LeftTrigger, input.LeftTrigger);
                _controller.SetSliderValue(Xbox360Slider.RightTrigger, input.RightTrigger);

                Console.WriteLine($"Slider input = {_controller.RightTrigger}");
                // Submit the updated report
                _controller.SubmitReport();
                Console.WriteLine("Inputs Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating controller inputs: {ex.Message}");
            }
        }

        public void ResetController()
        {
            // Reset all button states
            // Reset all button states explicitly
            _controller.SetButtonState(Xbox360Button.A, false);
            _controller.SetButtonState(Xbox360Button.B, false);
            _controller.SetButtonState(Xbox360Button.X, false);
            _controller.SetButtonState(Xbox360Button.Y, false);
            _controller.SetButtonState(Xbox360Button.Start, false);
            _controller.SetButtonState(Xbox360Button.Back, false);
            _controller.SetButtonState(Xbox360Button.LeftShoulder, false);
            _controller.SetButtonState(Xbox360Button.RightShoulder, false);
            _controller.SetButtonState(Xbox360Button.LeftThumb, false);
            _controller.SetButtonState(Xbox360Button.RightThumb, false);
            _controller.SetButtonState(Xbox360Button.Up, false);
            _controller.SetButtonState(Xbox360Button.Down, false);
            _controller.SetButtonState(Xbox360Button.Left, false);
            _controller.SetButtonState(Xbox360Button.Right, false);

            // Reset all axes (left and right thumbsticks) to neutral position (0)
            _controller.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
            _controller.SetAxisValue(Xbox360Axis.LeftThumbY, 0);
            _controller.SetAxisValue(Xbox360Axis.RightThumbX, 0);
            _controller.SetAxisValue(Xbox360Axis.RightThumbY, 0);

            // Reset all triggers to unpressed position (0)
            _controller.SetSliderValue(Xbox360Slider.LeftTrigger, 0);
            _controller.SetSliderValue(Xbox360Slider.RightTrigger, 0);

            // Submit the reset report
            _controller.SubmitReport();
        }

        public void MoveLeftStick(ConsoleKey key)
        {
            
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _upDownValueL += 1000;
                    _controller.SetAxisValue(Xbox360Axis.LeftThumbY, _upDownValueL);
                    Console.WriteLine("Left stick moved Up: " + _upDownValueL);
                    break;
                
                case ConsoleKey.DownArrow:
                    _upDownValueL -= 1000;
                    _controller.SetAxisValue(Xbox360Axis.LeftThumbY, _upDownValueL);
                    Console.WriteLine("Left stick moved Down: " + _upDownValueL);
                    break;
                
                case ConsoleKey.LeftArrow:
                    _leftRightValueL -= 1000;
                    _controller.SetAxisValue(Xbox360Axis.LeftThumbX, _leftRightValueL);
                    Console.WriteLine("Left stick moved left: " + _leftRightValueL);
                    break;
                
                case ConsoleKey.RightArrow:
                    _leftRightValueL += 1000;
                    _controller.SetAxisValue(Xbox360Axis.LeftThumbX, _leftRightValueL);
                    Console.WriteLine("Left stick moved right: " + _leftRightValueL);
                    break;
                
                default:
                    break;
            }
        }

        public void MoveRightStick(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _upDownValueR += 1000;
                    _controller.SetAxisValue(Xbox360Axis.RightThumbY, _upDownValueR);
                    Console.WriteLine("Left stick moved Up: " + _upDownValueR);
                    break;
                
                case ConsoleKey.DownArrow:
                    _upDownValueR -= 1000;
                    _controller.SetAxisValue(Xbox360Axis.RightThumbY, _upDownValueR);
                    Console.WriteLine("Left stick moved Down: " + _upDownValueR);
                    break;
                
                case ConsoleKey.LeftArrow:
                    _leftRightValueR -= 1000;
                    _controller.SetAxisValue(Xbox360Axis.RightThumbX, _leftRightValueR);
                    Console.WriteLine("Left stick moved left: " + _leftRightValueR);
                    break;
                
                case ConsoleKey.RightArrow:
                    _leftRightValueR += 1000;
                    _controller.SetAxisValue(Xbox360Axis.RightThumbX, _leftRightValueR);
                    Console.WriteLine("Left stick moved right: " + _leftRightValueR);
                    break;
                
                default:
                    break;
            }
        }
        
        // static void Main2(string[] args)
        // {
        //     ControllerEmu test = new ControllerEmu();
        //     var controller = test._controller;
        //     var client = test._client;
        //    
        //
        //     Console.WriteLine("Virtual Xbox controller is now connected.");
        //     Console.WriteLine("Press 'A' or 'B' to simulate button presses.");
        //     Console.WriteLine("Press 'Q' to quit the program.");
        //
        //     bool isAButtonPressed = false;
        //     bool isBButtonPressed = false;
        //     bool isLeftStickActive = false;
        //     bool isRightStickActive = false;
        //
        //     while (true)
        //     {
        //         // Check if a key is pressed
        //         if (Console.KeyAvailable)
        //         {
        //             var keyInfo = Console.ReadKey(intercept: true); // intercept the key to avoid printing it
        //
        //             if (keyInfo.Key == ConsoleKey.A)
        //             {
        //                 short value= 0;
        //                 // Toggle the state of the 'A' button
        //                 isAButtonPressed = !isAButtonPressed;
        //                 value =  isAButtonPressed ?  (short) 10000 : (short) -10000;
        //                 controller.SetButtonState(Xbox360Button.A, isAButtonPressed);
        //                 Console.WriteLine(isAButtonPressed ? "Button A is pressed." : "Button A is released.");
        //                 controller.SetAxisValue(Xbox360Axis.LeftThumbX, value); // Simulate left thumbstick movement
        //                 controller.SetAxisValue(Xbox360Axis.LeftThumbY, value); // Simulate right thumbstick movement
        //             }
        //             else if (keyInfo.Key == ConsoleKey.B)
        //             {
        //                 short value= 0;
        //                 // Toggle the state of the 'B' button
        //                 isBButtonPressed = !isBButtonPressed;
        //                 value =  isBButtonPressed ?  (short) 30000 : (short) -30000;
        //                 controller.SetButtonState(Xbox360Button.B, isBButtonPressed);
        //                 Console.WriteLine(isBButtonPressed ? "Button B is pressed." : "Button B is released.");
        //                 controller.SetAxisValue(Xbox360Axis.RightThumbX, value); // Simulate left thumbstick movement
        //                 controller.SetAxisValue(Xbox360Axis.RightThumbY, value); // Simulate right thumbstick movement
        //             }
        //             else if (keyInfo.Key == ConsoleKey.L)
        //             {
        //                 isLeftStickActive = !isLeftStickActive;
        //                 Console.WriteLine("Left stick is: " + isLeftStickActive);
        //             }
        //             else if (keyInfo.Key == ConsoleKey.O)
        //             {
        //                 isRightStickActive = !isRightStickActive;
        //                 Console.WriteLine("Right stick is: " + isRightStickActive);
        //             }
        //             else if (keyInfo.Key == ConsoleKey.R)
        //             {
        //                 //Reset the Controller Input
        //                 test.ResetController();
        //                 Console.WriteLine("Inputs have been reset");
        //             }
        //             else if (keyInfo.Key == ConsoleKey.Q)
        //             {
        //                 // Exit the program when 'Q' is pressed
        //                 break;
        //             }
        //
        //             if (isLeftStickActive && !isRightStickActive)
        //             {
        //                 test.MoveLeftStick(keyInfo.Key);
        //             }
        //             else if (isRightStickActive && !isLeftStickActive)
        //             {
        //                 test.MoveRightStick(keyInfo.Key);
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Only one Stick Active at a time Please!!!!");
        //             }
        //         }
        //
        //         // Simulate other controller states (you can keep this part for other inputs if needed)
        //         
        //         controller.SetSliderValue(Xbox360Slider.RightTrigger, 255); // Simulate trigger pull
        //
        //         // Send the updates to the virtual controller
        //         controller.SubmitReport();
        //
        //         Thread.Sleep(100); // Delay to avoid high CPU usage
        //     }
        //
        //     // Disconnect the controller when done
        //     controller.Disconnect();
        //     client.Dispose();
        //
        //     Console.WriteLine("Program has exited.");
        // }
    }
}
