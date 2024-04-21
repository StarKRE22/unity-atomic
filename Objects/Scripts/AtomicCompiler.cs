using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    public static class AtomicCompiler
    {
        private static readonly Dictionary<Type, AtomicEntityInfo> compiledEntities = new();

        //Call it in background thread!
        public static void PrecompileEntity(Type objectType)
        {
            CompileEntity(objectType);
        }

        internal static AtomicEntityInfo CompileEntity(Type objectType)
        {
            if (compiledEntities.TryGetValue(objectType, out AtomicEntityInfo objectInfo))
            {
                return objectInfo;
            }

            objectInfo = CompileEntityInternal(objectType);
            compiledEntities.Add(objectType, objectInfo);
            return objectInfo;
        }

        private static AtomicEntityInfo CompileEntityInternal(Type objectType)
        {
            var types = new HashSet<string>();
            var references = new List<ValueInfo>();

            foreach (Type @interface in objectType.GetInterfaces())
            {
                types.UnionWith(AtomicScanner.ScanTypes(@interface));
                references.AddRange(AtomicScanner.ScanValues(@interface));
            }

            while (objectType != typeof(AtomicObject) && objectType != typeof(AtomicEntity) && objectType != null)
            {
                types.UnionWith(AtomicScanner.ScanTypes(objectType));
                references.AddRange(AtomicScanner.ScanValues(objectType));
                objectType = objectType!.BaseType;
            }

            return new AtomicEntityInfo(types, references);
        }
    }
}
