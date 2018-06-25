using System.Threading.Tasks;
using System.Windows.Input;

namespace Movel.Commands
{
    public interface ICommand<in TInput, TOutput> : ICommand
    {
        TOutput Execute(TInput parameter);
    }

    public interface ICommand<in TInput> : ICommand
    {
        void Execute(TInput parameter);
    }
}