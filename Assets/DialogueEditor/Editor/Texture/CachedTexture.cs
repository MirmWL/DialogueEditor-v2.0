using UnityEngine;

public class CachedTexture : ITexture2D
{
    private readonly ICondition _returnCached;
    private readonly ITexture2D _texture;
    
    private ITexture2D _cached;

    public CachedTexture(ICondition returnCached, ITexture2D texture)
    {
        _returnCached = returnCached;
        _texture = texture;
        _cached = _texture;
    }

    public Texture2D Get()
    {
        var result = _returnCached.Execute() ? _cached : _texture;

        _cached = result;
        return result.Get();
    }
}