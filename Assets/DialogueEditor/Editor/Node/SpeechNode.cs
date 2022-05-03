using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpeechNode : INode
{
    private readonly IRect _rect;
    private readonly IRect _dragRect;
    private readonly IInput _clickInput;
    private readonly IPredicate _pinned;
    
    private readonly Texture2D _texture;
    private readonly Texture2D _dragTexture;
    private SerializedObject _eventSerializedObject;
    
    private EventBase _eventBase;

    private string _name;
    private string _phrase;
    

    public SpeechNode(
        IInput clickInput, 
        ITexture2D simpleTexture2D,
        ITexture2D  dragTexture,
        IRect rect, 
        IRect dragRect, 
        IPredicate pinned)
    {
        _clickInput = clickInput;
        _texture = simpleTexture2D.Get();
        _dragTexture = dragTexture.Get();
        _rect = rect;
        _dragRect = dragRect;
        _pinned = pinned;
    }

    public void Update()
    {
        var rect = _rect.Get();

        GUI.DrawTexture(rect, _texture);
        GUI.DrawTexture(_dragRect.Get(), _dragTexture);
        
        GUILayout.BeginArea(rect);

        _name = EditorGUILayout.TextField(_name);
        _phrase = EditorGUILayout.TextArea(_phrase);

        if (_pinned.Execute())
            DrawEvent();
        
        GUILayout.EndArea();
        
        if (_clickInput.HasInput())
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