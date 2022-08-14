public class ConditionFork : ICondition
{
    private readonly ICondition _condition;
    private readonly ICondition _first;
    private readonly ICondition _second;

    public ConditionFork(ICondition condition, ICondition first, ICondition second)
    {
        _condition = condition;
        _first = first;
        _second = second;
    }

    public bool Execute()
    {
        return _condition.Execute() ? _first.Execute() : _second.Execute();
    }
}