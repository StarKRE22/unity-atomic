using UnityEngine;

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Entity Proxy")]
    [DisallowMultipleComponent]
    public class AtomicEntityProxy : MonoBehaviour, IAtomicEntity
    {
        public AtomicEntity Source
        {
            get => this.source;
            set => this.source = value;
        }

        [SerializeField]
        private AtomicEntity source;

        public T Get<T>(int index)
        {
            return source.Get<T>(index);
        }

        public object Get(int index)
        {
            return source.Get(index);
        }

        public bool TryGet<T>(int index, out T result) 
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
    }
}