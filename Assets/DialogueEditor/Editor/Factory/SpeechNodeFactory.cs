
public class SpeechNodeFactory : INodeFactory
{
    private readonly ITexture2D _texture;
    private readonly ITexture2D _dragTexture;

    public SpeechNodeFactory(ITexture2D texture, ITexture2D dragTexture)
    {
        _texture = texture;
        _dragTexture = dragTexture;
    }

    public INode Create(IRect rect, IRect dragRect, IPredicate pinnedPredicate, IInput clickInput)
    {
        //var inRect = new InRect(rect, _draggerPosition);
        //var click = new EventInput(new PredicateDependentInput(inRect, clickInput));
        
        return new SpeechNode(clickInput, _texture, _dragTexture, rect, dragRect, pinnedPredicate);
    }
}