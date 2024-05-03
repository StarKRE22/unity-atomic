// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Atomic
// {
//     [AddComponentMenu("Atomic/Mutable Atomic Entity Proxy")]
//     [DisallowMultipleComponent]
//     public class MutableAtomicEntityProxy : MonoBehaviour, IMutableAtomicEntity
//     {
//         public MutableAtomicEntity Source
//         {
//             get => this.source;
//             set => this.source = value;
//         }
//
//         [SerializeField]
//         private MutableAtomicEntity source;
//
//         public T Get<T>(string key) where T : class
//         {
//             return source.Get<T>(key);
//         }
//
//         public T Get<T>(int index) where T : class
//         {
//             return source.Get<T>(index);
//         }
//
//         public object Get(string key)
//         {
//             return source.Get(key);
//         }
//
//         public object Get(int index)
//         {
//             return source.Get(index);
//         }
//
//         public bool TryGet<T>(string key, out T result) where T : class
//         {
//             return source.TryGet(key, out result);
//         }
//
//         public bool TryGet<T>(int index, out T result) where T : class
//         {
//             return source.TryGet(index, out result);
//         }
//
//         public bool TryGet(string key, out object result)
//         {
//             return source.TryGet(key, out result);
//         }
//
//         public bool TryGet(int index, out object result)
//         {
//             return source.TryGet(index, out result);
//         }
//
//         public bool Is(string type)
//         {
//             return source.Is(type);
//         }
//
//         public bool Is(int index)
//         {
//             return source.Is(index);
//         }
//
//         public void GetValues(out List<KeyValuePair<string, object>> results)
//         {
//             source.GetValues(out results);
//         }
//
//         public int GetValuesNonAlloc(KeyValuePair<string, object>[] results)
//         {
//             return source.GetValuesNonAlloc(results);
//         }
//
//         public void GetValues(out List<KeyValuePair<int, object>> results)
//         {
//             source.GetValues(out results);
//         }
//
//         public int GetValuesNonAlloc(KeyValuePair<int, object>[] results)
//         {
//             return source.GetValuesNonAlloc(results);
//         }
//
//         public void GetTypes(out List<string> results)
//         {
//             source.GetTypes(out results);
//         }
//
//         public int GetTypesNonAlloc(string[] results)
//         {
//             return source.GetTypesNonAlloc(results);
//         }
//
//         public void GetTypes(out List<int> results)
//         {
//             source.GetTypes(out results);
//         }
//
//         public int GetTypesNonAlloc(int[] results)
//         {
//             return source.GetTypesNonAlloc(results);
//         }
//
//         public bool Add(string key, object value)
//         {
//             return source.Add(key, value);
//         }
//
//         public bool Add(int index, object value)
//         {
//             return source.Add(index, value);
//         }
//
//         public void Set(string key, object value)
//         {
//             source.Set(key, value);
//         }
//
//         public void Set(int index, object value)
//         {
//             source.Set(index, value);
//         }
//
//         public bool Remove(string key)
//         {
//             return source.Remove(key);
//         }
//
//         public bool Remove(int index)
//         {
//             return source.Remove(index);
//         }
//
//         public bool Remove(string key, out object value)
//         {
//             return source.Remove(key, out value);
//         }
//
//         public bool Remove(int index, out object value)
//         {
//             return source.Remove(index, out value);
//         }
//
//         public void Override(string key, object value, out object prevValue)
//         {
//             source.Override(key, value, out prevValue);
//         }
//
//         public void Override(int index, object value, out object prevValue)
//         {
//             source.Override(index, value, out prevValue);
//         }
//
//         public bool AddType(string type)
//         {
//             return source.AddType(type);
//         }
//
//         public bool AddType(int index)
//         {
//             return source.AddType(index);
//         }
//
//         public void AddTypes(IEnumerable<string> types)
//         {
//             source.AddTypes(types);
//         }
//
//         public void AddTypes(IEnumerable<int> indexes)
//         {
//             source.AddTypes(indexes);
//         }
//
//         public bool RemoveType(string type)
//         {
//             return source.RemoveType(type);
//         }
//
//         public bool RemoveType(int index)
//         {
//             return source.RemoveType(index);
//         }
//
//         public void RemoveTypes(IEnumerable<string> types)
//         {
//             source.RemoveTypes(types);
//         }
//
//         public void RemoveTypes(IEnumerable<int> indexes)
//         {
//             source.RemoveTypes(indexes);
//         }
//     }
// }