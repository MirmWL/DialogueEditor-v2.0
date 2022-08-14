namespace EditorInput
{
    public class PredicateDependentInput : IInput
    {
        private readonly ICondition _condition;
        private readonly IInput _input;

        public PredicateDependentInput(ICondition condition, IInput input)
        {
            _condition = condition;
            _input = input;
        }

        public bool HasInput()
        {
            return _condition.Execute() && _input.HasInput();
        }
    }
}
