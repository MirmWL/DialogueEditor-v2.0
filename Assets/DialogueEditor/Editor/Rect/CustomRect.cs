using UnityEngine;

public class CustomRect : IRect, IUpdate
{
    private readonly IPosition _position;
    private readonly IPosition _size;

    private Rect _rect;
    
    public CustomRect(IPosition position, IPosition size)
    {
        _position = position;
        _size = size;
        _rect = new Rect(position.Get(), size.Get());
    }

    public Rect Get()
    {
        return _rect;
    }

    public void Update()
    {
        Debug.Log($"{_position.Get()}");
        _rect = new Rect(_position.Get(), _size.Get());
    }
}