namespace Merge2D.Source.Game
{
    public interface IProvider<T>
    {
        T Value { get; set; }
    }
}