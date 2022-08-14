public class PredicateDependentUpdate : IUpdate
{
    private readonly IUpdate _update;
    private readonly ICondition _condition;

    public PredicateDependentUpdate(IUpdate update, ICondition condition)
    {
        _update = update;
        _condition = condition;
    }

    public void Update()
    {
        if(_condition.Execute())
            _update.Update();
    }
}