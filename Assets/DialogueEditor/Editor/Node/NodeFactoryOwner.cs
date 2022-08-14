public class NodeFactoryOwner : IUpdate
{
    private readonly ICondition _create;
    private readonly NodeFactory _nodeFactory;
    private readonly Updates _updates;

    public NodeFactoryOwner(
        ICondition create,
        NodeFactory nodeFactory,
        Updates updates)
    {
        _create = create;
        _nodeFactory = nodeFactory;
        _updates = updates;
    }

    public void Update()
    {
        if (_create.Execute())
            _updates.Add(_nodeFactory.Create());
    }
}