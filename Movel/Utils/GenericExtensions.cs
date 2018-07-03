using System;
using System.Collections.Generic;

namespace Movel.Utils
{
    public static class GenericExtensions
    {
        public static Type GetListElementType(this Type listType)
        {
            return listType.GetGenericArgument(typeof(IList<>), 0);
        }

        public static bool IsGenericList(this Type listType)
        {
            return IsType(listType, typeof(IList<>));
        }

        public static Type GetGenericArgument(this Type type, Type typeToGetParameterFrom, int argumentIndex)
        {
            foreach (Type current in type.GetComposition())
            {
                type = current;
                if (!current.IsGenericType)
                {
                    continue;
                }

                Type genericTypeDefinition = current.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeToGetParameterFrom)
                {
                    break;
                }
            }

            Type[] genericArgs = type.GenericTypeArguments;
            Type result = genericArgs[argumentIndex];
            return result;
        }

        public static IEnumerable<Type> GetComposition(this Type type)
        {
            return type.GetAncestry(true);
        }

        public static IEnumerable<Type> GetAncestry(this Type type, bool includeInterfaces = false)
        {
            Type current = type;
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
            if (includeInterfaces)
            {
                IEnumerable<Type> interfaces = type.GetInterfaces();
                foreach (Type @interface in interfaces)
                {
                    yield return @interface;
                }
            }
        }

        public static bool IsType(this Type type, Type ancestor)
        {
            if (ancestor.IsInterface)
            {
                if (type.IsGenericType)
                {
                    Type current = type.GetGenericTypeDefinition();
                    if (current == ancestor)
                    {
                        return true;
                    }
                }

                Type[] interfaces = type.GetInterfaces();
                foreach (Type intf in interfaces)
                {
                    Type current = intf;
                    if (current.IsGenericType)
                    {
                        current = intf.GetGenericTypeDefinition();
                    }
                    if (current == ancestor)
                    {
                        return true;
                    }
                }
            }
            else
            {
                while (type != null)
                {
                    if (type.IsGenericType)
                    {
                        type = type.GetGenericTypeDefinition();
                    }
                    if (type == ancestor)
                    {
                        return true;
                    }
                    type = type.BaseType;
                }
            }
            return false;
        }
    }
}