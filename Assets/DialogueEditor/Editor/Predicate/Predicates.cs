using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Predicates : IPredicate
{
    private readonly IEnumerable<IPredicate> _predicates;

    public Predicates(IEnumerable<IPredicate> predicates)
    {
        _predicates = predicates;
    }

    public bool Execute()
    {
        return _predicates.All(predicate => predicate.Execute());
    }
}