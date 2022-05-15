using UnityEngine;

public class TwoPredicateRectFork :IRect
{
    private readonly IPredicate _firstPredicate;
    private readonly IPredicate _secondPredicate;
    
    private readonly IRect _firstRect;
    private readonly IRect _secondRect;
    private readonly IRect _defaultRect;

    public TwoPredicateRectFork(IPredicate firstPredicate, IPredicate secondPredicate, IRect firstRect, IRect secondRect, IRect defaultRect)
    {
        _firstPredicate = firstPredicate;
        _secondPredicate = secondPredicate;
        _firstRect = firstRect;
        _secondRect = secondRect;
        _defaultRect = defaultRect;
    }

    public Rect Get()
    {
        if (_firstPredicate.Execute())
            return _firstRect.Get();
        
        return _secondPredicate.Execute() ? _secondRect.Get() : _defaultRect.Get();
    }
}