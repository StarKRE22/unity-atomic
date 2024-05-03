using System.Collections.Generic;
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
        
        public T Get<T>(string key) where T : class
        {
            return this.source.Get<T>(key);
        }

        public T Get<T>(int index) where T : class
        {
            return source.Get<T>(index);
        }

        public object Get(string key)
        {
            return this.source.Get(key);
        }

        public object Get(int index)
        {
            return source.Get(index);
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            return this.source.TryGet(key, out result);
        }

        public bool TryGet<T>(int index, out T result) where T : class
        {
            return source.TryGet(index, out result);
        }

        public bool TryGet(string key, out object result)
        {
            return this.source.TryGet(key, out result);
        }

        public bool TryGet(int index, out object result)
        {
            return source.TryGet(index, out result);
        }

        public bool Is(string type)
        {
            return this.source.Is(type);
        }

        public bool Is(int index)
        {
            return source.Is(index);
        }

        public void Fields(out List<KeyValuePair<string, object>> results)
        {
            source.Fields(out results);
        }

        public void Types(out List<string> results)
        {
            source.Types(out results);
        }

        public int TypesNonAlloc(string[] results)
        {
            return this.source.TypesNonAlloc(results);
        }

        public void Types(out List<int> results)
        {
            source.Types(out results);
        }

        public int TypesNonAlloc(int[] results)
        {
            return source.TypesNonAlloc(results);
        }

        public int FieldsNonAlloc(KeyValuePair<string, object>[] results)
        {
            return this.source.FieldsNonAlloc(results);
        }

        public void Fields(out List<KeyValuePair<int, object>> results)
        {
            source.Fields(out results);
        }

        public int FieldsNonAlloc(KeyValuePair<int, object>[] results)
        {
            return source.FieldsNonAlloc(results);
        }

        public bool AddField(string key, object value)
        {
            return source.AddField(key, value);
        }

        public bool AddField(int index, object value)
        {
            return source.AddField(index, value);
        }

        public void SetField(string key, object value)
        {
            source.SetField(key, value);
        }

        public void SetField(int index, object value)
        {
            source.SetField(index, value);
        }

        public bool RemoveField(string key)
        {
            return source.RemoveField(key);
        }

        public bool RemoveField(int index)
        {
            return source.RemoveField(index);
        }

        public bool RemoveField(string key, out object value)
        {
            return source.RemoveField(key, out value);
        }

        public bool RemoveField(int index, out object value)
        {
            return source.RemoveField(index, out value);
        }

        public void OverrideField(string key, object value, out object prevValue)
        {
            source.OverrideField(key, value, out prevValue);
        }

        public void OverrideField(int index, object value, out object prevValue)
        {
            source.OverrideField(index, value, out prevValue);
        }

        public bool AddType(string type)
        {
            return source.AddType(type);
        }

        public bool AddType(int index)
        {
            return source.AddType(index);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            source.AddTypes(types);
        }

        public void AddTypes(IEnumerable<int> indexes)
        {
            source.AddTypes(indexes);
        }

        public bool RemoveType(string type)
        {
            return source.RemoveType(type);
        }

        public bool RemoveType(int index)
        {
            return source.RemoveType(index);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            source.RemoveTypes(types);
        }

        public void RemoveTypes(IEnumerable<int> indexes)
        {
            source.RemoveTypes(indexes);
        }
    }
}