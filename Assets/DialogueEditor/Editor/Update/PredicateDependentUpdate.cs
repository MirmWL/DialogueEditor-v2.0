public class PredicateDependentUpdate : IUpdate
{
    private readonly IUpdate _update;
    private readonly IPredicate _predicate;

    public PredicateDependentUpdate(IUpdate update, IPredicate predicate)
    {
        _update = update;
        _predicate = predicate;
    }

    public void Update()
    {
        if(_predicate.Execute())
            _update.Update();
    }
}