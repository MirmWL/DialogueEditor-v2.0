using UnityEngine;

public class EditNodePanel : IUpdate
{
    private readonly ReferenceRect _rect;
    private readonly Texture2D _texture;

    public EditNodePanel(ReferenceRect rect, ITexture2D texture)
    {
        _rect = rect;
        _texture = texture.Get();
    }

    public void Update()
    {
        Debug.Log("draw");
        GUI.DrawTexture(_rect.Get(), _texture);
    }
}