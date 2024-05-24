using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Atomic Object Proxy")]
    [DisallowMultipleComponent]
    public sealed class MonoObjectProxy : MonoBehaviour, IObject
    {
        [SerializeField]
        private MonoObject source;

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

        public T GetValue<T>(int id) where T : class
        {
            return source.GetValue<T>(id);
        }

        public object GetValue(int id)
        {
            return source.GetValue(id);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return source.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return source.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return source.AddValue(id, value);
        }

        public void SetValue(int id, object value)
        {
            source.SetValue(id, value);
        }

        public bool DelValue(int id)
        {
            return source.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return source.DelValue(id, out removed);
        }

        public bool AddLogic(ILogic logic)
        {
            return source.AddLogic(logic);
        }

        public bool AddLogic<T>() where T : ILogic, new()
        {
            return source.AddLogic<T>();
        }

        public bool DelLogic(ILogic logic)
        {
            return source.DelLogic(logic);
        }

        public bool DelLogic<T>() where T : ILogic
        {
            return source.DelLogic<T>();
        }
    }
}