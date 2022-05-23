
public class SpeechNodeFactory : INodeFactory
{
    private readonly ITexture2D _texture;
    private readonly ITexture2D _dragTexture;

    public SpeechNodeFactory(ITexture2D texture, ITexture2D dragTexture)
    {
        _texture = texture;
        _dragTexture = dragTexture;
    }

    public INode Create(IRect rect, IRect dragRect,IRect createConnectionButtonRect, IPredicate pinnedPredicate, IInput clickInput)
    {
        return new SpeechNode(clickInput, _texture, _dragTexture, rect, dragRect, pinnedPredicate, createConnectionButtonRect);
    }
}