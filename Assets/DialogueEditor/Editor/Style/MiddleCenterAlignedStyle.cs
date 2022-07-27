using UnityEngine;

public class MiddleCenterAlignedStyle : IGUIStyle
{
    private readonly IGUIStyle _style;

    public MiddleCenterAlignedStyle(IGUIStyle style)
    {
        _style = style;
    }

    public GUIStyle Get()
    {
        return new GUIStyle(_style.Get())
        {
            alignment = TextAnchor.MiddleCenter
        };
    }
}