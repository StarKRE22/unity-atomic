using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Objects
{
    //TODO: RENAME GAME OBJECT
    internal sealed class Object : IObject
    {
        public static IObject Dummy => new Object(-1, "", false);

        #region Main

        public event Action<IObject, string> OnNameChanged;
        public event Action<IObject> OnEnabled;
        public event Action<IObject> OnDisabled;

        private int id;
        private string name;
        private bool composed;
        private bool enabled;

        [ShowInInspector, ReadOnly] //TODO: REMOVE
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnNameChanged?.Invoke(this, value);
                }
            }
        }

        [ShowInInspector, ReadOnly]
        public int Id
        {
            get { return this.id; }
        }

        [ShowInInspector, ReadOnly]
        public bool Alive
        {
            get { return this.id != -1; }
        }
        
        bool IObject.Constructed
        {
            get => composed;
            set => composed = value;
        }

        [ShowInInspector, ReadOnly]
        public bool Enabled
        {
            get { return this.enabled; }
            set
            {
                if (this.enabled == value)
                {
                    return;
                }

                this.enabled = value;

                if (value)
                {
                    this.OnEnabled?.Invoke(this);
                }
                else
                {
                    this.OnDisabled?.Invoke(this);
                }
            }
        }

        public Object(int id, string name, bool enabled)
        {
            this.id = id;
            this.name = name;
            this.enabled = enabled;
        }

        void IObject.Dispose()
        {
            this.tags.Clear();
            this.values.Clear();
            this.logics.Clear();

            this.composed = false;
            this.enabled = false;
            this.id = -1;
        }

        #endregion

        #region Tags

        public event Action<IObject, int> OnTagAdded;
        public event Action<IObject, int> OnTagDeleted;

        public IReadOnlyCollection<int> Tags => this.tags;

        private readonly HashSet<int> tags = new();

        public bool DelTag(int tag)
        {
            if (this.tags.Remove(tag))
            {
                this.OnTagDeleted?.Invoke(this, tag);
                return true;
            }

            return false;
        }

        public bool HasTag(int tag)
        {
            return this.tags.Contains(tag);
        }

        public bool AddTag(int tag)
        {
            if (this.tags.Add(tag))
            {
                this.OnTagAdded?.Invoke(this, tag);
                return true;
            }

            return false;
        }

        #endregion

        #region Values

        public event Action<IObject, int, object> OnValueAdded;
        public event Action<IObject, int, object> OnValueDeleted;
        public event Action<IObject, int, object> OnValueChanged;

        public IReadOnlyDictionary<int, object> Values => this.values;

        private readonly Dictionary<int, object> values = new();

        public T GetValue<T>(int id) where T : class
        {
            if (this.values.TryGetValue(id, out object value))
            {
                return value as T;
            }

            return null;
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            if (this.values.TryGetValue(id, out object field))
            {
                value = field as T;
                return true;
            }

            value = default;
            return false;
        }

        public object GetValue(int id)
        {
            return this.values[id];
        }

        public void SetValue(int id, object value)
        {
            this.values[id] = value;
            this.OnValueChanged?.Invoke(this, id, value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            if (this.values.TryAdd(id, value))
            {
                this.OnValueAdded?.Invoke(this, id, value);
                return true;
            }

            return false;
        }

        public bool DelValue(int id)
        {
            if (this.values.Remove(id, out object removed))
            {
                this.OnValueDeleted?.Invoke(this, id, removed);
                return true;
            }

            return false;
        }

        public bool DelValue(int id, out object removed)
        {
            if (this.values.Remove(id, out removed))
            {
                this.OnValueDeleted?.Invoke(this, id, removed);
                return true;
            }

            return false;
        }

        public bool HasValue(int id)
        {
            return this.values.ContainsKey(id);
        }

        #endregion

        #region Logic

        public event Action<IObject, ILogic> OnLogicAdded;
        public event Action<IObject, ILogic> OnLogicDeleted;

        public IReadOnlyList<ILogic> Logics => this.logics;
        
        private readonly List<ILogic> logics = new();

        public T GetLogic<T>() where T : ILogic
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is T logic)
                {
                    return logic;
                }
            }

            return default;
        }

        public bool TryGetLogic<T>(out T logic) where T : ILogic
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is T tLogic)
                {
                    logic = tLogic;
                    return true;
                }
            }

            logic = default;
            return false;
        }

        public bool AddLogic(ILogic logic)
        {
            if (logic == null)
            {
                return false;
            }

            if (this.logics.Contains(logic))
            {
                return false;
            }

            this.logics.Add(logic);
            this.OnLogicAdded?.Invoke(this, logic);
            return true;
        }

        public bool AddLogic<T>() where T : ILogic, new()
        {
            return this.AddLogic(new T());
        }

        public bool DelLogic(ILogic logic)
        {
            if (logic == null)
            {
                return false;
            }

            if (!this.logics.Remove(logic))
            {
                return false;
            }

            this.OnLogicDeleted?.Invoke(this, logic);
            return true;
        }

        public bool DelLogic<T>() where T : ILogic
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is T behaviour)
                {
                    return this.DelLogic(behaviour);
                }
            }

            return false;
        }

        public bool HasLogic(ILogic logic)
        {
            return this.logics.Contains(logic);
        }

        public bool HasLogic<T>() where T : ILogic
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is T)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}