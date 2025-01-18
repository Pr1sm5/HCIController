using System;

namespace ControllerEmulation
{
    public class ControllerInputData
    {
        //Controller Buttons
        private bool _buttonA;
        public bool ButtonA
        {
            get => _buttonA;
            set
            {
                if (_buttonA != value)
                {
                    _buttonA = value;
                    OnInputChanged?.Invoke();
                }   
            }
        }
        
        private bool _buttonB;
        public bool ButtonB { get => _buttonB;
            set
            {
                if (_buttonB != value)
                {
                    _buttonB = value;
                    OnInputChanged?.Invoke();
                }
            }
        }
        private bool _buttonX;

        public bool ButtonX
        {
            get => _buttonX;
            set
            {
                if (_buttonX != value)
                {
                    _buttonX = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonY;
        public bool ButtonY {
            get => _buttonY;
            set
            {
                if (_buttonY != value)
                {
                    _buttonY = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonL1;
        public bool ButtonL1 {
            get => _buttonL1;
            set
            {
                if (_buttonL1 != value)
                {
                    _buttonL1 = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonL3;
        public bool ButtonL3 {
            get => _buttonL3;
            set
            {
                if (_buttonL3 != value)
                {
                    _buttonL3 = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonR1;
        public bool ButtonR1 {
            get => _buttonR1;
            set
            {
                if (_buttonR1 != value)
                {
                    _buttonR1 = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonR3;
        public bool ButtonR3 {
            get => _buttonR3;
            set
            {
                if (_buttonR3 != value)
                {
                    _buttonR3 = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonStart;
        public bool ButtonStart {
            get => _buttonStart;
            set
            {
                if (_buttonStart != value)
                {
                    _buttonStart = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _buttonBack;
        public bool ButtonBack {
            get => _buttonBack;
            set
            {
                if (_buttonBack != value)
                {
                    _buttonBack = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _leftArrow;
        public bool LeftArrow {
            get => _leftArrow;
            set
            {
                if (_leftArrow != value)
                {
                    _leftArrow = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _rightArrow;
        public bool RightArrow {
            get => _rightArrow;
            set
            {
                if (_rightArrow != value)
                {
                    _rightArrow = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _downArrow;
        public bool DownArrow {
            get => _downArrow;
            set
            {
                if (_downArrow != value)
                {
                    _downArrow = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private bool _upArrow;
        public bool UpArrow {
            get => _upArrow;
            set
            {
                if (_upArrow != value)
                {
                    _upArrow = value;
                    OnInputChanged?.Invoke();
                }
            }
        }
    
        //Controller analog sticks
        private short _leftThumbX;
        public short LeftThumbX {
            get => _leftThumbX;
            set
            {
                if (_leftThumbX != value)
                {
                    _leftThumbX = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private short _leftThumbY;
        public short LeftThumbY {
            get => _leftThumbY;
            set
            {
                if (_leftThumbY != value)
                {
                    _leftThumbY = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private short _rightThumbX;
        public short RightThumbX {
            get => _rightThumbX;
            set
            {
                if (_rightThumbX != value)
                {
                    _rightThumbX = value;
                    OnInputChanged?.Invoke();
                }
            }
        }

        private short _rightThumbY;
        public short RightThumbY {
            get => _rightThumbY;
            set
            {
                if (_rightThumbY != value)
                {
                    _rightThumbY = value;
                    OnInputChanged?.Invoke();
                }
            }
        }
    
        //Controller Trigger
        public byte LeftTrigger { get; set; }
        public byte RightTrigger { get; set; }

        public event Action OnInputChanged;
    } 
}

