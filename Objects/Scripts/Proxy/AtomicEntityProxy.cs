using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Objects
{
    public class AtomicEntityProxy : MonoBehaviour, IMutableAtomicEntity
    {
        public AtomicEntity Source
        {
            get => this.source;
            set => this.source = value;
        }

        [SerializeField]
        private AtomicEntity source;
        
        public T Get<T>(string key) where T : class
        {
            return this.source.Get<T>(key);
        }

        public object Get(string key)
        {
            return this.source.Get(key);
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            return this.source.TryGet(key, out result);
        }

        public bool TryGet(string key, out object result)
        {
            return this.source.TryGet(key, out result);
        }

        public bool Is(string type)
        {
            return this.source.Is(type);
        }

        public bool Is(params string[] types)
        {
            return this.source.Is(types);
        }

        public string[] AllTypes()
        {
            return this.source.AllTypes();
        }

        public KeyValuePair<string, object>[] AllData()
        {
            return this.source.AllData();
        }

        public ISet<string> AllTypesUnsafe()
        {
            return this.source.AllTypesUnsafe();
        }

        public IDictionary<string, object> AllDataUnsafe()
        {
            return this.source.AllDataUnsafe();
        }

        public int AllTypesNonAlloc(string[] results)
        {
            return this.source.AllTypesNonAlloc(results);
        }

        public int AllDataNonAlloc(KeyValuePair<string, object>[] results)
        {
            return this.source.AllDataNonAlloc(results);
        }

        public bool AddData(string key, object value)
        {
            return this.source.AddData(key, value);
        }

        public void SetData(string key, object value)
        {
            this.source.SetData(key, value);
        }

        public bool RemoveData(string key)
        {
            return this.source.RemoveData(key);
        }

        public bool RemoveData(string key, out object value)
        {
            return this.source.RemoveData(key, out value);
        }

        public void OverrideData(string key, object value, out object prevValue)
        {
            this.source.OverrideData(key, value, out prevValue);
        }

        public bool AddType(string type)
        {
            return this.source.AddType(type);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            this.source.AddTypes(types);
        }

        public bool RemoveType(string type)
        {
            return this.source.RemoveType(type);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            this.source.RemoveTypes(types);
        }
    }
}