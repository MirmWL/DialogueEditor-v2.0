using EditorInput;

public class PinPredicate : IPredicate
{
    private readonly IPosition _nodeDraggerPosition;
    private readonly IPosition _dragUnpinnedPosition;
    private readonly IRect _editNodePanelRect;
    private readonly IInput _dragInput;
    private readonly IRect _dragUnpinnedRect;

    public PinPredicate(
        IPosition nodeDraggerPosition,
        IPosition dragUnpinnedPosition,
        IRect editNodePanelRect,
        IInput dragInput, 
        IRect dragUnpinnedRect)
    {
        _nodeDraggerPosition = nodeDraggerPosition;
        _dragUnpinnedPosition = dragUnpinnedPosition;
        _editNodePanelRect = editNodePanelRect;
        _dragInput = dragInput;
        _dragUnpinnedRect = dragUnpinnedRect;
    }

    public bool Execute()
    {
        var inEditPanel = new Predicates(
            new InRect(_dragUnpinnedRect, _nodeDraggerPosition), 
            new InRect(_editNodePanelRect, _dragUnpinnedPosition));
        
        var pin = new CachedPredicate(
            new Not(new InputToPredicateAdapter(_dragInput)),
            inEditPanel);
        
        return pin.Execute();
    }
}