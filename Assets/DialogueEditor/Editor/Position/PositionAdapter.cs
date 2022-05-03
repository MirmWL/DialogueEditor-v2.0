using UnityEngine;

public class PositionAdapter : IPosition
{
    private readonly Vector2 _position;

    public PositionAdapter(Vector2 position)
    {
        _position = position;
    }

    public Vector2 Get()
    {
        return _position;
    }
}