using EditorInput;

public class PinConditionFactory
{
    private readonly IPosition _nodeDraggerPosition;
    private readonly IPosition _dragUnpinnedPosition;
    private readonly IRect _editNodePanelRect;
    private readonly IInput _dragInput;
    private readonly IRect _dragUnpinnedRect;
    
    public PinConditionFactory(
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

    public ICondition Create()
    {
        var inEditPanel = new Conditions(
            new InRect(_dragUnpinnedRect, _nodeDraggerPosition), 
            new InRect(_editNodePanelRect, _dragUnpinnedPosition));
        
        var pin = new CachedCondition(
            new Not(new InputToConditionAdapter(_dragInput)),
            inEditPanel);

        return pin;
    }
}