using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Objects/Scene Object Proxy")]
    [DisallowMultipleComponent]
    public sealed class SceneObjectProxy : MonoBehaviour, IObject
    {
        [SerializeField]
        private SceneObject source;
        
        public event Action<IObject, string> OnNameChanged
        {
            add => source.OnNameChanged += value;
            remove => source.OnNameChanged -= value;
        }

        public event Action<IObject> OnEnabled
        {
            add => source.OnEnabled += value;
            remove => source.OnEnabled -= value;
        }

        public event Action<IObject> OnDisabled
        {
            add => source.OnDisabled += value;
            remove => source.OnDisabled -= value;
        }

        public event Action<IObject, int> OnTagAdded
        {
            add => source.OnTagAdded += value;
            remove => source.OnTagAdded -= value;
        }

        public event Action<IObject, int> OnTagDeleted
        {
            add => source.OnTagDeleted += value;
            remove => source.OnTagDeleted -= value;
        }

        public event Action<IObject, int, object> OnValueAdded
        {
            add => source.OnValueAdded += value;
            remove => source.OnValueAdded -= value;
        }

        public event Action<IObject, int, object> OnValueDeleted
        {
            add => source.OnValueDeleted += value;
            remove => source.OnValueDeleted -= value;
        }

        public event Action<IObject, int, object> OnValueChanged
        {
            add => source.OnValueChanged += value;
            remove => source.OnValueChanged -= value;
        }

        public event Action<IObject, ILogic> OnLogicAdded
        {
            add => source.OnLogicAdded += value;
            remove => source.OnLogicAdded -= value;
        }

        public event Action<IObject, ILogic> OnLogicDeleted
        {
            add => source.OnLogicDeleted += value;
            remove => source.OnLogicDeleted -= value;
        }
        public int Id => source.Id;

        public string Name
        {
            get => source.Name;
            set => source.Name = value;
        }

        public bool Enabled
        {
            get => source.Enabled;
            set => source.Enabled = value;
        }

        public bool Alive => source.Alive;

        bool IObject.Constructed
        {
            get => ((IObject) source).Constructed;
            set => ((IObject) source).Constructed = value;
        }

        void IObject.Dispose()
        {
            ((IObject) source).Dispose();
        }

        public IReadOnlyCollection<int> Tags => source.Tags;

        public bool HasTag(int tag)
        {
            return source.HasTag(tag);
        }

        public bool AddTag(int tag)
        {
            return source.AddTag(tag);
        }

        public bool DelTag(int tag)
        {
            return source.DelTag(tag);
        }

        public IReadOnlyDictionary<int, object> Values => source.Values;

        public T GetValue<T>(int id) where T : class
        {
            return source.GetValue<T>(id);
        }

        public object GetValue(int id)
        {
            return source.GetValue(id);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return source.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return source.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return source.AddValue(id, value);
        }

        public void SetValue(int id, object value)
        {
            source.SetValue(id, value);
        }

        public bool DelValue(int id)
        {
            return source.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return source.DelValue(id, out removed);
        }

        public bool HasValue(int id)
        {
            return source.HasValue(id);
        }

        public IReadOnlyList<ILogic> Logics => source.Logics;

        public T GetLogic<T>() where T : ILogic
        {
            return source.GetLogic<T>();
        }

        public bool TryGetLogic<T>(out T logic) where T : ILogic
        {
            return source.TryGetLogic(out logic);
        }

        public bool AddLogic(ILogic logic)
        {
            return source.AddLogic(logic);
        }

        public bool AddLogic<T>() where T : ILogic, new()
        {
            return source.AddLogic<T>();
        }

        public bool DelLogic(ILogic logic)
        {
            return source.DelLogic(logic);
        }

        public bool DelLogic<T>() where T : ILogic
        {
            return source.DelLogic<T>();
        }

        public bool HasLogic(ILogic logic)
        {
            return source.HasLogic(logic);
        }

        public bool HasLogic<T>() where T : ILogic
        {
            return source.HasLogic<T>();
        }
    }
}