using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PropertiesPanel : IUpdate
{
    private readonly IEnumerable<SerializedProperty> _properties;
    
    private readonly Texture2D _texture;
    private readonly Rect _rect;

    public PropertiesPanel(IProperties property, Rect rect, CustomSimpleTexture2D simpleTexture2D)
    {
        _properties = property.Get();
        _rect = rect;

        _texture = simpleTexture2D.Get();
    }
    
    public void Update()
    {
        GUI.DrawTexture(_rect, _texture);
        
        GUILayout.BeginArea(_rect);
        
        foreach (var property in _properties)
            EditorGUILayout.PropertyField(property);
        
        GUILayout.EndArea();
    }
}