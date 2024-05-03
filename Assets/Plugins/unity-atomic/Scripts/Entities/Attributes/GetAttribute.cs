using System;
using JetBrains.Annotations;

namespace Atomic
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GetAttribute : Attribute
    {
        internal readonly string key;
        internal readonly int index;

        public GetAttribute(string key, int index = -1)
        {
            this.key = key;
            this.index = index;
        }
        
        public GetAttribute(int index, string key = null)
        {
            this.key = key;
            this.index = index;
        }
    }
}