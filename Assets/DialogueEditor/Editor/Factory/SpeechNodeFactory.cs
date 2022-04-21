public class SpeechNodeFactory : IFactory<INode>
{
    private readonly CustomSimpleTexture2D _texture;
    private readonly IPosition _draggerPosition;
    private readonly IPosition _mousePosition;
    private readonly int _clickCountToZoomNode;
    
    public SpeechNodeFactory(CustomSimpleTexture2D texture, IPosition draggerPosition, int clickCountToZoomNode)
    {
        _texture = texture;
        _draggerPosition = draggerPosition;
        _clickCountToZoomNode = clickCountToZoomNode;
        _mousePosition = new MousePosition();
    }

    public INode Create(ReferenceRect rect)
    {
        var clickInput = new EventInput(
            new PredicateDependentInput(new InRect(rect, _mousePosition), new Click()));
        
        var dragInput = new EventInput(
            new PredicateDependentInput(new InRect(rect, _mousePosition), new Drag()));
        
        var fewClicksInput = new EventInput(
            new PredicateDependentInput(
                new InRect(rect, _mousePosition), new FewClicks(_clickCountToZoomNode)));
        
        return new SpeechNode(clickInput, dragInput, fewClicksInput, _draggerPosition, _texture, rect);
    }
}