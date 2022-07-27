public class NodeFactoryBusinessRules : IUpdate
{
    private readonly IPredicate _createPredicate;
    private readonly NodeFactory _factory;

    public NodeFactoryBusinessRules(IPredicate createPredicate, NodeFactory factory)
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