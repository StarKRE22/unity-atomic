using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    ///Represents a product of float members 
    
    [Serializable]
    public sealed class AtomicFloatProduct : AtomicExpression<float>
    {
        public AtomicFloatProduct()
        {
        }

        public AtomicFloatProduct(IEnumerable<IAtomicValue<float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<IAtomicValue<float>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            float result = 1;
            
            for (int i = 0; i < count; i++)
            {
                IAtomicValue<float> member = members[i];
                result *= member.Value;
            }

            return result;
        }
    }
}
