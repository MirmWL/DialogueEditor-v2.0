public interface INodeFactory
{
    INode Create(IRect rect, IRect dragRect, IPredicate pinnedPredicate, IInput clickInput);
}