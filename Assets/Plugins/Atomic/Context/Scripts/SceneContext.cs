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

        public event Action<int, object> OnDataAdded
        {
            add => context.OnDataAdded += value;
            remove => context.OnDataAdded -= value;
        }

        public event Action<int, object> OnDataDeleted
        {
            add => context.OnDataDeleted += value;
            remove => context.OnDataDeleted -= value;
        }

        public event Action<int, object> OnDataChanged
        {
            add => context.OnDataChanged += value;
            remove => context.OnDataChanged -= value;
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

        public IReadOnlyDictionary<int, object> AllData => context.AllData;

        public IReadOnlyList<ISystem> AllSystems => context.AllSystems;

        public IContext Parent
        {
            get => context.Parent;
            set => context.Parent = value;
        }

        public IReadOnlyList<IContext> Children => context.Children;

        public bool AddData(int key, object value)
        {
            return context.AddData(key, value);
        }

        public void SetData(int key, object value)
        {
            context.SetData(key, value);
        }

        public bool DelData(int key)
        {
            return context.DelData(key);
        }

        public bool DelData(int key, out object removed)
        {
            return context.DelData(key, out removed);
        }

        public bool HasData(int key)
        {
            return context.HasData(key);
        }

        public T GetData<T>(int key) where T : class
        {
            return context.GetData<T>(key);
        }

        public object GetData(int key)
        {
            return context.GetData(key);
        }

        public bool TryGetData<T>(int id, out T value) where T : class
        {
            return context.TryGetData(id, out value);
        }

        public bool TryGetData(int id, out object value)
        {
            return context.TryGetData(id, out value);
        }

        public T GetSystem<T>() where T : ISystem
        {
            return context.GetSystem<T>();
        }

        public bool TryGetLogic<T>(out T logic) where T : ISystem
        {
            return context.TryGetLogic(out logic);
        }

        public bool AddSystem(ISystem system)
        {
            return context.AddSystem(system);
        }

        public bool AddSystem<T>() where T : ISystem
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

        public bool AddSystem(int key, ISystem logic)
        {
            return context.AddSystem(key, logic);
        }

        public void AddComponent(int key, ISystem component)
        {
            context.AddComponent(key, component);
        }
    }
}