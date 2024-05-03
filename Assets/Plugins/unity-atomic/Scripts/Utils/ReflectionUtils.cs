using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Atomic
{
    internal static class ReflectionUtils
    {
        private static readonly Dictionary<Type, FieldInfo[]> fieldsMap = new();
        private static readonly Dictionary<Type, PropertyInfo[]> propertiesMap = new();
        private static readonly Dictionary<Type, MethodInfo[]> methodsMap = new();

        internal static FieldInfo[] GetFields(Type targetType)
        {
            if (fieldsMap.TryGetValue(targetType, out var fields))
            {
                return fields;
            }
        
            fields = targetType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            fieldsMap.Add(targetType, fields);
            return fields;
        }

        internal static PropertyInfo[] GetProperties(Type targetType)
        {
            if (propertiesMap.TryGetValue(targetType, out var properties))
            {
                return properties;
            }
        
            properties = targetType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );
            
            propertiesMap.Add(targetType, properties);
            return properties;
        }

        internal static MethodInfo[] GetMethods(Type targetType)
        {
            if (methodsMap.TryGetValue(targetType, out var methods))
            {
                return methods;
            }
        
            methods = targetType.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            methodsMap.Add(targetType, methods);
            return methods;
        }
        
        internal static Func<object, object> CreateAtomicActionProvider(MethodInfo method)
        {
            Type[] paramTypes = method!
                .GetParameters()
                .Select(it => it.ParameterType)
                .ToArray();

            int paramsCount = paramTypes.Length;

            if (paramsCount == 0)
            {
                Type delegateType = typeof(Action);
                return target => new AtomicAction((Action) Delegate.CreateDelegate(delegateType, target, method));
            }

            if (paramsCount == 1)
            {
                Type delegateType = typeof(Action<>).MakeGenericType(paramTypes[0]);
                Type actionType = typeof(AtomicAction<>).MakeGenericType(paramTypes[0]);
                return target =>
                    Activator.CreateInstance(actionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            if (paramsCount == 2)
            {
                Type delegateType = typeof(Action<,>).MakeGenericType(paramTypes[0], paramTypes[1]);
                Type actionType = typeof(AtomicAction<,>).MakeGenericType(paramTypes[0], paramTypes[1]);
                return target =>
                    Activator.CreateInstance(actionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            if (paramsCount == 3)
            {
                Type delegateType = typeof(Action<,,>).MakeGenericType(paramTypes[0], paramTypes[1], paramTypes[2]);
                Type actionType = typeof(AtomicAction<,,>).MakeGenericType(paramTypes[0], paramTypes[1], paramTypes[2]);
                return target =>
                    Activator.CreateInstance(actionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            throw new Exception($"Can't create Action delegate with parameters count: {paramsCount}!");
        }
        
        internal static Func<object, object> CreateAtomicFunctionProvider(MethodInfo method)
        {
            Type returnType = method.ReturnType;

            Type[] paramTypes = method!
                .GetParameters()
                .Select(it => it.ParameterType)
                .ToArray();

            int paramsCount = paramTypes.Length;

            if (paramsCount == 0)
            {
                Type delegateType = typeof(Func<>).MakeGenericType(returnType);
                Type functionType = typeof(AtomicFunction<>).MakeGenericType(returnType);
                return target =>
                    Activator.CreateInstance(functionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            if (paramsCount == 1)
            {
                Type delegateType = typeof(Func<,>).MakeGenericType(paramTypes[0], returnType);
                Type functionType = typeof(AtomicAction<,>).MakeGenericType(paramTypes[0], returnType);
                return target =>
                    Activator.CreateInstance(functionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            if (paramsCount == 2)
            {
                Type delegateType = typeof(Func<,,>).MakeGenericType(paramTypes[0], paramTypes[1], returnType);
                Type functionType =
                    typeof(AtomicFunction<,,>).MakeGenericType(paramTypes[0], paramTypes[1], returnType);
                return target =>
                    Activator.CreateInstance(functionType, Delegate.CreateDelegate(delegateType, target, method));
            }

            throw new Exception($"Can't create Func delegate with parameters count: {paramsCount}!");
        }
    }
}