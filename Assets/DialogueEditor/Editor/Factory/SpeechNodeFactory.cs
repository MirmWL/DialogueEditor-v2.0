using UnityEngine;

public class SpeechNodeFactory : IFactory<INode>
{
    private readonly CustomSimpleTexture2D _texture;
    private readonly IPosition _draggerPosition;
    private int _currentIndex;
    
    public SpeechNodeFactory(CustomSimpleTexture2D texture, IPosition draggerPosition)
    {
        _texture = texture;
        _draggerPosition = draggerPosition;
    }

    public INode Create(ReferenceRect rect)
    {
        var mousePosition = new MousePosition();
        
        var clickInput = new PredicateDependentInput(new InRect(rect, mousePosition), new Click());
        var dragInput = new PredicateDependentInput(new InRect(rect, mousePosition), new Drag());
        
        return new SpeechNode(clickInput, dragInput, _draggerPosition, _texture, rect, _currentIndex++);
    }
}