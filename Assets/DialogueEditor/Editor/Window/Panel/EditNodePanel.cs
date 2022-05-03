using UnityEngine;

public class EditNodePanel : IUpdate
{
    private readonly IRect _rect;
    private readonly Texture2D _texture;
    
    public EditNodePanel(ITexture2D texture, IRect rect)
    {
        _texture = texture.Get();
        _rect = rect;
    }

    public void Update()
    {
        GUI.DrawTexture(_rect.Get(), _texture);
    }
}