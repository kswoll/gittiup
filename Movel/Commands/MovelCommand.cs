using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Movel.Ears;

namespace Movel.Commands
{
    public class MovelCommand<TInput, TOutput> : IAsyncCommand<TInput, TOutput>, IAsyncCommand<TInput>, ICommand<TInput, TOutput>, ICommand<TInput>, IDisposable
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<TInput, Task<TOutput>> execute;
        private readonly IEar<bool> canExecute;

        public MovelCommand(Func<TInput, Task<TOutput>> execute, IEar<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
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

        public TOutput Execute(TInput parameter = default)
        {
            var locker = new ManualResetEvent(false);
            TOutput result = default;
            Task.Run(async () =>
            {
                result = await execute(parameter);
                locker.Set();
            });
            locker.WaitOne();
            return result;
        }

        public async Task<TOutput>ExecuteAsync(TInput parameter = default)
        {
            return await execute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((TInput)parameter);
        }

        public void Dispose()
        {
            canExecute.Dispose();
        }

        async Task<object> IAsyncCommand.ExecuteAsync(object parameter)
        {
            return await ExecuteAsync((TInput)parameter);
        }

        Task IAsyncCommand<TInput>.Execute(TInput parameter)
        {
            return ExecuteAsync(parameter);
        }

        void ICommand<TInput>.Execute(TInput parameter)
        {
            Execute(parameter);
        }
    }
}