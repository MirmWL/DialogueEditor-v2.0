using UnityEngine;

public class XVector : IPosition
{
    private readonly IPosition _position;
    
    public XVector(IPosition position)
    {
        _position = position;
    }
    
    public Vector2 Get()
    {
        return new Vector2(_position.Get().x, 0);
    }
}