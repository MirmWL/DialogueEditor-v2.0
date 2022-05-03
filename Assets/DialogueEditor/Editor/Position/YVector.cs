using UnityEngine;

public class YVector : IPosition
{
    private readonly IPosition _position;

    public YVector(IPosition position)
    {
        _position = position;
    }

    public Vector2 Get()
    {
        return new Vector2(0, _position.Get().y);
    }
}