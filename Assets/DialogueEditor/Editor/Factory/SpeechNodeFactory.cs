﻿
public class SpeechNodeFactory : INodeFactory<INode>
{
    private readonly ITexture2D _texture;
    private readonly ITexture2D _dragTexture;
    private readonly IPosition _draggerPosition;
    private readonly IPosition _mousePosition;
    private readonly ReferenceRect _pinnedRect;
    
    public SpeechNodeFactory(ITexture2D texture, ITexture2D dragTexture, IPosition draggerPosition, ReferenceRect pinnedRect)
    {
        _texture = texture;
        _dragTexture = dragTexture;
        _draggerPosition = draggerPosition;
        _mousePosition = new MousePosition();
        _pinnedRect = pinnedRect;
    }

    public INode Create(ReferenceRect rect, ReferenceRect dragRect)
    {
        var clickInput = new EventInput(
            new PredicateDependentInput(new InRect(rect, _mousePosition), new Click()));
        
        var dragInput = new EventInput(
            new PredicateDependentInput(new InRect(dragRect, _mousePosition), new Drag()));

        return new SpeechNode(clickInput, dragInput, _draggerPosition, _texture,_dragTexture ,rect, _pinnedRect, dragRect);
    }
}