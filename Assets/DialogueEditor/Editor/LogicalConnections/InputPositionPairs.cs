using System.Collections.Generic;
using System.Linq;
using EditorInput;

public class InputPositionPairs
{
    private readonly Dictionary<IInput, IPosition> _inputPositionPairs;

    public InputPositionPairs(Dictionary<IInput, IPosition> inputPositionPairs)
    {
        _inputPositionPairs = inputPositionPairs;
    }

    public IInput Input(IPosition position)
    {
        return _inputPositionPairs.FirstOrDefault(s => s.Value == position).Key;
    }
    
    public IPosition Position(IInput input)
    {
        return _inputPositionPairs.FirstOrDefault(s => s.Key == input).Value;
    }
}