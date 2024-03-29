﻿using UnityEngine;

public class CachedRect : IRect
{
    private readonly IRect _rect;
    private readonly ICondition _returnCached;
    private IRect _cached;
    
    public CachedRect(ICondition returnCached, IRect rect)
    {
        _rect = rect;
        _returnCached = returnCached;
        _cached = rect;
    }

    public Rect Get()
    {
        var result = _returnCached.Execute() ? _cached : _rect;

        _cached = result;
        return result.Get();
    }
}