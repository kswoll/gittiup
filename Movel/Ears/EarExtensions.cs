using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Movel.Utils;

namespace Movel.Ears
{
    public static class EarExtensions
    {
        public static Ear<TOutput> Listen<TOutput>(this INotifyPropertyChanged target, params PropertyInfo[] path)
        {
            return new Ear<TOutput>(target, path);
        }

        public static Ear<TOutput> Listen<T, TOutput>(this T target, Expression<Func<T, TOutput>> path)
            where T : INotifyPropertyChanged
        {
            var propertyInfos = ConvertLambdaToPropertyArray(path).ToArray();

            return new Ear<TOutput>(target, propertyInfos);
        }

        private static IEnumerable<PropertyInfo> ConvertLambdaToPropertyArray(LambdaExpression path)
        {
            Expression body = path.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                body = ((UnaryExpression)body).Operand;
            }

            var member = body as MemberExpression;
            if (member == null)
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
    }
}