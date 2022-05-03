using System.Collections.Generic;

public class Updates
{
    private readonly List<IUpdate> _updates;
    private bool _isStopped;

    public Updates()
    {
        _updates = new List<IUpdate>();
    }

    public void Add(params IUpdate[] updates)
    {
        foreach (var update in updates)
            _updates.Add(update);
    }
    
    private void Remove(IUpdate update)
    {
        var index = _updates.FindIndex(s => s == update);
        var lastIndex = _updates.Count - 1;
        _updates[index] = _updates[lastIndex];
        _updates.RemoveAt(lastIndex);
    }

    public void Update()
    {
        if (_isStopped) 
            return;

        for (var i = 0; i < _updates.Count; i++)
            _updates[i].Update();
    }

    public void Stop()
    {
        _isStopped = true;
    }

    public void Resume()
    {
        _isStopped = false;
    }

}