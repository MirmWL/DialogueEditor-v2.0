﻿using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpeechNode : INode
{
    private readonly ReferenceRect _rect;
    private readonly Texture2D _texture;
    
    private readonly IInput _clickInput;
    private readonly IInput _dragInput;
    private readonly IInput _fewClicksInput;
    private readonly IPosition _draggerPosition;

    private string _name = "жиза тиа";
    private string _phrase = "вапапапап";

    public SpeechNode(IInput clickInput, IInput dragInput, IInput fewClicksInput, IPosition draggerPosition, CustomSimpleTexture2D simpleTexture2D, ReferenceRect rect)
    {
        _clickInput = clickInput;
        _dragInput = dragInput;
        _fewClicksInput = fewClicksInput;
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

        if (_fewClicksInput.HasInput())
            Debug.Log("zoom");    
        
        GUI.DrawTexture(rect, _texture);
        
        GUILayout.BeginArea(rect);

        _name = GUILayout.TextField(_name);
        _phrase = GUILayout.TextArea(_phrase);
        
        GUILayout.EndArea();
    }
}