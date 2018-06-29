using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Movel.Utils;

namespace Movel.Ears
{
    public static class EarExtensions
    {
        public static Ear<TOutput> Listen<TOutput>(this INotifyPropertyChanged target, params PropertyInfo[] path)
        {
            var ear = new Ear<TOutput>(target, path);
            if (target is IDisposableHost host)
            {
                host.AddDisposable(ear);
            }
            return ear;
        }

        public static Ear<TOutput> Listen<T, TOutput>(this T target, Expression<Func<T, TOutput>> path)
            where T : INotifyPropertyChanged
        {
            var propertyInfos = ConvertLambdaToPropertyArray(path).ToArray();
            return target.Listen<TOutput>(propertyInfos);
        }

        private static IEnumerable<PropertyInfo> ConvertLambdaToPropertyArray(LambdaExpression path)
        {
            var body = path.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                body = ((UnaryExpression)body).Operand;
            }

            if (!(body is MemberExpression member))
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            return member
                .SelectRecursive(x => x.Expression as MemberExpression)
                .Select(x => x.Member as PropertyInfo ?? throw new ArgumentException("Only properties (not fields) are allowed", nameof(path))).Reverse();
        }

        private static IEnumerable<T> SelectRecursive<T>(this T obj, Func<T, T> next) where T : class
        {
            T current = obj;
            while (current != null)
            {
                yield return current;
                current = next(current);
            }
        }

        public static IEar<bool> ToEar<TInput>(this Func<TInput, bool> canExecute)
        {
            return new ConstantEar<bool>(true);
        }

        public static void Then<T>(this Ear<T> ear, EarValueChangedHandler<T> handler)
        {
//            var semaphore = new Semaphore(1, 1);
            var instrumentedHandler = new EarValueChangedHandler<T>((e, oldValue, newValue) =>
            {
//                if (semaphore.WaitOne())
//                {
                    handler(e, oldValue, newValue);
//                }
            });
            ear.ValueChanged += instrumentedHandler;
            ear.AddDisposable(() => ear.ValueChanged -= handler);
        }

        public static void Then<T>(this Ear<T> ear, Action handler)
        {
            ear.Then((_, __, ___) => handler());
        }
    }
}