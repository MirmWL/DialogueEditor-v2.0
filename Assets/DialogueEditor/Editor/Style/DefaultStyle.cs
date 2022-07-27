using UnityEngine;

public class DefaultStyle : IGUIStyle
{
    public GUIStyle Get()
    {
        return new GUIStyle();
    }
}