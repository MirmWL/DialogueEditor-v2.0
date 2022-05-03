public class InputDependentPredicate : IPredicate
{
    private readonly IInput _input;
    private readonly IPredicate _predicate;

    public InputDependentPredicate(IInput input, IPredicate predicate)
    {
        _input = input;
        _predicate = predicate;
    }

    public bool Execute()
    {
        return _input.HasInput() && _predicate.Execute();
    }
}