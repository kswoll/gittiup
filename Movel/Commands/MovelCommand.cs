using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Movel.Ears;
using Movel.Utils;

namespace Movel.Commands
{
    public class MovelCommand<TInput, TOutput> : IAsyncCommand<TInput, TOutput>, IAsyncCommand<TInput>, IDisposable, IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<TInput, Task<TOutput>> execute;
        private readonly IEar<bool> canExecute;

        public MovelCommand(Func<TInput, Task<TOutput>> execute, IEar<bool> canExecute)
        {
            this.execute = execute ?? (input => Task.FromResult(default(TOutput)));
            this.canExecute = canExecute ?? new ConstantEar<bool>(true);
            this.canExecute.ValueChanged += EarOnValueChanged;
        }

        private void EarOnValueChanged(Ear<bool> ear, bool oldvalue, bool newvalue)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute.Value;
        }

        public async Task<TOutput>ExecuteAsync(TInput parameter = default)
        {
            return await execute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((TInput)(parameter ?? default(TInput))).RunAsync();
        }

        public void Dispose()
        {
            canExecute.Dispose();
        }

        async Task IAsyncCommand.ExecuteAsync()
        {
            await ExecuteAsync();
        }

        async Task IAsyncCommand<TInput>.ExecuteAsync(TInput parameter)
        {
            await ExecuteAsync(parameter);
        }

        async Task<TOutput> IAsyncCommand<TInput, TOutput>.ExecuteAsync(TInput parameter)
        {
            return await ExecuteAsync(parameter);
        }
    }
}