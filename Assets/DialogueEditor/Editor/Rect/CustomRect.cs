using UnityEngine;

public class CustomRect : IRect
{
    private readonly IPosition _position;
    private readonly IPosition _size;

    public CustomRect(IPosition position, IPosition size)
    {
        _position = position;
        _size = size;
    }

    public Rect Get()
    {
        return new Rect(_position.Get(), _size.Get());
    }
}