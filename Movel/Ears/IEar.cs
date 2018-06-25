namespace Movel.Ears
{
    public interface IEar<T>
    {
        T Value { get; }
        event EarValueChangedHandler<T> ValueChanged;
    }
}