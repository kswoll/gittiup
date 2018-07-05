using System;
using System.Threading.Tasks;
using System.Windows;
using Movel.Utils;

namespace Movel.Wpf
{
    public class WpfMovelDispatcher : IMovelDispatcher
    {
        public async Task<T> DispatchAsync<T>(Func<Task<T>> task)
        {
            var dispatcher = Application.Current.Dispatcher;
            return await dispatcher.Invoke(task);
        }

        public async Task DispatchAsync(Func<Task> task)
        {
            var dispatcher = Application.Current.Dispatcher;
            await dispatcher.Invoke(task);
        }
    }
}