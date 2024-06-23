using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    ///Represents a product of float members 

    [Serializable]
    public class AtomicIntMul : AtomicExpression<int>
    {
        public AtomicIntMul()
        {
        }

        public AtomicIntMul(IEnumerable<Func<int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 1;
            
            for (int i = 0; i < count; i++)
            {
                Func<int> member = members[i];
                result *= member.Invoke();
            }

            return result;
        }
    }
}
