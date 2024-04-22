using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    ///Represents a product of float members 

    [Serializable]
    public sealed class AtomicIntProduct : AtomicExpression<int>
    {
        public AtomicIntProduct()
        {
        }

        public AtomicIntProduct(IEnumerable<IAtomicValue<int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<IAtomicValue<int>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 1;
            
            for (int i = 0; i < count; i++)
            {
                IAtomicValue<int> member = members[i];
                result *= member.Value;
            }

            return result;
        }
    }
}
