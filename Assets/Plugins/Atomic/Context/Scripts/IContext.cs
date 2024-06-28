using System;
using System.Collections.Generic;

namespace Atomic.Contexts
{
    public interface IContext
    {
        event Action<ContextState> OnStateChanged; 
        
        event Action<int, object> OnDataAdded;
        event Action<int, object> OnDataDeleted;
        event Action<int, object> OnDataChanged;

        event Action<ISystem> OnSystemAdded;
        event Action<ISystem> OnSystemRemoved; 

        string Name { get; set; }
        ContextState State { get; }
        
        IReadOnlyDictionary<int, object> AllData { get; }
        IReadOnlyList<ISystem> AllSystems { get; }
        
        IContext Parent { get; set; }
        IReadOnlyList<IContext> Children { get; }

        bool AddData(int key, object value);
        void SetData(int key, object value);
        bool DelData(int key);
        bool DelData(int key, out object removed);
        bool HasData(int key);
        
        T GetData<T>(int key) where T : class;
        object GetData(int key);
        bool TryGetData<T>(int id, out T value) where T : class;
        bool TryGetData(int id, out object value);
        
        T GetSystem<T>() where T : ISystem;
        bool TryGetLogic<T>(out T logic) where T : ISystem;
        
        bool AddSystem(ISystem system);
        bool AddSystem<T>() where T : ISystem;
        bool DelSystem(ISystem system);
        bool DelSystem<T>() where T : ISystem;
        bool HasSystem(ISystem system);
        bool HasSystem<T>() where T : ISystem;
    }
}