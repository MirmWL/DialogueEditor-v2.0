public class InputToPredicateAdapter : IPredicate
{
    private readonly IInput _input;

    public InputToPredicateAdapter(IInput input)
    {
        _input = input;
    }

    public bool Execute()
    {
        return _input.HasInput();
    }
}