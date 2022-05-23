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
    private readonly IInput _dragInput;
    
    private readonly Texture2D _texture;
    private readonly Updates _updates;

    public NodeGenerator(
        INodeFactory nodeFactory, 
        Updates updates, 
        ITexture2D texture, 
        IRect panelRect, 
        IRect createButtonRect,
        IPosition nodeDraggerPosition,
        IRect editNodePanelRect, 
        IPosition editNodePanelPosition,
        IInput dragInput)
    {
        _nodeFactory = nodeFactory;
        _updates = updates;
        _texture = texture.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
        _nodeDraggerPosition = nodeDraggerPosition;
        _editNodePanelRect = editNodePanelRect;
        _editNodePanelPosition = editNodePanelPosition;
        _dragInput = dragInput;
    }
    
    public void Update()
    {
        GUI.DrawTexture(_panelRect.Get(), _texture);
        
        EditorGUILayout.BeginVertical();

        if (GUI.Button(_createButtonRect.Get(), "Create node"))
            Create();
        
        EditorGUILayout.EndVertical();
    }

    private void Create()
    {
        var dragUnpinnedSize = new PositionAdapter(new Vector2(50, 50));
        var dragUnpinnedPosition = GetDragUnpinnedPosition(dragUnpinnedSize);
        var dragUnpinnedRect = new CustomRect(dragUnpinnedPosition, dragUnpinnedSize);
        var dragPinnedRect = GetDragPinnedRect();

        var unpinnedSize = new Vector2(100, 100);
        var unpinnedPosition = new OffsetPosition(dragUnpinnedPosition,
            new PositionAdapter(-new Vector2(unpinnedSize.x, unpinnedSize.y / 4)));

        var unpinnedRect = new CustomRect(unpinnedPosition, new PositionAdapter(unpinnedSize));
 
        var inEditPanel = new Predicates(
            new InRect(dragUnpinnedRect, _nodeDraggerPosition),
            new InRect(_editNodePanelRect, dragUnpinnedPosition)); 
        
        var pinPredicate = new CachedPredicate(
            new Not(new InputToPredicateAdapter(_dragInput)),
            inEditPanel);
        
        var rect = new RectFork(pinPredicate, _editNodePanelRect, unpinnedRect);
        var dragRect = new RectFork(pinPredicate, dragPinnedRect, dragUnpinnedRect);

        var inDragRect = new InRect(dragRect, _nodeDraggerPosition);

        var inRect = new InRect(rect, _nodeDraggerPosition);
        var clickInput = new EventInput(new PredicateDependentInput(inRect, new MouseClick()));

        var node = _nodeFactory.Create(rect, dragRect, pinPredicate, clickInput);

        var dragInput = new PredicateDependentInput(inDragRect, _dragInput);

        var updateRect = new InputDependentUpdate(new EventInput(dragInput), unpinnedRect);
        var updateDragUnpinnedRect = new InputDependentUpdate(dragInput, dragUnpinnedRect);

        _updates.Add(updateDragUnpinnedRect, updateRect, node);
    }

    private CustomRect GetDragPinnedRect()
    {
        var dragPinnedSize = new PositionAdapter(new Vector2(25, _editNodePanelRect.Get().size.y));
        var dragPinnedPosition =
            new XVector(new OffsetPosition(_editNodePanelPosition, new PositionAdapter(_editNodePanelRect.Get().size)));
        
        return new CustomRect(dragPinnedPosition, dragPinnedSize);
    }
    
    private IPosition GetDragUnpinnedPosition(IPosition dragUnpinnedSize)
    {
        var dragUnpinnedCenter = new PositionAdapter(-dragUnpinnedSize.Get() / 2);
        return new OffsetPosition(_nodeDraggerPosition, dragUnpinnedCenter);
    }
}