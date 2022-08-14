using EditorInput;

public class NodeFactory
{
    private readonly IPosition _nodeDraggerPosition;
    private readonly ITexture2D _nodeTexture;
    private readonly ITexture2D _dragTexture;
    private readonly IRect _dragPinnedRect;
    private readonly IRect _editNodePanelRect;
    private readonly IInput _dragInput;
    private readonly IInput _selectInput;
    private readonly IRect _dragUnpinnedRect;
    private readonly IRect _unpinnedRect;
    private readonly PinConditionFactory _pinConditionFactory;
    
    public NodeFactory(
        ITexture2D nodeTexture, 
        ITexture2D dragTexture, 
        IPosition nodeDraggerPosition,
        IRect editNodePanelRect,
        IInput dragInput, 
        IInput selectInput, 
        IRect dragPinnedRect, 
        IRect dragUnpinnedRect,
        IRect unpinnedRect,
        PinConditionFactory pinConditionFactory)
    {
        _nodeTexture = nodeTexture;
        _dragTexture = dragTexture;
        _nodeDraggerPosition = nodeDraggerPosition;
        _editNodePanelRect = editNodePanelRect;
        _dragInput = dragInput;
        _selectInput = selectInput;
        _dragPinnedRect = dragPinnedRect;
        _dragUnpinnedRect = dragUnpinnedRect;
        _unpinnedRect = unpinnedRect;
        _pinConditionFactory = pinConditionFactory;
    }

    public INode Create()
    {
        var pin = _pinConditionFactory.Create();
        
        var rect = new RectFork(pin, _editNodePanelRect, _unpinnedRect);
        var dragRect = new RectFork(pin, _dragPinnedRect, _dragUnpinnedRect);

        var inDragRect = new InRect(dragRect, _nodeDraggerPosition);

        var inRect = new InRect(rect, _nodeDraggerPosition);
        var clickInput = new EventInput(new PredicateDependentInput(inRect, _selectInput));
        
        var dragInput = new PredicateDependentInput(inDragRect, _dragInput);
        var drag = new InputToConditionAdapter(dragInput);
        
        var node = new Node(clickInput,
            _nodeTexture,
            _dragTexture,
            new CachedRect(drag, rect),
            new CachedRect(drag, dragRect),
            pin);
        
        return node;
    }
}