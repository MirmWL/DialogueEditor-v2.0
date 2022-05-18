using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Predicates : IPredicate
{
    private readonly IPredicate[] _predicates;

    public Predicates(params IPredicate[] predicates)
    {
        _predicates = predicates;
    }

    public bool Execute()
    {
        return _predicates.All(predicate => predicate.Execute());
    }
}