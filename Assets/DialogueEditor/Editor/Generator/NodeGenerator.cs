using UnityEditor;
using UnityEngine;

public class NodeGenerator : IUpdate
{
    private readonly IRect _panelRect;
    private readonly IRect _createButtonRect;
    private readonly IPosition _nodeDraggerPosition;
    private readonly IPosition _editNodePanelPosition;
    private readonly IRect _editNodePanelRect;
    private readonly INodeFactory _nodeFactory;

    private readonly Texture2D _texture;
    private readonly Updates _updates;

    public NodeGenerator(INodeFactory nodeFactory, Updates updates, ITexture2D texture, 
        IRect panelRect, IRect createButtonRect, IPosition nodeDraggerPosition, IRect editNodePanelRect, IPosition editNodePanelPosition)
    {
        _nodeFactory = nodeFactory;
        _updates = updates;
        _texture = texture.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
        _nodeDraggerPosition = nodeDraggerPosition;
        _editNodePanelRect = editNodePanelRect;
        _editNodePanelPosition = editNodePanelPosition;
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

        var dragPinnedSize = new PositionAdapter(new Vector2(25, _editNodePanelRect.Get().y / 1.25f));
        var dragPinnedCenter = new PositionAdapter(-dragPinnedSize.Get() / 2);
        var dragPinnedPosition = new OffsetPosition(_editNodePanelPosition, dragPinnedCenter);
        var dragPinnedRect = new CustomRect(dragPinnedPosition, dragPinnedSize);
        var inDragRect = new InRect(dragUnpinnedRect, _nodeDraggerPosition);
        
        var unpinnedSize = new Vector2(100, 100);
        var unpinnedPosition = new OffsetPosition(dragUnpinnedPosition, 
            new PositionAdapter(-new Vector2(unpinnedSize.x, unpinnedSize.y / 4)));
        var unpinnedRect = new CustomRect(unpinnedPosition, new PositionAdapter(unpinnedSize));
        
        var pinPredicate = new InRect(_editNodePanelRect, dragUnpinnedPosition);
        var rect = new RectFork(pinPredicate, _editNodePanelRect, unpinnedRect);
        var dragRect = new RectFork(pinPredicate, dragPinnedRect, dragUnpinnedRect);
        
        var node = _nodeFactory.Create(rect, dragRect);

        var dragInput = new PredicateDependentInput(inDragRect, new MouseDrag());
            
        var updateRect = new InputDependentUpdate(new EventInput(dragInput), unpinnedRect);
        var updateDragUnpinnedRect = new InputDependentUpdate(dragInput, dragUnpinnedRect);
        var updateDragPinnedRect = new InputDependentUpdate(dragInput, dragPinnedRect);

        _updates.Add(updateDragUnpinnedRect, updateRect, updateDragPinnedRect, node);
    }
}