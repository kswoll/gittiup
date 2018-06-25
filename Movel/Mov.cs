using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Movel.Commands;
using Movel.Ears;
using Movel.Utils;

namespace Movel
{
    public static class Mov
    {
        public static MovelCommand<TInput, TOutput> CommandAsync<TInput, TOutput>(Func<TInput, Task<TOutput>> execute, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, TOutput>(execute, canExecute.ToEar());
        }

        public static MovelCommand<TInput, Nothing> CommandAsync<TInput>(Func<TInput, Task> execute, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, Nothing>(
                async x =>
                {
                    await execute(x);
                    return Nothing.Value;
                },
                canExecute.ToEar());
        }

        public static MovelCommand<TInput, TOutput> Command<TInput, TOutput>(Func<TInput, TOutput> execute, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, TOutput>(x => Task.FromResult(execute(x)), canExecute.ToEar());
        }

        public static MovelCommand<TInput, Nothing> Command<TInput>(Action<TInput> execute, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, Nothing>(
                x =>
                {
                    execute(x);
                    return Task.FromResult(Nothing.Value);
                },
                canExecute.ToEar());
        }

        public static MovelCommand<TInput, TOutput> CommandAsync<TInput, TOutput, TCanExecuteTarget>(Func<TInput, Task<TOutput>> execute, TCanExecuteTarget canExecuteTarget, Expression<Func<TCanExecuteTarget, bool>> canExecute)
            where TCanExecuteTarget : INotifyPropertyChanged
        {
            return new MovelCommand<TInput, TOutput>(execute, canExecuteTarget.Listen(canExecute));
        }
    }
}