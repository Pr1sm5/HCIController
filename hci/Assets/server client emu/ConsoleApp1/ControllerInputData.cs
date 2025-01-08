namespace ControllerEmulation;

public class ControllerInputData
{
    //Controller Buttons
    public bool ButtonA { get; set; }
    public bool ButtonB { get; set; }
    public bool ButtonX { get; set; }
    public bool ButtonY { get; set; }
    public bool ButtonL1 { get; set; }
    public bool ButtonL3 { get; set; }
    public bool ButtonR1 { get; set; }
    public bool ButtonR3 { get; set; }
    public bool ButtonStart { get; set; }
    public bool ButtonBack { get; set; }
    public bool LeftArrow { get; set; }
    public bool RightArrow { get; set; }
    public bool DownArrow { get; set; }
    public bool UpArrow { get; set; }
    
    //Controller analog sticks
    public short LeftThumbX { get; set; }
    public short LeftThumbY { get; set; }
    public short RightThumbX { get; set; }
    public short RightThumbY { get; set; }
    
    //Controller Trigger
    public byte LeftTrigger { get; set; }
    public byte RightTrigger { get; set; }
}