namespace Movel.Ears
{
    public delegate void EarValueChangedHandler<TOutput>(Ear<TOutput> ear, TOutput oldValue, TOutput newValue);
}