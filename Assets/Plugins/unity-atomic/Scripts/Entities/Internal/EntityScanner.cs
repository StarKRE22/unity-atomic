using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic
{
    internal static class EntityScanner
    {
        private static readonly Dictionary<Type, (IEnumerable<string>, IEnumerable<int>)> scannedTypes = new();
        private static readonly Dictionary<Type, (IEnumerable<ValueKeyInfo>, IEnumerable<ValueIndexInfo>)> scannedValues = new();

        internal static (IEnumerable<string>, IEnumerable<int>) ScanTypes(Type target)
        {
            if (scannedTypes.TryGetValue(target, out var types))
            {
                return types;
            }

            types = ScanTypesInternal(target);
            scannedTypes.Add(target, types);
            return types;
        }

        internal static (IEnumerable<ValueKeyInfo>, IEnumerable<ValueIndexInfo>) ScanValues(Type target)
        {
            if (scannedValues.TryGetValue(target, out var values))
            {
                return values;
            }

            values = ScanValuesInternal(target);
            scannedValues.Add(target, values);
            return values;
        }

        private static (IEnumerable<string>, IEnumerable<int>) ScanTypesInternal(Type target)
        {
            IsAttribute attribute = target.GetCustomAttribute<IsAttribute>();

            if (attribute != null)
            {
                return (attribute.Types, attribute.Indexes);
            }

            return (Array.Empty<string>(), Array.Empty<int>());
        }

        private static (IEnumerable<ValueKeyInfo>, IEnumerable<ValueIndexInfo>) ScanValuesInternal(Type target)
        {
            List<ValueKeyInfo> dataKeys = new List<ValueKeyInfo>();
            List<ValueIndexInfo> dataIndexes = new List<ValueIndexInfo>();

            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                GetAttribute attribute = field.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                Func<object, object> valueProvider = field.GetValue;

                string key = attribute.key;
                if (key != null)
                {
                    dataKeys.Add(new ValueKeyInfo(key, valueProvider));
                }

                int index = attribute.index;
                if (index >= 0)
                {
                    dataIndexes.Add(new ValueIndexInfo(index, valueProvider));
                }
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];

                GetAttribute getAttribute = property.GetCustomAttribute<GetAttribute>();
                if (getAttribute != null)
                {
                    Func<object, object> valueProvider = property.GetValue;
                    
                    string key = getAttribute.key;
                    if (key != null)
                    {
                        dataKeys.Add(new ValueKeyInfo(key, valueProvider));
                    }

                    int index = getAttribute.index;
                    if (index >= 0)
                    {
                        dataIndexes.Add(new ValueIndexInfo(index, valueProvider));
                    }
                }

                AtomicGetterAttribute getterAttribute = property.GetCustomAttribute<AtomicGetterAttribute>();
                if (getterAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicFunctionProvider(property.GetMethod);
                  
                    string key = getterAttribute.key;
                    if (key != null)
                    {
                        dataKeys.Add(new ValueKeyInfo(key, valueProvider));
                    }

                    int index = getterAttribute.index;
                    if (index >= 0)
                    {
                        dataIndexes.Add(new ValueIndexInfo(index, valueProvider));
                    }
                }

                AtomicSetterAttribute setterAttribute = property.GetCustomAttribute<AtomicSetterAttribute>();
                if (setterAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicActionProvider(property.SetMethod);
                  
                    string key = setterAttribute.key;
                    if (key != null)
                    {
                        dataKeys.Add(new ValueKeyInfo(key, valueProvider));
                    }

                    int index = setterAttribute.index;
                    if (index >= 0)
                    {
                        dataIndexes.Add(new ValueIndexInfo(index, valueProvider));
                    }
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
                    dataKeys.Add(new ValueKeyInfo(actionAttribute.key, valueProvider));
                    dataIndexes.Add(new ValueIndexInfo(actionAttribute.index, valueProvider));
                }

                AtomicFunctionAttribute functionAttribute = method.GetCustomAttribute<AtomicFunctionAttribute>();
                if (functionAttribute != null)
                {
                    Func<object, object> valueProvider = ReflectionUtils.CreateAtomicFunctionProvider(method);
                    dataKeys.Add(new ValueKeyInfo(functionAttribute.key, valueProvider));
                    dataIndexes.Add(new ValueIndexInfo(functionAttribute.index, valueProvider));
                }
            }

            return (dataKeys, dataIndexes);
        }
    }
}