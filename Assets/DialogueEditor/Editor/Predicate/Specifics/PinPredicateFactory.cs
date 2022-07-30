using EditorInput;

public class PinPredicateFactory
{
    private readonly IPosition _nodeDraggerPosition;
    private readonly IPosition _dragUnpinnedPosition;
    private readonly IRect _editNodePanelRect;
    private readonly IInput _dragInput;

    public PinPredicateFactory(
        IPosition nodeDraggerPosition,
        IPosition dragUnpinnedPosition,
        IRect editNodePanelRect,
        IInput dragInput)
    {
        _nodeDraggerPosition = nodeDraggerPosition;
        _dragUnpinnedPosition = dragUnpinnedPosition;
        _editNodePanelRect = editNodePanelRect;
        _dragInput = dragInput;
    }

    public IPredicate Create(IRect dragUnpinnedRect)
    {
        var inEditPanel = new Predicates(
            new InRect(dragUnpinnedRect, _nodeDraggerPosition), 
            new InRect(_editNodePanelRect, _dragUnpinnedPosition));
        
        var pin = new CachedPredicate(
            new Not(new InputToPredicateAdapter(_dragInput)),
            inEditPanel);
        
        return pin;
    }
}