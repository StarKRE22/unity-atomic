using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IObject
    {
        event Action<IObject, string> OnNameChanged;
        event Action<IObject> OnEnabled;
        event Action<IObject> OnDisabled;

        event Action<IObject, int> OnTagAdded;
        event Action<IObject, int> OnTagDeleted;

        event Action<IObject, int, object> OnValueAdded;
        event Action<IObject, int, object> OnValueDeleted;
        event Action<IObject, int, object> OnValueChanged;

        event Action<IObject, ILogic> OnLogicAdded;
        event Action<IObject, ILogic> OnLogicDeleted;

        int Id { get; }
        string Name { get; set; }
        bool Enabled { get; set; }
        bool Alive { get; }
        internal bool Constructed { get; set; }
        internal void Dispose();

        IReadOnlyCollection<int> Tags { get; }
        bool HasTag(int tag);
        bool AddTag(int tag);
        bool DelTag(int tag);

        IReadOnlyDictionary<int, object> Values { get; }
        T GetValue<T>(int id) where T : class;
        object GetValue(int id);
        bool TryGetValue<T>(int id, out T value) where T : class;
        bool TryGetValue(int id, out object value);

        bool AddValue(int id, object value);
        void SetValue(int id, object value);
        bool DelValue(int id);
        bool DelValue(int id, out object removed);
        bool HasValue(int id);

        IReadOnlyList<ILogic> Logics { get; }
        T GetLogic<T>() where T : ILogic;
        bool TryGetLogic<T>(out T logic) where T : ILogic;

        bool AddLogic(ILogic logic);
        bool AddLogic<T>() where T : ILogic, new();
        bool DelLogic(ILogic logic);
        bool DelLogic<T>() where T : ILogic;
        bool HasLogic(ILogic logic);
        bool HasLogic<T>() where T : ILogic;
    }
}

