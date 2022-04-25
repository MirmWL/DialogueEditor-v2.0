using UnityEngine;

public class OffsetPosition : IPosition
{
    private readonly IPosition _position;
    private readonly IPosition _offset;

    public OffsetPosition(IPosition position, IPosition offset)
    {
        _position = position;
        _offset = offset;
    }

    public Vector2 Get()
    {
        return _position.Get() + _offset.Get();
    }
}