public class Not : IPredicate
{
    private readonly IPredicate _predicate;

    public Not(IPredicate predicate)
    {
        _predicate = predicate;
    }

    public bool Execute()
    {
        return !_predicate.Execute();
    }
}