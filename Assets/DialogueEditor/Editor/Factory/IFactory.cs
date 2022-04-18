public interface IFactory<out T>
{
    T Create(ReferenceRect rect);
}