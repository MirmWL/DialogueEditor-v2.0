using System.Collections.Generic;
using UnityEditor;

public class Line : IUpdate
{
    private readonly KeyValuePair<IPosition, IPosition> _connection;
    
    public Line(KeyValuePair<IPosition, IPosition> connection)
    {
        _connection = connection;
    }

    public void Update()
    {
        var firstPoint = _connection.Key.Get(); 
        var secondPoint = _connection.Value.Get();
        
        Handles.DrawLine(firstPoint, secondPoint);
    }
}