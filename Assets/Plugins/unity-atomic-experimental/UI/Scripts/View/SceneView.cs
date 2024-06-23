using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI
{
    public class SceneView : MonoBehaviour, IView, ISerializationCallbackReceiver
    {
        #region Install

        [SerializeReference]
        protected IViewInstaller[] installers;

        private bool installed;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Install();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        private void Install()
        {
            if (this.installed)
            {
                return;
            }
            
            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IViewInstaller installer = this.installers[i];
                    installer?.Install(this);
                }
            }

            this.installed = true;
        }

        #endregion

        #region Main

        private void OnEnable()
        {
            this.view.IsShown = true;
        }

        private void OnDisable()
        {
            this.view.IsShown = false;
        }
        
        private readonly View view = new();

        public event Action<IView, string> OnNameChanged
        {
            add => view.OnNameChanged += value;
            remove => view.OnNameChanged -= value;
        }

        public event Action<IView> OnShown
        {
            add => view.OnShown += value;
            remove => view.OnShown -= value;
        }

        public event Action<IView> OnHidden
        {
            add => view.OnHidden += value;
            remove => view.OnHidden -= value;
        }

        public string Name
        {
            get => view.Name;
            set => view.Name = value;
        }

        public bool IsShown
        {
            get
            {
                return this.enabled &&
                       this.gameObject.activeSelf &&
                       this.gameObject.activeInHierarchy;
            }
            set
            {
                this.gameObject.SetActive(value);
            }
        }

        #endregion

        #region Values

        public event Action<IView, int, object> OnValueAdded
        {
            add => view.OnValueAdded += value;
            remove => view.OnValueAdded -= value;
        }

        public event Action<IView, int, object> OnValueDeleted
        {
            add => view.OnValueDeleted += value;
            remove => view.OnValueDeleted -= value;
        }

        public event Action<IView, int, object> OnValueChanged
        {
            add => view.OnValueChanged += value;
            remove => view.OnValueChanged -= value;
        }

        public IReadOnlyDictionary<int, object> Values => view.Values;

        public T GetValue<T>(int id) where T : class
        {
            return view.GetValue<T>(id);
        }

        public object GetValue(int id)
        {
            return view.GetValue(id);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return view.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return view.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return view.AddValue(id, value);
        }

        public void SetValue(int id, object value)
        {
            view.SetValue(id, value);
        }

        public bool DelValue(int id)
        {
            return view.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return view.DelValue(id, out removed);
        }

        public bool HasValue(int id)
        {
            return view.HasValue(id);
        }

        #endregion

        #region Logic

        public IReadOnlyList<ILogic> Logics => view.Logics;

        public T GetLogic<T>() where T : ILogic
        {
            return view.GetLogic<T>();
        }

        public bool TryGetLogic<T>(out T logic) where T : ILogic
        {
            return view.TryGetLogic(out logic);
        }

        public bool AddLogic(ILogic logic)
        {
            return view.AddLogic(logic);
        }

        public bool AddLogic<T>() where T : ILogic, new()
        {
            return view.AddLogic<T>();
        }

        public bool DelLogic(ILogic logic)
        {
            return view.DelLogic(logic);
        }

        public bool DelLogic<T>() where T : ILogic
        {
            return view.DelLogic<T>();
        }

        public bool HasLogic(ILogic logic)
        {
            return view.HasLogic(logic);
        }

        public bool HasLogic<T>() where T : ILogic
        {
            return view.HasLogic<T>();
        }

        #endregion
    }
}