public class PredicateFork : IPredicate
{
    private readonly IPredicate _predicate;
    private readonly IPredicate _first;
    private readonly IPredicate _second;

    public PredicateFork(IPredicate predicate, IPredicate first, IPredicate second)
    {
        _predicate = predicate;
        _first = first;
        _second = second;
    }

    public bool Execute()
    {
        return _predicate.Execute() ? _first.Execute() : _second.Execute();
    }
}