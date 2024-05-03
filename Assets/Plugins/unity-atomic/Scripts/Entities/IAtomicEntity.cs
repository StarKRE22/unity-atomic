using System;
using System.Collections.Generic;

namespace Atomic
{
    public interface IAtomicEntity
    {
        [Obsolete] //УБРАТЬ
        T Get<T>(string key) where T : class;
        T Get<T>(int index) where T : class;
        object Get(string key);
        object Get(int index);

        [Obsolete] //УБРАТЬ
        bool TryGet<T>(string key, out T result) where T : class;
        bool TryGet<T>(int index, out T result) where T : class;

        bool TryGet(string key, out object result);
        bool TryGet(int index, out object result);
        
        bool Is(string type);
        bool Is(int index);
        
        void Fields(out List<KeyValuePair<string, object>> results);
        int FieldsNonAlloc(KeyValuePair<string, object>[] results);

        void Fields(out List<KeyValuePair<int, object>> results);
        int FieldsNonAlloc(KeyValuePair<int, object>[] results);

        void Types(out List<string> results);
        int TypesNonAlloc(string[] results);
        
        void Types(out List<int> results);
        int TypesNonAlloc(int[] results);

        bool AddField(string key, object value);
        bool AddField(int index, object value);

        void SetField(string key, object value);
        void SetField(int index, object value);

        bool RemoveField(string key);
        bool RemoveField(int index);
        bool RemoveField(string key, out object value);
        bool RemoveField(int index, out object value);

        void OverrideField(string key, object value, out object prevValue);
        void OverrideField(int index, object value, out object prevValue);

        bool AddType(string type);
        bool AddType(int index);

        void AddTypes(IEnumerable<string> types);
        void AddTypes(IEnumerable<int> indexes);

        bool RemoveType(string type);
        bool RemoveType(int index);
        void RemoveTypes(IEnumerable<string> types);
        void RemoveTypes(IEnumerable<int> indexes);
    }
}