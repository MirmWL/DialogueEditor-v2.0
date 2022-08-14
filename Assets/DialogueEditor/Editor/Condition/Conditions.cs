using System.Linq;

public class Conditions : ICondition
{
    private readonly ICondition[] _predicates;

    public Conditions(params ICondition[] predicates)
    {
        _predicates = predicates;
    }

    public bool Execute()
    {
        return _predicates.All(predicate => predicate.Execute());
    }
}