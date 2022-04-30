using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpeechNode : INode
{
    private readonly ReferenceRect _rect;
    private readonly ReferenceRect _pinnedRect;
    private readonly ReferenceRect _dragRect;
    private readonly Vector2 _unpinnedSize;
    private readonly Texture2D _texture;
    private readonly Texture2D _dragTexture;
    private SerializedObject _eventSerializedObject;
    
    private EventBase _eventBase;
    
    private readonly IInput _clickInput;
    private readonly IInput _dragInput;
    private readonly IPosition _draggerPosition;
    
    private string _name;
    private string _phrase;
    private bool _pinned;

    public SpeechNode(IInput clickInput, IInput dragInput, IPosition draggerPosition, ITexture2D simpleTexture2D, ITexture2D  dragTexture, ReferenceRect rect, ReferenceRect pinnedRect, ReferenceRect dragRect)
    {
        _clickInput = clickInput;
        _dragInput = dragInput;
        _draggerPosition = draggerPosition;
        _texture = simpleTexture2D.Get();
        _dragTexture = dragTexture.Get();
        _rect = rect;
        _pinnedRect = pinnedRect;
        _unpinnedSize = rect.Get().size;
        _dragRect = dragRect;
    }

    public Rect Rect => _rect.Get();
    
    public void Update()
    {
        var rect = _rect.Get();
        var offset = new Vector2(-rect.width / 2, -rect.height / 2);

        GUI.DrawTexture(rect, _texture);
        GUI.DrawTexture(_dragRect.Get(), _dragTexture);
        
        GUILayout.BeginArea(rect);

        _name = EditorGUILayout.TextField(_name);
        _phrase = EditorGUILayout.TextArea(_phrase);

        if (_pinned)
            DrawEvent();
        
        GUILayout.EndArea();
        
        if (_clickInput.HasInput())
            Debug.Log("click");

        if (_dragInput.HasInput())
            _rect.Get() = new Rect(_draggerPosition.Get() + offset, new Vector2(rect.width, rect.height));
    }
    
    public void Pin()
    {
        _rect.Get() = _pinnedRect.Get();
        _pinned = true;
    }
    
    public void UnPin()
    {
        _rect.Get().size = _unpinnedSize;
        _pinned = false;
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