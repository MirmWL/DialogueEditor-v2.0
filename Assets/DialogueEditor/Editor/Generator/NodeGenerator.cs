using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeGenerator : IUpdate
{
    private readonly IRect _panelRect;
    private readonly IRect _createButtonRect;
    private readonly IPosition _nodeDraggerPosition;
    private readonly IRect _editNodePanelRect;
    private readonly INodeFactory _nodeFactory;

    private readonly Texture2D _texture;
    private readonly Updates _updates;

    public NodeGenerator(INodeFactory nodeFactory, Updates updates, ITexture2D texture, 
        IRect panelRect, IRect createButtonRect, IPosition nodeDraggerPosition, IRect editNodePanelRect)
    {
        _nodeFactory = nodeFactory;
        _updates = updates;
        _texture = texture.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
        _nodeDraggerPosition = nodeDraggerPosition;
        _editNodePanelRect = editNodePanelRect;
    }
    
    public void Update()
    {
        var createButtonRectPosition = _createButtonRect.Get();

        GUI.DrawTexture(_panelRect.Get(), _texture);
        
        EditorGUILayout.BeginVertical();

        if (GUI.Button(createButtonRectPosition, "Create node"))
        { 
            var nodeRectSize = new Vector2(300, 300);
            var nodeOffset = new PositionAdapter(new Vector2(-nodeRectSize.x / 2, -nodeRectSize.y / 2));
            var nodeRectPosition = new OffsetPosition(_nodeDraggerPosition, nodeOffset);

            var nodeUnpinnedRect = new CustomRect(nodeRectPosition, new PositionAdapter(nodeRectSize));
            
            var pinPredicate = new InputDependentPredicate(
                new MouseUpInput(), 
                new InRect(_editNodePanelRect, nodeRectPosition));

            var nodeRect = new RectFork(pinPredicate, _editNodePanelRect, nodeUnpinnedRect);
            
            var dragOffsetPosition = new OffsetPosition(
                nodeRectPosition, 
                new PositionAdapter(new Vector2(50, 0)));
            
            var dragSize = new PositionAdapter(new Vector2(50, 50));
            
            var dragUnpinnedRect = new CustomRect(dragOffsetPosition, dragSize);
            
            var node = _nodeFactory.Create(nodeRect, dragUnpinnedRect);

            var mouseDownInput = new EventInput(new MouseDownInput());
            
            var updateNodeRect = new InputDependentUpdate(mouseDownInput, nodeUnpinnedRect);
            var updateDragRect = new InputDependentUpdate(mouseDownInput, dragUnpinnedRect);
            
            _updates.Add(updateDragRect, updateNodeRect, node);
        }
        
        EditorGUILayout.EndVertical();
    }
    
}