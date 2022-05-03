
using UnityEngine;

public class ScreenSize : IPosition
{
    public Vector2 Get()
    {
        return new Vector2(Screen.width, Screen.height);
    }
}
