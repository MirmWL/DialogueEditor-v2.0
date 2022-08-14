using UnityEngine;

public class PositionFork : IPosition
{
    private readonly ICondition _condition;
    private readonly IPosition _first;
    private readonly IPosition _second;

    public PositionFork(ICondition condition, IPosition first, IPosition second)
    {
        _condition = condition;
        _first = first;
        _second = second;
    }

    public Vector2 Get()
    {
        return _condition.Execute() ? _first.Get() : _second.Get();
    }
}