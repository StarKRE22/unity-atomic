using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI
{
    public interface IView
    {
        event Action<IView> OnInitialized; 
        event Action<IView> OnDisposed; 
        event Action<IView> OnShown;
        event Action<IView> OnHidden;
        
        event Action<int, object> OnValueAdded;
        event Action<int, object> OnValueDeleted;
        event Action<int, object> OnValueChanged;
        
        event Action<IBehaviour> OnBehaviourAdded;
        event Action<IBehaviour> OnBehaviourRemoved;

        string Name { get; }
        bool Initialized { get; }
        bool IsVisible { get; }

        IReadOnlyDictionary<int, object> Values { get; }
        public IReadOnlyCollection<IBehaviour> Behaviours { get; }
        
        void SetVisible(bool visible);
        void Show();
        void Hide();

        bool AddValue(int key, object value);
        void SetValue(int key, object value);
        bool DelValue(int key);
        bool DelValue(int key, out object removed);
        bool HasValue(int key);

        T GetValue<T>(int key) where T : class;
        object GetValue(int key);
        bool TryGetValue<T>(int id, out T value) where T : class;
        bool TryGetValue(int id, out object value);

        T GetBehaviour<T>() where T : IBehaviour;
        bool TryGetBehaviour<T>(out T result) where T : IBehaviour;
        
        bool AddBehaviour<T>() where T : IBehaviour, new();
        bool AddBehaviour(IBehaviour handler);
        bool DelBehaviour<T>() where T : IBehaviour;
        bool DelBehaviour(IBehaviour handler);
        
        bool HasBehaviour(IBehaviour system);
        bool HasBehaviour<T>() where T : IBehaviour;
    }
}