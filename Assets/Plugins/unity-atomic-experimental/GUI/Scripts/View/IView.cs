using System;
using System.Collections.Generic;

namespace Atomic.UI
{
    public interface IView
    {
        event Action<IView, string> OnNameChanged;
        event Action<IView> OnShown;
        event Action<IView> OnHidden;

        event Action<IView, int, object> OnValueAdded;
        event Action<IView, int, object> OnValueDeleted;
        event Action<IView, int, object> OnValueChanged;

        string Name { get; set; }
        bool IsShown { get; set; }

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