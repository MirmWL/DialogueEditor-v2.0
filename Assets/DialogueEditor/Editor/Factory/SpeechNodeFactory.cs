
public class SpeechNodeFactory : IFactory<INode>
{
    private readonly CustomSimpleTexture2D _texture;
    private readonly IPosition _draggerPosition;
    private readonly IPosition _mousePosition;
    private int _currentIndex;
    
    public SpeechNodeFactory(CustomSimpleTexture2D texture, IPosition draggerPosition)
    {
        _texture = texture;
        _draggerPosition = draggerPosition;
        _mousePosition = new MousePosition();
    }

    public INode Create(ReferenceRect rect)
    {
        var clickInput = new PredicateDependentInput(new InRect(rect, _mousePosition), new Click());
        var dragInput = new PredicateDependentInput(new InRect(rect, _mousePosition), new Drag());
        
        return new SpeechNode(clickInput, dragInput, _draggerPosition, _texture, rect, _currentIndex++);
    }
}