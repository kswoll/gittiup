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
        public static MovelCommand<TInput, TOutput> CommandAsync<TInput, TOutput>(Func<TInput, Task<TOutput>> execute = null, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, TOutput>(execute, canExecute.ToEar());
        }

        public static MovelCommand<Nothing, TOutput> CommandAsync<TOutput>(Func<Task<TOutput>> execute = null, Func<Nothing, bool> canExecute = null)
        {
            return new MovelCommand<Nothing, TOutput>(_ => execute(), canExecute.ToEar());
        }

        public static MovelCommand<Nothing, Nothing> CommandAsync(Func<Task> execute = null, Func<Nothing, bool> canExecute = null)
        {
            return new MovelCommand<Nothing, Nothing>(
                _ =>
                {
                    execute();
                    return Task.FromResult(Nothing.Value);
                },
                canExecute.ToEar());
        }

        public static MovelCommand<TInput, Nothing> CommandAsync<TInput>(Func<TInput, Task> execute = null, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, Nothing>(
                async x =>
                {
                    await execute(x);
                    return Nothing.Value;
                },
                canExecute.ToEar());
        }

        public static MovelCommand<TInput, TOutput> Command<TInput, TOutput>(Func<TInput, TOutput> execute = null, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, TOutput>(x => Task.FromResult(execute(x)), canExecute.ToEar());
        }

        public static MovelCommand<TInput, Nothing> Command<TInput>(Action<TInput> execute = null, Func<TInput, bool> canExecute = null)
        {
            return new MovelCommand<TInput, Nothing>(
                x =>
                {
                    execute(x);
                    return Task.FromResult(Nothing.Value);
                },
                canExecute.ToEar());
        }

        public static MovelCommand<Nothing, Nothing> CommandAsync<TCanExecuteTarget>(TCanExecuteTarget canExecuteTarget, Expression<Func<TCanExecuteTarget, bool>> canExecute)
            where TCanExecuteTarget : INotifyPropertyChanged
        {
            return new MovelCommand<Nothing, Nothing>(null, canExecuteTarget.Listen(canExecute));
        }

        public static MovelCommand<TInput, TOutput> CommandAsync<TInput, TOutput, TCanExecuteTarget>(Func<TInput, Task<TOutput>> execute, TCanExecuteTarget canExecuteTarget, Expression<Func<TCanExecuteTarget, bool>> canExecute)
            where TCanExecuteTarget : INotifyPropertyChanged
        {
            return new MovelCommand<TInput, TOutput>(execute, canExecuteTarget.Listen(canExecute));
        }

        // Disposableb based

        public static MovelCommand<TInput, TOutput> CreateCommandAsync<TInput, TOutput>(this IDisposableHost host, Func<TInput, Task<TOutput>> execute = null, Func<TInput, bool> canExecute = null)
        {
            return CommandAsync(execute, canExecute).DisposeWith(host);
        }

        public static MovelCommand<Nothing, TOutput> CreateCommandAsync<TOutput>(this IDisposableHost host, Func<Task<TOutput>> execute = null, Func<Nothing, bool> canExecute = null)
        {
            return CommandAsync(execute, canExecute).DisposeWith(host);
        }

        public static MovelCommand<Nothing, Nothing> CreateCommandAsync(this IDisposableHost host, Func<Task> execute = null, Func<Nothing, bool> canExecute = null)
        {
            return CommandAsync(execute, canExecute).DisposeWith(host);
        }

        public static MovelCommand<TInput, Nothing> CreateCommandAsync<TInput>(this IDisposableHost host, Func<TInput, Task> execute = null, Func<TInput, bool> canExecute = null)
        {
            return CommandAsync(execute, canExecute);
        }

        public static MovelCommand<TInput, TOutput> CreateCommand<TInput, TOutput>(this IDisposableHost host, Func<TInput, TOutput> execute = null, Func<TInput, bool> canExecute = null)
        {
            return Command(execute, canExecute).DisposeWith(host);
        }

        public static MovelCommand<TInput, Nothing> CreateCommand<TInput>(this IDisposableHost host, Action<TInput> execute = null, Func<TInput, bool> canExecute = null)
        {
            return Command(execute, canExecute).DisposeWith(host);
        }

        public static MovelCommand<Nothing, Nothing> CreateCommandAsync<TCanExecuteTarget>(this IDisposableHost host, TCanExecuteTarget canExecuteTarget, Expression<Func<TCanExecuteTarget, bool>> canExecute)
            where TCanExecuteTarget : INotifyPropertyChanged
        {
            return CommandAsync(canExecuteTarget, canExecute).DisposeWith(host);
        }

        public static MovelCommand<TInput, TOutput> CreateCommandAsync<TInput, TOutput, TCanExecuteTarget>(this IDisposableHost host, Func<TInput, Task<TOutput>> execute, TCanExecuteTarget canExecuteTarget, Expression<Func<TCanExecuteTarget, bool>> canExecute)
            where TCanExecuteTarget : INotifyPropertyChanged
        {
            return CommandAsync(execute, canExecuteTarget, canExecute).DisposeWith(host);
        }
    }
}