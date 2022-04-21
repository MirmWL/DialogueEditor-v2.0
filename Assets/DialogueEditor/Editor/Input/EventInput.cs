using UnityEngine;

public class EventInput : IInput
{
    private readonly IInput _input;

    public EventInput(IInput input)
    {
        _input = input;
    }

    public bool HasInput()
    {
        var hasInput = _input.HasInput();
        
        if(hasInput)
            Event.current.Use();
        
        return hasInput;
    }
}