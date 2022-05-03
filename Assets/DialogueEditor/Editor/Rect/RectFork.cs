using UnityEngine;

public class RectFork : IRect
{
    private readonly IPredicate _predicate;
    private readonly IRect _first;
    private readonly IRect _second;

    public RectFork(IPredicate predicate, IRect first, IRect second)
    {
        _predicate = predicate;
        _first = first;
        _second = second;
    }

    public Rect Get()
    {
        return _predicate.Execute() ? _first.Get() : _second.Get();
    }
}