public class CustomButtonFactory
{
    private readonly ITexture2D _texture;
    private readonly string _text;

    public CustomButtonFactory(ITexture2D texture, string text)
    {
        _texture = texture;
        _text = text;
    }

    public CustomButton Create(IRect rect)
    {
        var button = new CustomButton(rect, _texture, _text);
        return button;
    }
    
}