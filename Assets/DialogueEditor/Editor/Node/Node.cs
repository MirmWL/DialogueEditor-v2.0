using EditorInput;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Node : INode
{
    private readonly IRect _rect;
    private readonly IRect _dragRect;
    private readonly IInput _click;
    private readonly ICondition _pin;
    
    private readonly Texture2D _mainTexture;
    private readonly Texture2D _dragTexture;
    private SerializedObject _eventSerializedObject;
    
    private EventBase _eventBase;

    private string _name;
    private string _phrase;
    
    public Node(
        IInput click, 
        ITexture2D mainTexture,
        ITexture2D dragTexture,
        IRect rect, 
        IRect dragRect, 
        ICondition pin)
    {
        _click = click;
        _mainTexture = mainTexture.Get();
        _dragTexture = dragTexture.Get();
        _rect = rect;
        _dragRect = dragRect;
        _pin = pin;
    }

    public void Update()
    {
        var rect = _rect.Get();

        GUI.DrawTexture(rect, _mainTexture);
        GUI.DrawTexture(_dragRect.Get(), _dragTexture);
        
        GUILayout.BeginArea(rect);

        _name = EditorGUILayout.TextField(_name);
        _phrase = EditorGUILayout.TextArea(_phrase);

        if (_pin.Execute())
            DrawEvent();
        
        GUILayout.EndArea();
        
        if (_click.HasInput())
            Debug.Log("click");
    }

    private void DrawEvent()
    {
        var eventBase = (EventBase)EditorGUILayout.ObjectField(_eventBase, typeof(EventBase));
        
        if (_eventBase != eventBase)
        {
            _eventBase = eventBase;
            _eventSerializedObject = new SerializedObject(_eventBase);
        }
        
        if (_eventBase == null) return;

        var property = _eventSerializedObject.FindProperty(nameof(_eventBase.Event));
        EditorGUILayout.PropertyField(property);
    }
}