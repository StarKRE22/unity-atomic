using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Atomic.Objects
{
    internal static class AtomicScanner
    {
        private static readonly Dictionary<Type, IEnumerable<string>> scannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ValueInfo>> scannedValues = new();

        internal static IEnumerable<string> ScanTypes(Type target)
        {
            if (scannedTypes.TryGetValue(target, out IEnumerable<string> types))
            {
                return types;
            }

            types = ScanTypesInternal(target);
            scannedTypes.Add(target, types);
            return types;
        }

        internal static IEnumerable<ValueInfo> ScanValues(Type target)
        {
            if (scannedValues.TryGetValue(target, out IEnumerable<ValueInfo> references))
            {
                return references;
            }

            references = ScanValuesInternal(target);
            scannedValues.Add(target, references);
            return references;
        }

        private static IEnumerable<string> ScanTypesInternal(Type target)
        {
            IsAttribute attribute = target.GetCustomAttribute<IsAttribute>();

            if (attribute != null)
            {
                return attribute.Types;
            }

            return Array.Empty<string>();
        }

        private static IEnumerable<ValueInfo> ScanValuesInternal(Type target)
        {
            var result = new List<ValueInfo>();

            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                GetAttribute attribute = field.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ValueInfo(attribute.Id, field.GetValue);
                result.Add(reference);
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];
                GetAttribute attribute = property.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ValueInfo(attribute.Id, property.GetValue);
                result.Add(reference);
            }

            return result;
        }
    }
}