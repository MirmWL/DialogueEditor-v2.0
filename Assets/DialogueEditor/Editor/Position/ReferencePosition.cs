using UnityEngine;

public class ReferencePosition : IReferencePosition
{
    private Vector2 _position;

    public ReferencePosition(Vector2 position)
    {
        _position = position;
    }

    public ref Vector2 Get()
    {
        return ref _position;
    }
}