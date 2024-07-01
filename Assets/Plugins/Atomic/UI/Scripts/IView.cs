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
        
        event Action<IViewBehaviour> OnBehaviourAdded;
        event Action<IViewBehaviour> OnBehaviourRemoved;

        string Name { get; }
        bool Initialized { get; }
        bool IsVisible { get; }

        IReadOnlyDictionary<int, object> Values { get; }
        public IReadOnlyCollection<IViewBehaviour> Behaviours { get; }
        
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

        T GetBehaviour<T>() where T : IViewBehaviour;
        bool TryGetBehaviour<T>(out T result) where T : IViewBehaviour;
        
        bool AddBehaviour<T>() where T : IViewBehaviour, new();
        bool AddBehaviour(IViewBehaviour handler);
        bool DelBehaviour<T>() where T : IViewBehaviour;
        bool DelBehaviour(IViewBehaviour handler);
        
        bool HasBehaviour(IViewBehaviour system);
        bool HasBehaviour<T>() where T : IViewBehaviour;
    }
}