using System;
using System.Threading.Tasks;

namespace Movel.Utils
{
    public interface IMovelDispatcher
    {
        Task<T> DispatchAsync<T>(Func<Task<T>> task);
        Task DispatchAsync(Func<Task> task);
    }
}