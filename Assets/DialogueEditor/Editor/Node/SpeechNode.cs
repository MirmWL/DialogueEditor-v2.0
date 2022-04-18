using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpeechNode : INode
{
    private readonly ReferenceRect _rect;
    private readonly Texture2D _texture;
    
    private readonly IInput _clickInput;
    private readonly IInput _dragInput;
    private readonly IPosition _draggerPosition;

    private readonly int _index;

    public SpeechNode(IInput clickInput, IInput dragInput, IPosition draggerPosition, CustomSimpleTexture2D simpleTexture2D, ReferenceRect rect, int index)
    {
        _clickInput = clickInput;
        _dragInput = dragInput;
        _draggerPosition = draggerPosition;
        _texture = simpleTexture2D.Get();
        _rect = rect;
        _index = index;
    }
    
    public void Update()
    {
        var rectPosition = _rect.Get();
        var offset = new Vector2(-rectPosition.width / 2, -rectPosition.height / 2);

        GUI.DrawTexture(rectPosition, _texture);

        if (_clickInput.HasInput())
            Debug.Log("click");

        if (_dragInput.HasInput())
            _rect.Get().position = _draggerPosition.Get() + offset;
        
        Event.current.Use();
    }
}