using UnityEngine;

public class CustomSimpleTexture2D : ITexture2D
{
    private readonly Color _color;
    private readonly int _width;
    private readonly int _height;

    public CustomSimpleTexture2D(Color color, int width, int height)
    {
        _color = color;
        _width = width;
        _height = height;
    }

    public Texture2D Get()
    {
        var texture = new Texture2D(_width, _height);
        texture.SetPixel(0, 0, _color);
        
        return texture;
    }
}