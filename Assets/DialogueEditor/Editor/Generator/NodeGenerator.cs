using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
            Create();
        
        EditorGUILayout.EndVertical();
    }

    private void Create()
    {
        var dragUnpinnedSize = new PositionAdapter(new Vector2(50, 50));
        var dragUnpinnedCenter = new PositionAdapter(-dragUnpinnedSize.Get() / 2);
        var dragUnpinnedPosition = new OffsetPosition(_nodeDraggerPosition, dragUnpinnedCenter);
        var dragUnpinnedRect = new CustomRect(dragUnpinnedPosition, dragUnpinnedSize);
            
        var unpinnedSize = new Vector2(100, 100);
        var unpinnedPosition = new OffsetPosition(dragUnpinnedPosition, 
            new PositionAdapter(-new Vector2(unpinnedSize.x, unpinnedSize.y / 4)));

        var unpinnedRect = new CustomRect(unpinnedPosition, new PositionAdapter(unpinnedSize));
            
        var pinPredicate = new InputDependentPredicate(
            new MouseUpInput(), 
            new InRect(_editNodePanelRect, unpinnedPosition));

        var rect = new RectFork(pinPredicate, _editNodePanelRect, unpinnedRect);

        var inDragRect = new InRect(dragUnpinnedRect, _nodeDraggerPosition);
            
        var node = _nodeFactory.Create(rect, dragUnpinnedRect);

        var mouseDragInput = new PredicateDependentInput(inDragRect, new MouseDrag());
            
        var updateRect = new InputDependentUpdate(new EventInput(mouseDragInput), unpinnedRect);
        var updateDragRect = new InputDependentUpdate(mouseDragInput, dragUnpinnedRect);
            
        _updates.Add(updateDragRect, updateRect, node);
    }
}