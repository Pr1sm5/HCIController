namespace ControllerEmulation
{
    public interface IJoystick
    {
        bool isDragging { get; }
        UnityEngine.Vector2 GetInput();
    }
}