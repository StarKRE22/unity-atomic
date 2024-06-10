using System;
using System.Collections.Generic;
using GameEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicBoolAnd : AtomicExpression<bool>
    {
        public AtomicBoolAnd()
        {
        }

        public AtomicBoolAnd(params Func<bool>[] members) : base(members)
        {
        }

        public AtomicBoolAnd(IEnumerable<Func<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    
    [Serializable]
    public class AtomicBoolAnd<T> : AtomicExpression<T, bool>
    {
        public AtomicBoolAnd()
        {
        }

        public AtomicBoolAnd(params Func<T, bool>[] members) : base(members)
        {
        }

        public AtomicBoolAnd(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Invoke(args))
                {
                    return false;
                }
            }

            return true;
        }
    }
}