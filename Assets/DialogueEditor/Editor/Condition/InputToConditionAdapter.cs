using EditorInput;

public class InputToConditionAdapter : ICondition
{
    private readonly IInput _input;

    public InputToConditionAdapter(IInput input)
    {
        _input = input;
    }

    public bool Execute()
    {
        return _input.HasInput();
    }
}