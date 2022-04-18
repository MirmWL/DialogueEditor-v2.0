using UnityEngine;

public class MousePosition : IPosition
{
    public Vector2 Get()
    {
        return Event.current.mousePosition;
    }
}