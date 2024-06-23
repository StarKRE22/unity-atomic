using System;
using System.Collections.Generic;
using Atomic.Elements;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable PublicConstructorInAbstractClass

namespace Atomic.Elements
{
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AtomicExpression<T> : IAtomicExpression<T>
    {
        private readonly List<Func<T>> members;

        public AtomicExpression()
        {
            this.members = new List<Func<T>>();
        }

        public AtomicExpression(params Func<T>[] members)
        {
            this.members = new List<Func<T>>(members);
        }

        public AtomicExpression(IEnumerable<Func<T>> members)
        {
            this.members = new List<Func<T>>(members);
        }

        public void Append(Func<T> member)
        {
            if (member != null)
            {
                this.members.Add(member);
            }
        }

        public void Remove(Func<T> member)
        {
            if (member != null)
            {
                this.members.Remove(member);
            }
        }

        public bool Contains(Func<T> member)
        {
            return this.members.Contains(member);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public T Invoke()
        {
            return this.Invoke(this.members);
        }

        protected abstract T Invoke(IReadOnlyList<Func<T>> members);
    }

    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AtomicExpression<T, R> : IAtomicExpression<T, R>
    {
        private readonly List<Func<T, R>> members;

        public AtomicExpression()
        {
            this.members = new List<Func<T, R>>();
        }

        public AtomicExpression(params Func<T, R>[] members)
        {
            this.members = new List<Func<T, R>>(members);
        }

        public AtomicExpression(IEnumerable<Func<T, R>> members)
        {
            this.members = new List<Func<T, R>>(members);
        }

        public void Append(Func<T, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(Func<T, R> member)
        {
            this.members.Remove(member);
        }

        public bool Contains(Func<T, R> member)
        {
            return this.members.Contains(member);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args)
        {
            return this.Invoke(this.members, args);
        }

        protected abstract R Invoke(IReadOnlyList<Func<T, R>> members, T args);
    }


    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AtomicExpression<T1, T2, R> : IAtomicExpression<T1, T2, R>
    {
        private readonly List<Func<T1, T2, R>> members;

        public AtomicExpression()
        {
            this.members = new List<Func<T1, T2, R>>();
        }

        public AtomicExpression(params Func<T1, T2, R>[] members)
        {
            this.members = new List<Func<T1, T2, R>>(members);
        }

        public AtomicExpression(IEnumerable<Func<T1, T2, R>> members)
        {
            this.members = new List<Func<T1, T2, R>>(members);
        }

        public void Append(Func<T1, T2, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(Func<T1, T2, R> member)
        {
            this.members.Remove(member);
        }

        public bool Contains(Func<T1, T2, R> member)
        {
            return this.members.Contains(member);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2)
        {
            return this.Invoke(this.members, arg1, arg2);
        }

        protected abstract R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2);
    }
}