using UnityEngine;

public class InRect : IPredicate
{
    private readonly ReferenceRect _rect;
    private readonly IPosition _position;

    public InRect(ReferenceRect rect, IPosition position)
    {
        _rect = rect;
        _position = position;
    }

    public bool Execute()
    {
        return _rect.Get().Contains(_position.Get());
    }
}