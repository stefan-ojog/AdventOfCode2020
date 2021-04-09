namespace AdventOfCode.Base
{
    public interface IInputTransform<T> : IInputTransform
    {
        T Create(string[] inputLines);
    }

    public interface IInputTransform
    {
    }
}
