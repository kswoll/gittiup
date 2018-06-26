using System.Windows.Input;

namespace Movel.Commands
{
    public interface ICommand<in TInput, out TOutput> : ICommand
    {
        TOutput Execute(TInput parameter);
    }

    public interface ICommand<in TInput> : ICommand
    {
        void Execute(TInput parameter);
    }
}