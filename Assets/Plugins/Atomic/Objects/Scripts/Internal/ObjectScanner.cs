using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    internal static class ObjectScanner
    {
        private static readonly Dictionary<Type, IEnumerable<int>> scannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ValueInfo>> scannedReferences = new();
        private static readonly Dictionary<Type, IEnumerable<LogicInfo>> scannedBehaviours = new();

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

        internal static IEnumerable<ValueInfo> ScanReferences(Type target)
        {
            if (scannedReferences.TryGetValue(target, out var values))
            {
                return values;
            }

            values = ScanReferencesInternal(target);
            scannedReferences.Add(target, values);
            return values;
        }

        private static IEnumerable<int> ScanTypesInternal(Type target)
        {
            TagsAttribute attribute = target.GetCustomAttribute<TagsAttribute>();
            return attribute != null ? attribute.typeIds : Array.Empty<int>();
        }

        private static IEnumerable<ValueInfo> ScanReferencesInternal(Type target)
        {
            List<ValueInfo> references = new List<ValueInfo>();
            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                ValueAttribute attribute = field.GetCustomAttribute<ValueAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                int index = attribute.id;
                if (index >= 0)
                {
                    references.Add(new ValueInfo(index, field.GetValue));
                }
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];

                ValueAttribute referenceAttribute = property.GetCustomAttribute<ValueAttribute>();
                if (referenceAttribute != null)
                {
                    int index = referenceAttribute.id;
                    if (index >= 0)
                    {
                        references.Add(new ValueInfo(index, property.GetValue));
                    }
                }
            }

            return references;
        }

        public static IEnumerable<LogicInfo> ScanBehaviours(Type target)
        {
            if (scannedBehaviours.TryGetValue(target, out var behaviours))
            {
                return behaviours;
            }

            behaviours = ScanBehavioursInternal(target);
            scannedBehaviours.Add(target, behaviours);
            return behaviours;
        }

        private static IEnumerable<LogicInfo> ScanBehavioursInternal(Type target)
        {
            List<LogicInfo> behaviours = new List<LogicInfo>();
            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                if (field.IsDefined(typeof(LogicAttribute)) && 
                    typeof(IAtomicLogic).IsAssignableFrom(field.FieldType))
                {
                    behaviours.Add(new LogicInfo(field.GetValue));
                    
                }
            }

            return behaviours;
        }
    }
}