namespace EditorInput
{
    public class PredicateDependentInput : IInput
    {
        private readonly IPredicate _predicate;
        private readonly IInput _input;

        public PredicateDependentInput(IPredicate predicate, IInput input)
        {
            _predicate = predicate;
            _input = input;
        }

        public bool HasInput()
        {
            return _predicate.Execute() && _input.HasInput();
        }
    }
}
