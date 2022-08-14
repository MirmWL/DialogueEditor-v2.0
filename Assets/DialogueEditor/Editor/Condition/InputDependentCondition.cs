using EditorInput;

public class InputDependentCondition : ICondition
{
    private readonly IInput _input;
    private readonly ICondition _condition;

    public InputDependentCondition(IInput input, ICondition condition)
    {
        _input = input;
        _condition = condition;
    }

    public bool Execute()
    {
        return _input.HasInput() && _condition.Execute();
    }
}