using System;
using System.Collections.Generic;

namespace Atomic.UI
{
    internal sealed class View : IView
    {
        #region Main

        public event Action<IView, string> OnNameChanged;
        public event Action<IView> OnShown;
        public event Action<IView> OnHidden;

        private string name;
        private bool shown;

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

        public bool IsShown
        {
            get { return this.shown; }
            set
            {
                if (this.shown == value)
                {
                    return;
                }

                this.shown = value;

                if (value)
                {
                    this.OnShow();
                    this.OnShown?.Invoke(this);
                }
                else
                {
                    this.OnHide();
                    this.OnHidden?.Invoke(this);
                }
            }
        }

        private void OnShow()
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                ILogic logic = this.logics[i];
                if (logic is IShow showable)
                {
                    showable.OnShow(this);
                }

                if (logic is IUpdate update)
                {
                    ViewSystem.AddUpdate(update);
                }
            }
        }

        private void OnHide()
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                ILogic logic = this.logics[i];
                if (logic is IHide showable)
                {
                    showable.OnHide(this);
                }

                if (logic is IUpdate update)
                {
                    ViewSystem.RemoveUpdate(update);
                }
            }
        }

        #endregion

        #region Values

        public event Action<IView, int, object> OnValueAdded;
        public event Action<IView, int, object> OnValueDeleted;
        public event Action<IView, int, object> OnValueChanged;

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

            if (this.shown)
            {
                if (logic is IShow show)
                {
                    show.OnShow(this);
                }

                if (logic is IUpdate update)
                {
                    ViewSystem.AddUpdate(update);
                }
            }

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

            if (this.shown)
            {
                if (logic is IHide hide)
                {
                    hide.OnHide(this);
                }

                if (logic is IUpdate update)
                {
                    ViewSystem.RemoveUpdate(update);
                }
            }

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