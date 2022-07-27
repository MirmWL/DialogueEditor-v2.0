using UnityEngine;

public class CustomButton : IUpdate
{
    private readonly IRect _rect;
    private readonly ITexture2D _texture;
    private readonly IGUIStyle _style;
    private readonly string _text;

    public CustomButton(IRect rect, ITexture2D texture, IGUIStyle style, string text)
    {
        _rect = rect;
        _texture = texture;
        _style = style;
        _text = text;
    }

    public void Update()
    {
        var rect = _rect.Get();
        
        GUI.DrawTexture(rect, _texture.Get());
        GUI.Label(rect, _text, _style.Get());
    }
}