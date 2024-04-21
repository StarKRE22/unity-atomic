using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicAnd : AtomicExpression<bool>
    {
        public AtomicAnd()
        {
        }

        public AtomicAnd(IEnumerable<IAtomicValue<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}