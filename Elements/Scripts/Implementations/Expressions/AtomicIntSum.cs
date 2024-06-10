using System;
using System.Collections.Generic;
using Atomic.Elements;
using GameEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicIntSum : AtomicExpression<int>
    {
        public AtomicIntSum()
        {
        }

        public AtomicIntSum(IEnumerable<Func<int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 0;
            
            for (int i = 0; i < count; i++)
            {
                Func<int> member = members[i];
                result += member.Invoke();
            }

            return result;
        }
    }
}