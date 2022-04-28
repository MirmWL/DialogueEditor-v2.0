using UnityEngine;

public class PositionAdapter : IPosition
{
    private readonly IReferencePosition _position;

    public PositionAdapter(IReferencePosition position)
    {
        _position = position;
    }

    public Vector2 Get()
    {
        return _position.Get();
    }
}