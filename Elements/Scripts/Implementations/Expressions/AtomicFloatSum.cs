using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicFloatSum : AtomicExpression<float>
    {
        public AtomicFloatSum()
        {
        }

        public AtomicFloatSum(params Func<float>[] members) : base(members)
        {
        }
        
        public AtomicFloatSum(IEnumerable<Func<float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            float result = 0;
            
            for (int i = 0; i < count; i++)
            {
                Func<float> member = members[i];
                result += member.Invoke();
            }

            return result;
        }
    }
}