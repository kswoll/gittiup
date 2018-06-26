using System;
using System.Collections.Generic;
using Movel.Utils;

namespace Movel.Tests.Utils
{
    public static class DisposableExtensions
    {
        public static void DisposeWith(this IDisposable disposable, IDisposableHost host)
        {
            host.AddDisposable(disposable);
        }

        public static void DisposeWith<T>(this ICollection<T> collection, IDisposableHost host)
            where T : IDisposable
        {
            host.AddDisposable(() =>
            {
                foreach (var item in collection)
                {
                    item?.Dispose();
                }
            });
        }
    }
}