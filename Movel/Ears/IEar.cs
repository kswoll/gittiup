using System;

namespace Movel.Ears
{
    public interface IEar<T> : IDisposable
    {
        T Value { get; }
        event EarValueChangedHandler<T> ValueChanged;
    }
}