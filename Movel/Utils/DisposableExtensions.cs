using System;
using System.Collections.Generic;

namespace Movel.Utils
{
    public static class DisposableExtensions
    {
        public static TDisposable DisposeWith<TDisposable>(this TDisposable disposable, IDisposableHost host)
            where TDisposable : IDisposable
        {
            host.AddDisposable(disposable);
            return disposable;
        }

        public static TCollection DisposeWith<TCollection, T>(this TCollection collection, IDisposableHost host)
            where TCollection : ICollection<T>
            where T : IDisposable
        {
            host.AddDisposable(() =>
            {
                foreach (var item in collection)
                {
                    item?.Dispose();
                }
            });
            return collection;
        }
    }
}