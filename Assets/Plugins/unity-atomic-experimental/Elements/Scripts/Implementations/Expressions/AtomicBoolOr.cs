using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicBoolOr : AtomicExpression<bool>
    {
        public AtomicBoolOr()
        {
        }

        public AtomicBoolOr(IEnumerable<Func<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Invoke())
                {
                    return true;
                }
            }

            return false;
        }
    }

    [Serializable]
    public class AtomicBoolOr<T> : AtomicExpression<T, bool>
    {
        public AtomicBoolOr()
        {
        }

        public AtomicBoolOr(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Invoke(args))
                {
                    return true;
                }
            }

            return false;
        }
    }
}