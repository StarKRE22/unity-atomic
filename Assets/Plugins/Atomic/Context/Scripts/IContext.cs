using System;
using System.Collections.Generic;

namespace Atomic.Contexts
{
    public interface IContext
    {
        event Action OnInitiazized;
        event Action OnEnabled;
        event Action OnDisabled;
        event Action OnDisposed;
        
        event Action<int, object> OnValueAdded;
        event Action<int, object> OnValueDeleted;
        event Action<int, object> OnValueChanged;

        event Action<ISystem> OnSystemAdded;
        event Action<ISystem> OnSystemRemoved;

        event Action<float> OnUpdate;
        event Action<float> OnFixedUpdate;
        event Action<float> OnLateUpdate;

        string Name { get; set; }
        bool Initialized { get; }
        bool Enabled { get; }

        IReadOnlyDictionary<int, object> Values { get; }
        IReadOnlyCollection<ISystem> Systems { get; }

        IContext Parent { get; set; }

        bool AddValue(int key, object value);
        void SetValue(int key, object value);
        bool DelValue(int key);
        bool DelValue(int key, out object removed);
        bool HasValue(int key);

        T GetValue<T>(int key) where T : class;
        object GetValue(int key);
        bool TryGetValue<T>(int id, out T value) where T : class;
        bool TryGetValue(int id, out object value);

        T GetSystem<T>() where T : ISystem;
        bool TryGetSystem<T>(out T result) where T : ISystem;

        bool AddSystem(ISystem system);
        bool AddSystem<T>() where T : ISystem, new();
        bool DelSystem(ISystem system);
        bool DelSystem<T>() where T : ISystem;
        bool HasSystem(ISystem system);
        bool HasSystem<T>() where T : ISystem;

        bool IsParent(IContext context);
    }
}