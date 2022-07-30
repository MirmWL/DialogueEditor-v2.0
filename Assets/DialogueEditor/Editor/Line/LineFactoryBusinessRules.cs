using EditorInput;

public class LineFactoryBusinessRules : IUpdate
{
    private readonly IPredicate _createPredicate;
    private readonly LineFactory _factory;
    
    public LineFactoryBusinessRules(IPredicate createPredicate, LineFactory factory)
    {
        _createPredicate = createPredicate;
        _factory = factory;
    }
    
    public void Update()
    {
        if (_createPredicate.Execute())
            _factory.Create();
    }
}