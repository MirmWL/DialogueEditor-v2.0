using UnityEngine;

public class RectFork : IRect
{
    private readonly ICondition _condition;
    private readonly IRect _first;
    private readonly IRect _second;

    public RectFork(ICondition condition, IRect first, IRect second)
    {
        _condition = condition;
        _first = first;
        _second = second;
    }

    public Rect Get()
    {
        return _condition.Execute() ? _first.Get() : _second.Get();
    }
}