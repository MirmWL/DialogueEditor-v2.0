using UnityEngine;

public class CustomBorderTexture2D : ITexture2D
{
    private readonly float _borderWidth;
    private readonly Color _color;
    
    private readonly int _width;
    private readonly int _height;

    public CustomBorderTexture2D(Color color, float borderWidth, int width, int height)
    {
        _color = color;
        _borderWidth = borderWidth;
        _width = width;
        _height = height;
    }

    public Texture2D Get()
    {
        var texture = new Texture2D(_width, _height);

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (x < _borderWidth || x > texture.width - 1 - _borderWidth)
                    texture.SetPixel(x, y, _color);
                else if (y < _borderWidth || y > texture.height - 1 - _borderWidth)
                    texture.SetPixel(x, y, _color);
            }
        }
       
        texture.Apply();
        return texture;
    }
}