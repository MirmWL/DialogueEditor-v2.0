using EditorInput;

public class ConnectionTextLabelFactory
{
    private readonly IPosition _pinnedPosition;
    private readonly IPosition _unpinnedPosition;
    private readonly IPosition _size;
    private readonly ITexture2D _texture;
    private readonly IGUIStyle _style;
    private readonly string _label;
    private readonly PinConditionFactory _pinFactory;

    public ConnectionTextLabelFactory(
        IPosition pinnedPosition,
        IPosition unpinnedPosition,
        IPosition size,
        ITexture2D texture,
        IGUIStyle style,
        string label, 
        PinConditionFactory pinFactory)
    {
        _pinnedPosition = pinnedPosition;
        _unpinnedPosition = unpinnedPosition;
        _size = size;
        _texture = texture;
        _style = style;
        _label = label;
        _pinFactory = pinFactory;
    }

    public CustomTextLabel Create(IInput dragInput)
    {
        var pin = _pinFactory.Create();
        
        var position = new PositionFork(pin, 
            _pinnedPosition,
            _unpinnedPosition);
        
        var rect = new CachedRect(
            new Not(new InputToConditionAdapter(new EventInput(dragInput))),
            new CustomRect(position, _size));

        var button = new CustomTextLabel(rect, _texture, _style, _label);

        return button;
    }

}