using System;
using JetBrains.Annotations;

namespace Atomic
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class IsAttribute : Attribute
    {
        internal readonly string[] Types;
        internal readonly int[] Indexes;

        public IsAttribute(params string[] types)
        {
            this.Types = types;
            this.Indexes = Array.Empty<int>();
        }
        
        public IsAttribute(params int[] indexes)
        {
            this.Indexes = indexes;
            this.Types = Array.Empty<string>();
        }
        
        public IsAttribute(string[] types, params int[] indexes)
        {
            this.Types = types;
            this.Indexes = indexes;
        }
        
        public IsAttribute(int[] indexes, params string[] types)
        {
            this.Types = types;
            this.Indexes = indexes;
        }
    }
}