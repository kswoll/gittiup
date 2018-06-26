using System;

namespace Movel.Utils
{
    public interface IDisposableHost : IDisposable
    {
        void AddDisposable(IDisposable disposable);
        void RemoveDisposable(IDisposable disposable);
    }
}