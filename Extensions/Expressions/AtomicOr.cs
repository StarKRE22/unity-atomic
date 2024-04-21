using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicOr : AtomicExpression<bool>
    {
        public AtomicOr()
        {
        }

        public AtomicOr(IEnumerable<IAtomicValue<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Value)
                {
                    return true;
                }

            }

            return false;
        }
    }
}