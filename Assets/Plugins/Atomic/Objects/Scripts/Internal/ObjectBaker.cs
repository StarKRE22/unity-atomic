using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    internal static class ObjectBaker
    {
        private static readonly Dictionary<Type, ObjectInfo> bakedObjects = new();

        internal static ObjectInfo Bake(Type objectType)
        {
            if (bakedObjects.TryGetValue(objectType, out ObjectInfo objectInfo))
            {
                return objectInfo;
            }

            objectInfo = BakeInternal(objectType);
            bakedObjects.Add(objectType, objectInfo);
            return objectInfo;
        }

        private static ObjectInfo BakeInternal(Type objectType)
        {
            List<int> types = new List<int>();
            List<ValueInfo> references = new List<ValueInfo>();
            List<LogicInfo> behaviours = new List<LogicInfo>();

            foreach (Type @interface in objectType.GetInterfaces())
            {
                if (@interface == typeof(IAtomicObject))
                {
                    continue;
                }

                types.AddRange(ObjectScanner.ScanTypes(@interface));
                references.AddRange(ObjectScanner.ScanReferences(@interface));
            }

            while (objectType != typeof(AtomicObject) && objectType != null)
            {
                types.AddRange(ObjectScanner.ScanTypes(objectType));
                references.AddRange(ObjectScanner.ScanReferences(objectType));
                behaviours.AddRange(ObjectScanner.ScanBehaviours(objectType));

                objectType = objectType!.BaseType;
            }

            return new ObjectInfo(
                types.ToArray(),
                references.ToArray(),
                behaviours.ToArray()
            );
        }
    }
}