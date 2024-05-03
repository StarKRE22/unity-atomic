using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UseDeconstruction

namespace Atomic
{
    internal static class EntityBaker
    {
        private static readonly Dictionary<Type, EntityInfo> compiledEntities = new();

        public static EntityInfo Bake(Type objectType)
        {
            if (compiledEntities.TryGetValue(objectType, out EntityInfo objectInfo))
            {
                return objectInfo;
            }

            objectInfo = CompileEntityInternal(objectType);
            compiledEntities.Add(objectType, objectInfo);
            return objectInfo;
        }

        private static EntityInfo CompileEntityInternal(Type objectType)
        {
            HashSet<string> typeKeys = new HashSet<string>();
            List<int> typeIndexes = new List<int>();

            List<ValueKeyInfo> dataKeys = new List<ValueKeyInfo>();
            List<ValueIndexInfo> dataIndexes = new List<ValueIndexInfo>();

            foreach (Type @interface in objectType.GetInterfaces())
            {
                if (@interface == typeof(IAtomicEntity) || @interface == typeof(IAtomicObject))
                {
                    continue;
                }
                
                (IEnumerable<string>, IEnumerable<int>) types = EntityScanner.ScanTypes(@interface);
                typeKeys.UnionWith(types.Item1);
                typeIndexes.AddRange(types.Item2);

                (IEnumerable<ValueKeyInfo>, IEnumerable<ValueIndexInfo>) values = EntityScanner.ScanValues(@interface);
                dataKeys.AddRange(values.Item1);
                dataIndexes.AddRange(values.Item2);
            }

            while (objectType != typeof(AtomicEntity) && objectType != typeof(AtomicObject) && objectType != null)
            {
                (IEnumerable<string>, IEnumerable<int>) types = EntityScanner.ScanTypes(objectType);
                typeKeys.UnionWith(types.Item1);
                typeIndexes.AddRange(types.Item2);

                (IEnumerable<ValueKeyInfo>, IEnumerable<ValueIndexInfo>) values = EntityScanner.ScanValues(objectType);
                dataKeys.AddRange(values.Item1);
                dataIndexes.AddRange(values.Item2);

                objectType = objectType!.BaseType;
            }

            return new EntityInfo(
                typeKeys.ToArray(),
                typeIndexes.ToArray(),
                dataKeys.ToArray(),
                dataIndexes.ToArray()
            );
        }
    }
}