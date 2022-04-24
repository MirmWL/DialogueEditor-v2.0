using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditNodePanel : IUpdate
{
    private readonly ReferenceRect _rect;
    private readonly Texture2D _texture;
    private readonly IInput _mouseUpInput;
    private readonly IEnumerable<INode> _nodes; 
    private INode _current;
    
    public EditNodePanel(IInput mouseUpInput, ITexture2D texture,  IEnumerable<INode> nodes, ReferenceRect rect)
    {
        _mouseUpInput = mouseUpInput;
        _texture = texture.Get();
        _nodes = nodes;
        _rect = rect;
    }

    public void Update()
    {
        GUI.DrawTexture(_rect.Get(), _texture);

        if (!_mouseUpInput.HasInput())
            return;
        
        if (_current != null)
        {
            if (_rect.Get().Contains(_current.Rect.position) == false)
            {
                _current?.UnPin();
                _current = null;
            }
        }

        var node = _nodes.FirstOrDefault(s => _rect.Get().Contains(s.Rect.position));
        _current = node;
        node?.Pin();
    }
}