using System.Collections.Generic;
using UnityEngine;
// ReSharper disable UnusedMember.Global

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Object Proxy")]
    [DisallowMultipleComponent]
    public class AtomicObjectProxy : MonoBehaviour, IAtomicObject
    {
        public AtomicObject Source
        {
            get => this.source;
            set => this.source = value;
        }

        [SerializeField]
        private AtomicObject source;

        public int Id => source.Id;

        public T Get<T>(int index) where T : class
        {
            return source.Get<T>(index);
        }

        public object Get(int index)
        {
            return source.Get(index);
        }

        public bool TryGet<T>(int index, out T result) where T : class
        {
            return source.TryGet(index, out result);
        }

        public bool TryGet(int index, out object result)
        {
            return source.TryGet(index, out result);
        }

        public bool Put(int index, object value)
        {
            return source.Put(index, value);
        }

        public void Set(int index, object value)
        {
            source.Set(index, value);
        }

        public bool Del(int index)
        {
            return source.Del(index);
        }

        public bool Is(int index)
        {
            return source.Is(index);
        }

        public bool Mark(int index)
        {
            return source.Mark(index);
        }

        public bool Unmark(int index)
        {
            return source.Unmark(index);
        }

        public void AddLogic(IAtomicLogic target)
        {
            source.AddLogic(target);
        }

        public void RemoveLogic(IAtomicLogic target)
        {
            source.RemoveLogic(target);
        }

        public IAtomicLogic[] AllLogic()
        {
            return source.AllLogic();
        }

        public IReadOnlyList<IAtomicLogic> AllLogicReadOnly()
        {
            return source.AllLogicReadOnly();
        }

        public int AllLogicNonAlloc(IAtomicLogic[] results)
        {
            return source.AllLogicNonAlloc(results);
        }
    }
}