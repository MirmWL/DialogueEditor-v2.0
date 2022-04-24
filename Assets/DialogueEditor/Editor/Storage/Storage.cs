using System.Collections.Generic;

public class Storage<T> : IFactory<T>
{
    private readonly IFactory<T> _factory;
    private readonly List<T> _objects;

    public Storage(IFactory<T> factory)
    {
        _factory = factory;
        _objects = new List<T>();
    }

    public IEnumerable<T> Objects => _objects;
    
    public T Create(ReferenceRect rect)
    {
        var instanceOfObject = _factory.Create(rect);
        _objects.Add(instanceOfObject);
        
        return instanceOfObject;
    }
}