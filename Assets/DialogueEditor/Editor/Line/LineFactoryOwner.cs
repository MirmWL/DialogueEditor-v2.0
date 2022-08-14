using EditorInput;

public class LineFactoryOwner : IUpdate
{
    private readonly ICondition _createCondition;
    private readonly LineFactory _factory;
    
    public LineFactoryOwner(ICondition createCondition, LineFactory factory)
    {
        _createCondition = createCondition;
        _factory = factory;
    }
    
    public void Update()
    {
        if (_createCondition.Execute())
            _factory.Create();
    }
}