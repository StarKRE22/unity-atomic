using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic
{
    internal static class EntityScanner
    {
        private static readonly Dictionary<Type, IEnumerable<int>> scannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ValueInfo>> scannedValues = new();

        internal static IEnumerable<int> ScanTypes(Type target)
        {
            if (scannedTypes.TryGetValue(target, out var types))
            {
                return types;
            }

            types = ScanTypesInternal(target);
            scannedTypes.Add(target, types);
            return types;
        }

        internal static IEnumerable<ValueInfo> ScanValues(Type target)
        {
            if (scannedValues.TryGetValue(target, out var values))
            {
                return values;
            }

            values = ScanValuesInternal(target);
            scannedValues.Add(target, values);
            return values;
        }

        private static IEnumerable<int> ScanTypesInternal(Type target)
        {
            IsAttribute attribute = target.GetCustomAttribute<IsAttribute>();

            if (attribute != null)
            {
                return attribute.indexes;
            }

            return Array.Empty<int>();
        }

        private static IEnumerable<ValueInfo> ScanValuesInternal(Type target)
        {
            List<ValueInfo> values = new List<ValueInfo>();

            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                GetAttribute attribute = field.GetCustomAttribute<GetAttribute>();

                if (attribute != null)
                {
                    values.Add(new ValueInfo(attribute.index, field.GetValue));
                }
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];

                GetAttribute getAttribute = property.GetCustomAttribute<GetAttribute>();
                if (getAttribute != null)
                {
                    values.Add(new ValueInfo(getAttribute.index, property.GetValue));
                }

                AtomicGetterAttribute getterAttribute = property.GetCustomAttribute<AtomicGetterAttribute>();
                if (getterAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicFunctionProvider(property.GetMethod);
                    values.Add(new ValueInfo(getterAttribute.index, valueProvider));
                }

                AtomicSetterAttribute setterAttribute = property.GetCustomAttribute<AtomicSetterAttribute>();
                if (setterAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicActionProvider(property.SetMethod);
                    values.Add(new ValueInfo(setterAttribute.index, valueProvider));
                }
            }

            MethodInfo[] methods = ReflectionUtils.GetMethods(target);
            for (int i = 0, count = methods.Length; i < count; i++)
            {
                MethodInfo method = methods[i];

                AtomicActionAttribute actionAttribute = method.GetCustomAttribute<AtomicActionAttribute>();
                if (actionAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicActionProvider(method);
                    values.Add(new ValueInfo(actionAttribute.index, valueProvider));
                }

                AtomicFunctionAttribute functionAttribute = method.GetCustomAttribute<AtomicFunctionAttribute>();
                if (functionAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicFunctionProvider(method);
                    values.Add(new ValueInfo(functionAttribute.index, valueProvider));
                }
            }

            return values;
        }
    }
}