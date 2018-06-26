using System;

namespace Movel.Utils
{
    public static class IDisposableHostExtensions
    {
        public static void AddDisposable(this IDisposableHost host, Action onDispose)
        {
            host.AddDisposable(new ActionDisposable(onDispose));
        }
    }
}