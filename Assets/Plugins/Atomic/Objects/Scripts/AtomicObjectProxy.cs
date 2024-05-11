using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Atomic Object Proxy")]
    [DisallowMultipleComponent]
    public sealed class AtomicObjectProxy : MonoBehaviour, IAtomicObject
    {
        [SerializeField]
        private AtomicObject source;

        private void Reset()
        {
            this.source = this.GetComponentInParent<AtomicObject>();
        }

        public T GetReference<T>(int id) where T : class
        {
            return source.GetReference<T>(id);
        }

        public object GetReference(int id)
        {
            return source.GetReference(id);
        }

        public bool TryGetReference<T>(int id, out T value) where T : class
        {
            return source.TryGetReference(id, out value);
        }

        public bool TryGetReference(int id, out object value)
        {
            return source.TryGetReference(id, out value);
        }

        public bool AddReference(int id, object value)
        {
            return source.AddReference(id, value);
        }

        public void SetReference(int id, object value)
        {
            source.SetReference(id, value);
        }

        public bool DelReference(int id)
        {
            return source.DelReference(id);
        }

        public bool DelReference(int id, out object removed)
        {
            return source.DelReference(id, out removed);
        }

        public bool AddBehaviour(IAtomicObject.IBehaviour behaviour)
        {
            return source.AddBehaviour(behaviour);
        }

        public bool AddBehaviour<T>() where T : IAtomicObject.IBehaviour, new()
        {
            return source.AddBehaviour<T>();
        }

        public bool DelBehaviour(IAtomicObject.IBehaviour behaviour)
        {
            return source.DelBehaviour(behaviour);
        }

        public bool DelBehaviour<T>() where T : IAtomicObject.IBehaviour
        {
            return source.DelBehaviour<T>();
        }

        public bool HasTag(int tag)
        {
            return source.HasTag(tag);
        }

        public bool AddTag(int tag)
        {
            return source.AddTag(tag);
        }

        public bool DelTag(int tag)
        {
            return source.DelTag(tag);
        }
    }
}