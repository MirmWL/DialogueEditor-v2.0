using System.Collections.Generic;
using UnityEngine;

public class CustomBorderTexture2D : ITexture2D
{
    private readonly KeyValuePair<int, int> _ratio;
    private readonly Color _color;
    
    private readonly int _width;
    private readonly int _height;

    public CustomBorderTexture2D(KeyValuePair<int, int> ratio, Color color, int width, int height)
    {
        _ratio = ratio;
        _color = color;
        _width = width;
        _height = height;
    }

    public Texture2D Get()
    {
        var texture = new Texture2D(_width, _height);
        var color = new[]{_color};
        
        texture.SetPixels(0,0, _width, _ratio.Value, color);
        texture.SetPixels(_ratio.Key, 0, _width, _ratio.Value, color);
        
        texture.SetPixels(0,0,_ratio.Key, _height, color);
        texture.SetPixels(0,_ratio.Value, _ratio.Key, _height, color);
  
        texture.Apply();
        return texture;
    }
}