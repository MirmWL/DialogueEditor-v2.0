public class NodeFactoryBusinessRules : IUpdate
{
    private readonly IPredicate _createPredicate;
    private readonly NodeFactory _nodeFactory;
    private readonly ConnectionButtonFactory _connectionButtonFactory;
    private readonly Updates _updates;

    public NodeFactoryBusinessRules(
        IPredicate createPredicate,
        NodeFactory nodeFactory,
        ConnectionButtonFactory connectionButtonFactory,
        Updates updates)
    {
        _createPredicate = createPredicate;
        _nodeFactory = nodeFactory;
        _connectionButtonFactory = connectionButtonFactory;
        _updates = updates;
    }

    public void Update()
    {
        if (_createPredicate.Execute())
            _updates.Add(_nodeFactory.Create(), _connectionButtonFactory.Create());
    }
}