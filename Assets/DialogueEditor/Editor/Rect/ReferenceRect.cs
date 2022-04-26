using UnityEngine;

public class ReferenceRect : IReferenceRect
{
    private Rect _rect;

    public ReferenceRect(Rect rect)
    {
        _rect = rect;
    }

    public ref Rect Get()
    {
        return ref _rect;
    }
}