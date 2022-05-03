public class InputDependentUpdate : IUpdate
{
    private readonly IInput _input;
    private readonly IUpdate _update;

    public InputDependentUpdate(IInput input, IUpdate update)
    {
        _input = input;
        _update = update;
    }

    public void Update()
    {
        if(_input.HasInput())
            _update.Update();
    }
}