using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IAtomicEntity
    {
        T Get<T>(string key) where T : class;

        object Get(string key);

        bool TryGet<T>(string key, out T result) where T : class;

        bool TryGet(string key, out object result);
        
        KeyValuePair<string, object>[] AllData();
        
        IDictionary<string, object> AllDataUnsafe();
        
        int AllDataNonAlloc(KeyValuePair<string, object>[] results);

        bool Is(string type);

        bool Is(params string[] types);

        string[] AllTypes();
        
        ISet<string> AllTypesUnsafe();
        
        int AllTypesNonAlloc(string[] results);
    }
}