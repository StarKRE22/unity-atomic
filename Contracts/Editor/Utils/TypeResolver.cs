using System;
using System.Collections.Generic;
using System.Reflection;

namespace Atomic.Objects
{
    //NOT USED!
    internal static class TypeResolver
    {
        private static readonly HashSet<Assembly> AssemblyCache = new();

        public static Type GetType(string name)
        {
            var type = Type.GetType(name);

            if (type is not null)
            {
                return type;
            }

            foreach (var assembly in AssemblyCache)
            {
                type = assembly.GetType(name);

                if (type is not null)
                {
                    return type;
                }
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (AssemblyCache.Contains(assembly))
                {
                    continue;
                }

                type = assembly.GetType(name);

                if (type is not null)
                {
                    AssemblyCache.Add(assembly);
                    return type;
                }
            }

            return null;
        }
    }
}