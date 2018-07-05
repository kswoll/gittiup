using System.Threading.Tasks;
using System.Windows.Input;

namespace Movel.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
    }

    public interface IAsyncCommand<in TInput> : ICommand
    {
        Task ExecuteAsync(TInput parameter);
    }

    public interface IAsyncCommand<in TInput, TOutput> : ICommand
    {
        Task<TOutput> ExecuteAsync(TInput parameter);
    }
}