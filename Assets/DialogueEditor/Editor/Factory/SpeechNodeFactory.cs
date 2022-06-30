using EditorInput;

public class SpeechNodeFactory : INodeFactory
{
    private readonly IPosition _nodeDraggerPosition;
    private readonly IPosition _dragUnpinnedSize;
    private readonly IPosition _unpinnedSize;
    private readonly IPosition _createConnectionButtonSize;
    private readonly IPosition _unpinnedPosition;
    private readonly IPosition _createConnectionButtonPinnedPosition;
    private readonly IPosition _createConnectionButtonUnpinnedPosition;
    private readonly IPosition _dragUnpinnedPosition;
    private readonly ITexture2D _nodeTexture;
    private readonly ITexture2D _dragTexture;
    private readonly IRect _dragPinnedRect;
    private readonly IRect _editNodePanelRect;
    private readonly IInput _dragInput;
    private readonly IInput _selectInput;
    private readonly Updates _updates;
    private readonly CustomButtonFactory _customButtonFactory;

    public SpeechNodeFactory(
        ITexture2D nodeTexture, 
        ITexture2D dragTexture, 
        IPosition nodeDraggerPosition, 
        IRect editNodePanelRect,
        IInput dragInput, 
        IInput selectInput, 
        IPosition dragUnpinnedSize, 
        IPosition unpinnedSize,
        IPosition createConnectionButtonSize,
        IPosition dragUnpinnedPosition,
        IRect dragPinnedRect, 
        IPosition unpinnedPosition, 
        IPosition createConnectionButtonPinnedPosition,
        IPosition createConnectionButtonUnpinnedPosition,
        Updates updates,
        CustomButtonFactory customButtonFactory)

    {
        _nodeTexture = nodeTexture;
        _dragTexture = dragTexture;
        _nodeDraggerPosition = nodeDraggerPosition;
        _editNodePanelRect = editNodePanelRect;
        _dragInput = dragInput;
        _selectInput = selectInput;
        _dragUnpinnedSize = dragUnpinnedSize;
        _unpinnedSize = unpinnedSize;
        _createConnectionButtonSize = createConnectionButtonSize;
        _dragUnpinnedPosition = dragUnpinnedPosition;
        _dragPinnedRect = dragPinnedRect;
        _unpinnedPosition = unpinnedPosition;
        _createConnectionButtonPinnedPosition = createConnectionButtonPinnedPosition;
        _createConnectionButtonUnpinnedPosition = createConnectionButtonUnpinnedPosition;
        _updates = updates;
        _customButtonFactory = customButtonFactory;
    }

    public INode Create()
    {
        var unpinnedRect = new CustomRect(_unpinnedPosition, _unpinnedSize);
        var dragUnpinnedRect = new CustomRect(_dragUnpinnedPosition, _dragUnpinnedSize);

        var inEditPanel = new Predicates(
            new InRect(dragUnpinnedRect, _nodeDraggerPosition), 
            new InRect(_editNodePanelRect, _dragUnpinnedPosition));
        
        var pinPredicate = new CachedPredicate(
            new Not(new InputToPredicateAdapter(_dragInput)),
            inEditPanel);

        var rect = new RectFork(pinPredicate, _editNodePanelRect, unpinnedRect);
        var dragRect = new RectFork(pinPredicate, _dragPinnedRect, dragUnpinnedRect);

        var inDragRect = new InRect(dragRect, _nodeDraggerPosition);

        var inRect = new InRect(rect, _nodeDraggerPosition);
        var clickInput = new EventInput(new PredicateDependentInput(inRect, _selectInput));

        var createConnectionButtonPosition = new PositionFork(pinPredicate, 
            _createConnectionButtonPinnedPosition,
            _createConnectionButtonUnpinnedPosition);

        var createConnectionButtonRect = new CustomRect(createConnectionButtonPosition, _createConnectionButtonSize);
        
        var dragInput = new PredicateDependentInput(inDragRect, _dragInput);

        var updateRect = new InputDependentUpdate(dragInput, unpinnedRect);
        var updateDragUnpinnedRect = new InputDependentUpdate(dragInput, dragUnpinnedRect);
        var updateCreateConnectionButtonRect = new InputDependentUpdate(new EventInput(dragInput), createConnectionButtonRect);

        var node = new SpeechNode(clickInput,
            _nodeTexture,
            _dragTexture,
            rect,
            dragRect,
            pinPredicate);

        var createConnectionButton = _customButtonFactory.Create(createConnectionButtonRect);

        _updates.Add(updateDragUnpinnedRect,
            updateRect,
            node,
            updateCreateConnectionButtonRect,
            createConnectionButton);
        
        return node;
    }
}