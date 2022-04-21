using UnityEngine;

public class FewClicks : IInput
{
    private readonly int _clickCount;

    public FewClicks(int clickCount)
    {
        _clickCount = clickCount;
    }

    public bool HasInput()
    {
        return Event.current.clickCount == _clickCount;
    }
}