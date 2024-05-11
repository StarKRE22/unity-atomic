using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    internal static class ObjectScanner
    {
        private static readonly Dictionary<Type, IEnumerable<int>> scannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ReferenceInfo>> scannedReferences = new();
        private static readonly Dictionary<Type, IEnumerable<BehaviourInfo>> scannedBehaviours = new();

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

        internal static IEnumerable<ReferenceInfo> ScanReferences(Type target)
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

        private static IEnumerable<ReferenceInfo> ScanReferencesInternal(Type target)
        {
            List<ReferenceInfo> references = new List<ReferenceInfo>();
            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                ReferenceAttribute attribute = field.GetCustomAttribute<ReferenceAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                int index = attribute.id;
                if (index >= 0)
                {
                    references.Add(new ReferenceInfo(index, field.GetValue));
                }
            }

            PropertyInfo[] properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];

                ReferenceAttribute referenceAttribute = property.GetCustomAttribute<ReferenceAttribute>();
                if (referenceAttribute != null)
                {
                    int index = referenceAttribute.id;
                    if (index >= 0)
                    {
                        references.Add(new ReferenceInfo(index, property.GetValue));
                    }
                }
            }

            return references;
        }

        public static IEnumerable<BehaviourInfo> ScanBehaviours(Type target)
        {
            if (scannedBehaviours.TryGetValue(target, out var behaviours))
            {
                return behaviours;
            }

            behaviours = ScanBehavioursInternal(target);
            scannedBehaviours.Add(target, behaviours);
            return behaviours;
        }

        private static IEnumerable<BehaviourInfo> ScanBehavioursInternal(Type target)
        {
            List<BehaviourInfo> behaviours = new List<BehaviourInfo>();
            FieldInfo[] fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                if (field.IsDefined(typeof(BehaviourAttribute)) && 
                    typeof(IAtomicObject.IBehaviour).IsAssignableFrom(field.FieldType))
                {
                    behaviours.Add(new BehaviourInfo(field.GetValue));
                    
                }
            }

            return behaviours;
        }
    }
}