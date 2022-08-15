using UnityEngine;

public class AppliedTexture : ITexture2D
{
    private readonly ITexture2D _texture;

    public AppliedTexture(ITexture2D texture)
    {
        _texture = texture;
    }

    public Texture2D Get()
    {
        var texture = _texture.Get();
        
        texture.Apply();
        return texture;
    }
}