public interface INodeFactory<out T> where T : INode
{
    T Create(ReferenceRect rect, ReferenceRect dragRect);
}