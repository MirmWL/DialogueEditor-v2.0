using EditorInput;

public class ConnectionButtonFactory
{
    private readonly IPosition _pinnedPosition;
    private readonly IPosition _unpinnedPosition;
    private readonly IPosition _size;
    private readonly ITexture2D _texture;
    private readonly IGUIStyle _style;
    private readonly Updates _updates;
    private readonly string _label;

    public ConnectionButtonFactory(
        IPosition pinnedPosition,
        IPosition unpinnedPosition,
        IPosition size,
        ITexture2D texture,
        IGUIStyle style,
        Updates updates,
        string label)
    {
        _pinnedPosition = pinnedPosition;
        _unpinnedPosition = unpinnedPosition;
        _size = size;
        _texture = texture;
        _style = style;
        _updates = updates;
        _label = label;
    }

    public CustomButton Create(IPredicate pin, IInput dragInput)
    {
        var position = new PositionFork(pin, 
            _pinnedPosition,
            _unpinnedPosition);
        
        var rect = new CachedRect(
            new InputToPredicateAdapter(new EventInput(dragInput)),
            new CustomRect(position, _size));

        var button = new CustomButton(rect, _texture, _style, _label);

        return button;
    }

}