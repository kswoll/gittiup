using System.Threading.Tasks;
using System.Windows.Input;

namespace Movel.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task<object> ExecuteAsync(object parameter = null);
    }

    public interface IAsyncCommand<in TInput, TOutput> : IAsyncCommand
    {
        Task<TOutput> ExecuteAsync(TInput parameter);
    }

    public interface IAsyncCommand<in TInput> : IAsyncCommand
    {
        Task Execute(TInput parameter);
    }
}