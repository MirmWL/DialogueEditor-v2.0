using System.Collections.Generic;

public class LineFactory
{

    public Line Create(IPosition first, IPosition second)
    {
        return new Line(new KeyValuePair<IPosition, IPosition>(first, second));
    }
}