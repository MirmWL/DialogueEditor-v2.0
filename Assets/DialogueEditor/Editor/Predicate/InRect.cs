using UnityEngine;

public class InRect : IPredicate
{
    private readonly IRect _rect;
    private readonly IPosition _position;

    public InRect(IRect rect, IPosition position)
    {
        _rect = rect;
        _position = position;
    }

    public bool Execute()
    {
        return _rect.Get().Contains(_position.Get());
    }
}