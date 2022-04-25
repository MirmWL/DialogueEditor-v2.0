using System.Collections.Generic;

public class Storage<T> : INodeFactory<T> where T : INode
{
    private readonly INodeFactory<T> _factory;
    private readonly List<T> _objects;

    public Storage(INodeFactory<T> factory)
    {
        _factory = factory;
        _objects = new List<T>();
    }

    public IEnumerable<T> Objects => _objects;
    
    public T Create(ReferenceRect rect, ReferenceRect dragRect)
    {
        var instanceOfObject = _factory.Create(rect, dragRect);
        _objects.Add(instanceOfObject);
        
        return instanceOfObject;
    }
}