using System;
using System.Collections.Generic;

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
            List<int> typeIndexes = new List<int>();
            List<ValueInfo> dataIndexes = new List<ValueInfo>();

            foreach (Type @interface in objectType.GetInterfaces())
            {
                if (@interface == typeof(IAtomicEntity) || @interface == typeof(IAtomicObject))
                {
                    continue;
                }
                
                var types = EntityScanner.ScanTypes(@interface);
                typeIndexes.AddRange(types);

                var values = EntityScanner.ScanValues(@interface);
                dataIndexes.AddRange(values);
            }

            while (objectType != typeof(AtomicEntity) && objectType != typeof(AtomicObject) && objectType != null)
            {
                var types = EntityScanner.ScanTypes(objectType);
                typeIndexes.AddRange(types);

                var values = EntityScanner.ScanValues(objectType);
                dataIndexes.AddRange(values);

                objectType = objectType!.BaseType;
            }

            return new EntityInfo(
                typeIndexes.ToArray(),
                dataIndexes.ToArray()
            );
        }
    }
}