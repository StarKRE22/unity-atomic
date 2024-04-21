using System;
using System.Collections.Generic;
using Atomic.Elements;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable PublicConstructorInAbstractClass

namespace GameEngine
{
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AtomicExpression<T> : IAtomicExpression<T>
    {
        private readonly List<IAtomicValue<T>> members;

        public AtomicExpression()
        {
            this.members = new List<IAtomicValue<T>>();
        }

        public AtomicExpression(params IAtomicValue<T>[] members)
        {
            this.members = new List<IAtomicValue<T>>(members);
        }

        public AtomicExpression(IEnumerable<IAtomicValue<T>> members)
        {
            this.members = new List<IAtomicValue<T>>(members);
        }

        public void Append(IAtomicValue<T> member)
        {
            if (member != null)
            {
                this.members.Add(member);
            }
        }

        public void Remove(IAtomicValue<T> member)
        {
            if (member != null)
            {
                this.members.Remove(member);
            }
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public T Invoke()
        {
            return this.Invoke(this.members);
        }

        protected abstract T Invoke(IReadOnlyList<IAtomicValue<T>> members);
    }

    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AtomicExpression<T, R> : IAtomicExpression<T, R>
    {
        private readonly List<IAtomicFunction<T, R>> members = new();

        public AtomicExpression()
        {
            this.members = new List<IAtomicFunction<T, R>>();
        }

        public AtomicExpression(params IAtomicFunction<T, R>[] members)
        {
            this.members = new List<IAtomicFunction<T, R>>(members);
        }

        public AtomicExpression(IEnumerable<IAtomicFunction<T, R>> members)
        {
            this.members = new List<IAtomicFunction<T, R>>(members);
        }
        
        public void Append(IAtomicFunction<T, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(IAtomicFunction<T, R> member)
        {
            this.members.Remove(member);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args)
        {
            return this.Invoke(this.members, args);
        }

        protected abstract R Invoke(IReadOnlyList<IAtomicFunction<T, R>> members, T args);
    }
}