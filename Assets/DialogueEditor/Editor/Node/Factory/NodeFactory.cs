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
    private readonly IPredicate _pin;

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
        IPredicate pin)
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
        _pin = pin;
    }

    public INode Create()
    {
        var rect = new RectFork(_pin, _editNodePanelRect, _unpinnedRect);
        var dragRect = new RectFork(_pin, _dragPinnedRect, _dragUnpinnedRect);

        var inDragRect = new InRect(dragRect, _nodeDraggerPosition);

        var inRect = new InRect(rect, _nodeDraggerPosition);
        var clickInput = new EventInput(new PredicateDependentInput(inRect, _selectInput));
        
        var dragInput = new PredicateDependentInput(inDragRect, _dragInput);
        var drag = new InputToPredicateAdapter(dragInput);
        
        var cachedRect = new CachedRect(drag, rect);
        var cachedDragRect = new CachedRect(drag, dragRect);
        
        var node = new SpeechNode(clickInput,
            _nodeTexture,
            _dragTexture,
            cachedRect,
            cachedDragRect,
            _pin);
        
        return node;
    }
}