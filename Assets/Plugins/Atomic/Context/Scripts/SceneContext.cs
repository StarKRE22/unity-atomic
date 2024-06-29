using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Contexts
{
    public sealed class SceneContext : MonoBehaviour, IContext
    {
        private Context context;

        public event Action<ContextState> OnStateChanged
        {
            add => context.OnStateChanged += value;
            remove => context.OnStateChanged -= value;
        }

        public event Action<int, object> OnValueAdded
        {
            add => context.OnValueAdded += value;
            remove => context.OnValueAdded -= value;
        }

        public event Action<int, object> OnValueDeleted
        {
            add => context.OnValueDeleted += value;
            remove => context.OnValueDeleted -= value;
        }

        public event Action<int, object> OnValueChanged
        {
            add => context.OnValueChanged += value;
            remove => context.OnValueChanged -= value;
        }

        public event Action<ISystem> OnSystemAdded
        {
            add => context.OnSystemAdded += value;
            remove => context.OnSystemAdded -= value;
        }

        public event Action<ISystem> OnSystemRemoved
        {
            add => context.OnSystemRemoved += value;
            remove => context.OnSystemRemoved -= value;
        }

        public string Name
        {
            get => context.Name;
            set => context.Name = value;
        }

        public ContextState State => context.State;

        public IReadOnlyDictionary<int, object> Values => context.Values;

        public IReadOnlyCollection<ISystem> Systems => context.Systems;

        public IContext Parent
        {
            get => context.Parent;
            set => context.Parent = value;
        }


        public IReadOnlyCollection<IContext> Children => context.Children;
        public ICollection<IContext> ChildrenUnsafe => context.ChildrenUnsafe;

        public bool AddValue(int key, object value)
        {
            return context.AddValue(key, value);
        }

        public void SetValue(int key, object value)
        {
            context.SetValue(key, value);
        }

        public bool DelValue(int key)
        {
            return context.DelValue(key);
        }

        public bool DelValue(int key, out object removed)
        {
            return context.DelValue(key, out removed);
        }

        public bool HasValue(int key)
        {
            return context.HasValue(key);
        }

        public T GetValue<T>(int key) where T : class
        {
            return context.GetValue<T>(key);
        }

        public object GetValue(int key)
        {
            return context.GetValue(key);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return context.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return context.TryGetValue(id, out value);
        }

        public T GetSystem<T>() where T : ISystem
        {
            return context.GetSystem<T>();
        }

        public bool TryGetSystem<T>(out T result) where T : ISystem
        {
            return context.TryGetSystem(out result);
        }

        public bool AddSystem(ISystem system)
        {
            return context.AddSystem(system);
        }

        public bool AddSystem<T>() where T : ISystem, new()
        {
            return context.AddSystem<T>();
        }

        public bool DelSystem(ISystem system)
        {
            return context.DelSystem(system);
        }

        public bool DelSystem<T>() where T : ISystem
        {
            return context.DelSystem<T>();
        }

        public bool HasSystem(ISystem system)
        {
            return context.HasSystem(system);
        }

        public bool HasSystem<T>() where T : ISystem
        {
            return context.HasSystem<T>();
        }

        public bool IsChild(Context context)
        {
            return this.context.IsChild(context);
        }

        public bool IsParent(Context context)
        {
            return this.context.IsParent(context);
        }
    }
}