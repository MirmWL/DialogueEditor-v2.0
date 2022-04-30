using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeGenerator : IUpdate
{
    private readonly ReferenceRect _panelRect;
    private readonly ReferenceRect _createButtonRect;
    private readonly Texture2D _texture;
    private readonly Updates _updates;
    
    private readonly Storage<INode> _nodeFactory;
    
    public NodeGenerator(Storage<INode> nodeFactory, Updates updates, ITexture2D texture, 
        ReferenceRect panelRect, ReferenceRect createButtonRect)
    {
        _nodeFactory = nodeFactory;
        _updates = updates;
        _texture = texture.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
    }

    public IEnumerable<INode> Nodes => _nodeFactory.Objects;
    
    public void Update()
    {
        var panelRectPosition = _panelRect.Get();
        var createButtonRectPosition = _createButtonRect.Get();
        
        _panelRect.Get() = new Rect(
            new Vector2(Screen.width - panelRectPosition.width, panelRectPosition.y),
            new Vector2(panelRectPosition.width, Screen.height));
        
        _createButtonRect.Get() = new Rect(
            new Vector2(Screen.width - createButtonRectPosition.width, createButtonRectPosition.y), 
            new Vector2(createButtonRectPosition.width, createButtonRectPosition.height));
        
        GUI.DrawTexture(panelRectPosition, _texture);
        
        EditorGUILayout.BeginVertical();

        if (GUI.Button(createButtonRectPosition, "Create node"))
        {
            var rectPosition = new ReferencePosition(new Vector2(300, 300));
            var rectSize = new Vector2(100, 100);
            
            var rect = new ReferenceRect(new Rect(rectPosition.Get(), rectSize));

            var dragOffset = new PositionWrap(new Vector2(50, 0));
            var dragOffsetPosition = new OffsetPosition(new PositionAdapter(rectPosition), dragOffset);
            var dragSize = new PositionWrap(new Vector2(50, 50));
            
            var dragRect = new ReferenceRect(new CustomRect(dragOffsetPosition, dragSize).Get());
            
            var node = _nodeFactory.Create(rect, dragRect);
            _updates.Add(node);
        }
        
        EditorGUILayout.EndVertical();
    }
}