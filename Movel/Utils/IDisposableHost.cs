using System;

namespace Movel.Utils
{
    public interface IDisposableHost : IDisposable
    {
        void Add(IDisposable disposable);
        void Remove(IDisposable disposable);
    }
}