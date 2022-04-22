using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpeechNode : INode
{
    private readonly ReferenceRect _rect;
    private readonly Texture2D _texture;
    
    private readonly IInput _clickInput;
    private readonly IInput _dragInput;
    private readonly IPosition _draggerPosition;
    private readonly float _zoomSizeCoefficient;
    
    private string _name = "жиза тиа";
    private string _phrase = "вапапапап";
    private bool _zoomed;
    
    public SpeechNode(IInput clickInput, IInput dragInput, IPosition draggerPosition, ITexture2D simpleTexture2D, ReferenceRect rect)
    {
        _clickInput = clickInput;
        _dragInput = dragInput;
        _draggerPosition = draggerPosition;
        _texture = simpleTexture2D.Get();
        _rect = rect;
    }
    
    public void Update()
    {
        var rect = _rect.Get();
        var offset = new Vector2(-rect.width / 2, -rect.height / 2);

        if (_clickInput.HasInput())
            Debug.Log("click");

        if (_dragInput.HasInput())
            _rect.Get() = new Rect(
                _draggerPosition.Get() + offset, 
                new Vector2(rect.width, rect.height));

        GUI.DrawTexture(rect, _texture);
        
        GUILayout.BeginArea(rect);

        _name = GUILayout.TextField(_name);
        _phrase = GUILayout.TextArea(_phrase);
  
        GUILayout.EndArea();
    }

}