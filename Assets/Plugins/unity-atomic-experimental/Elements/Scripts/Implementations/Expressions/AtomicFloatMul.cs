using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    ///Represents a product of float members 
    
    [Serializable]
    public class AtomicFloatMul : AtomicExpression<float>
    {
        public AtomicFloatMul()
        {
        }

        public AtomicFloatMul(params Func<float>[] members) : base(members)
        {
        }

        public AtomicFloatMul(IEnumerable<Func<float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            float result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<float> member = members[i];
                result *= member.Invoke();
            }

            return result;
        }
    }
    
    
    [Serializable]
    public class AtomicFloatMul<T> : AtomicExpression<T, float>
    {
        public AtomicFloatMul()
        {
        }

        public AtomicFloatMul(params Func<T, float>[] members) : base(members)
        {
        }

        public AtomicFloatMul(IEnumerable<Func<T, float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<T, float>> members, T arg)
        {
            float result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T, float> member = members[i];
                result *= member.Invoke(arg);
            }

            return result;
        }
    }
}