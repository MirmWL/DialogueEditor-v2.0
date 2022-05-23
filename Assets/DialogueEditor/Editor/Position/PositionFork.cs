using UnityEngine;

public class PositionFork : IPosition
{
    private readonly IPredicate _predicate;
    private readonly IPosition _first;
    private readonly IPosition _second;

    public PositionFork(IPredicate predicate, IPosition first, IPosition second)
    {
        _predicate = predicate;
        _first = first;
        _second = second;
    }

    public Vector2 Get()
    {
        return _predicate.Execute() ? _first.Get() : _second.Get();
    }
}