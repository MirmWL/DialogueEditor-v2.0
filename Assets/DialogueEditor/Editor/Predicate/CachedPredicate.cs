public class CachedPredicate : IPredicate
{
    private readonly IPredicate _returnCached;
    private readonly IPredicate _predicate;
    
    private bool _cached;

    public CachedPredicate(IPredicate returnCached, IPredicate predicate)
    {
        _returnCached = returnCached;
        _predicate = predicate;
    }

    public bool Execute()
    {
        var result = _returnCached.Execute() ? _cached : _predicate.Execute();

        _cached = result;
        return result;
    }
}