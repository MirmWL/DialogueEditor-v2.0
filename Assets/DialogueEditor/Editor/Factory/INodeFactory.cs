using EditorInput;

public interface INodeFactory
{
    INode Create(IRect rect, IRect dragRect,IRect createConnectionButtonRect, IPredicate pinnedPredicate, IInput clickInput);
}