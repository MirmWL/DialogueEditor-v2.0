using UnityEngine;

public class RectAdapter : IRect
{
    private readonly IReferenceRect _referenceRect;

    public RectAdapter(IReferenceRect referenceRect)
    {
        _referenceRect = referenceRect;
    }

    public Rect Get()
    {
        return _referenceRect.Get();
    }
}