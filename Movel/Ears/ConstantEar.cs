namespace Movel.Ears
{
    public class ConstantEar<T> : IEar<T>
    {
        public T Value { get; }
        public event EarValueChangedHandler<T> ValueChanged;

        public ConstantEar(T value)
        {
            Value = value;
        }
    }
}