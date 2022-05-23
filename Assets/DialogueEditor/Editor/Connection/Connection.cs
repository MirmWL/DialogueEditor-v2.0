using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Connection : IUpdate
{
    private readonly KeyValuePair<IRect, IRect> _connection;
    
    public Connection(KeyValuePair<IRect, IRect> connection)
    {
        _connection = connection;
    }

    public void Update()
    {
        var firstPoint = _connection.Key.Get().position;
        var secondPoint = _connection.Value.Get().position;
        
        Handles.DrawLine(firstPoint, secondPoint);
    }
}