using UnityEngine;

public class SpeechNodeFactory : IFactory<INode>
{
    private readonly ITexture2D _texture;
    private readonly IPosition _draggerPosition;
    private readonly IPosition _mousePosition;
    private readonly ReferenceRect _increasedSize;
    
    public SpeechNodeFactory(ITexture2D texture, IPosition draggerPosition, ReferenceRect increasedSize)
    {
        _texture = texture;
        _draggerPosition = draggerPosition;
        _mousePosition = new MousePosition();
        _increasedSize = increasedSize;
    }

    public INode Create(ReferenceRect rect)
    {
        var clickInput = new EventInput(
            new PredicateDependentInput(new InRect(rect, _mousePosition), new Click()));
        
        var dragInput = new EventInput(
            new PredicateDependentInput(new InRect(rect, _mousePosition), new Drag()));

        return new SpeechNode(clickInput, dragInput, _draggerPosition, _texture, rect, _increasedSize);
    }
}