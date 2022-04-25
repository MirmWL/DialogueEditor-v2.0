using UnityEngine;

public class PositionWrap : IPosition
{
    private readonly Vector2 _position;

    public PositionWrap(Vector2 position)
    {
        _position = position;
    }

    public Vector2 Get()
    {
        return _position;
    }
}