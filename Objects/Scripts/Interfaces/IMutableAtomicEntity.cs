using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IMutableAtomicEntity : IAtomicEntity
    {
        bool AddData(string key, object value);

        void SetData(string key, object value);

        bool RemoveData(string key);

        bool RemoveData(string key, out object value);

        void OverrideData(string key, object value, out object prevValue);

        bool AddType(string type);

        void AddTypes(IEnumerable<string> types);

        bool RemoveType(string type);

        void RemoveTypes(IEnumerable<string> types);
    }
}