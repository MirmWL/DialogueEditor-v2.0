
public class CachedCondition : ICondition
{
    private readonly ICondition _returnCached;
    private readonly ICondition _condition;
    
    private bool _cached;

    public CachedCondition(ICondition returnCached, ICondition condition)
    {
        _returnCached = returnCached;
        _condition = condition;
        _cached = _condition.Execute();
    }

    public bool Execute()
    {
        var result = _returnCached.Execute() ? _cached : _condition.Execute();

        _cached = result;
        return result;
    }
}