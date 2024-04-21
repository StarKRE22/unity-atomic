using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    public static class ReflectionUtils
    {
        private static readonly Dictionary<Type, FieldInfo[]> fieldsMap = new();
        private static readonly Dictionary<Type, System.Reflection.PropertyInfo[]> propertiesMap = new();
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

        internal static System.Reflection.PropertyInfo[] GetProperties(Type targetType)
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
    }
}