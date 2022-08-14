using EditorInput;

public class ConnectionButtonFactory
{
    private readonly IPosition _pinnedPosition;
    private readonly IPosition _unpinnedPosition;
    private readonly IPosition _size;
    private readonly ITexture2D _texture;
    private readonly IGUIStyle _style;
    private readonly string _label;
    private readonly PinConditionFactory _pinFactory;

    public ConnectionButtonFactory(
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

    public CustomButton Create()
    {
        var pin = _pinFactory.Create();
        
        var position = new PositionFork(pin, 
            _pinnedPosition,
            _unpinnedPosition);
        
        var rect = new CachedRect(
            new InputToConditionAdapter(new EventInput(dragInput)),
            new CustomRect(position, _size));

        var button = new CustomButton(rect, _texture, _style, _label);

        return button;
    }

}