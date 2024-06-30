using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Atomic.UI
{
    [AddComponentMenu("Atomic/UI/View")]
    public sealed class View : MonoBehaviour, IView, ISerializationCallbackReceiver
    {
        #region Main

        [SerializeReference, HideInPlayMode]
        private IViewInstaller[] installers;

        [LabelText("Behaviours"), HideInPlayMode]
        [SerializeReference]
        private IBehaviour[] initialBehaviours;

        [Header("Events")]
        [SerializeField]
        private UnityEvent initEvent;

        [SerializeField]
        private UnityEvent showEvent;

        [SerializeField]
        private UnityEvent hideEvent;

        [SerializeField]
        private UnityEvent disposeEvent;

        public string Name
        {
            get { return this.name; }
        }

        public bool IsVisible
        {
            get
            {
                return this.enabled &&
                       this.gameObject.activeSelf &&
                       this.gameObject.activeInHierarchy;
            }
        }

        public void SetVisible(bool visible)
        {
            this.enabled = visible;
            this.gameObject.SetActive(visible);
        }

        public void Show()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.OnInstall();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        private void Awake()
        {
            this.OnInitialize();
        }

        private void OnEnable()
        {
            this.OnShow();
        }

        private void OnDisable()
        {
            this.OnHide();
        }

        private void OnDestroy()
        {
            this.OnDispose();
        }

        #endregion

        #region Lifecycle

        public event Action<IView> OnInitialized;
        public event Action<IView> OnDisposed;
        public event Action<IView> OnShown;
        public event Action<IView> OnHidden;

        private bool initialized;

        public bool Initialized
        {
            get { return this.initialized; }
        }

        private void OnInstall()
        {
            this.values = new Dictionary<int, object>();
            this.behaviours = new HashSet<IBehaviour>();

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IViewInstaller installer = this.installers[i];
                    installer?.Install(this);
                }
            }

            if (this.initialBehaviours != null)
            {
                for (int i = 0, count = this.initialBehaviours.Length; i < count; i++)
                {
                    IBehaviour behaviour = this.initialBehaviours[i];
                    if (behaviour != null)
                    {
                        this.AddBehaviour(behaviour);
                    }
                }
            }
        }

        private void OnInitialize()
        {
            this.initialized = true;

            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IInitBehaviour initBehaviour)
                {
                    initBehaviour.Init(this);
                }
            }

            this.initEvent.Invoke();
            this.OnInitialized?.Invoke(this);
        }

        private void OnDispose()
        {
            this.initialized = false;

            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IDisposeBehaviour disposeBehaviour)
                {
                    disposeBehaviour.Dispose(this);
                }
            }

            this.disposeEvent.Invoke();
            this.OnDisposed?.Invoke(this);
        }

        private void OnShow()
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IShowBehaviour showBehaviour)
                {
                    showBehaviour.Show(this);
                }

                if (behaviour is IUpdateBehaviour updateBehaviour)
                {
                    ViewUpdateManager.AddBehaviour(updateBehaviour);
                }
            }

            this.showEvent.Invoke();
            this.OnShown?.Invoke(this);
        }

        private void OnHide()
        {
            ViewUpdateManager.RemoveBehaviours(this.behaviours);

            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IUpdateBehaviour updateBehaviour)
                {
                    ViewUpdateManager.RemoveBehaviour(updateBehaviour);
                }

                if (behaviour is IHideBehaviour hideBehaviour)
                {
                    hideBehaviour.Hide(this);
                }
            }

            this.hideEvent?.Invoke();
            this.OnHidden?.Invoke(this);
        }

        #endregion
        
        #region Values

        public event Action<int, object> OnValueAdded;
        public event Action<int, object> OnValueDeleted;
        public event Action<int, object> OnValueChanged;

        public IReadOnlyDictionary<int, object> Values => this.values;

        private Dictionary<int, object> values = new();

        public bool HasValue(int key)
        {
            return this.values.ContainsKey(key);
        }

        public T GetValue<T>(int key) where T : class
        {
            if (this.values.TryGetValue(key, out object value))
            {
                return value as T;
            }

            return null;
        }

        public object GetValue(int key)
        {
            if (this.values.TryGetValue(key, out var value))
            {
                return value;
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

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public bool AddValue(int key, object value)
        {
            if (this.values.TryAdd(key, value))
            {
                this.OnValueAdded?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public void SetValue(int key, object value)
        {
            this.values[key] = value;
            this.OnValueChanged?.Invoke(key, value);
        }

        public bool DelValue(int key)
        {
            if (this.values.Remove(key, out object removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        public bool DelValue(int key, out object removed)
        {
            if (this.values.Remove(key, out removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        #endregion

        #region Behaviours

        public event Action<IBehaviour> OnBehaviourAdded;
        public event Action<IBehaviour> OnBehaviourRemoved;

        public IReadOnlyCollection<IBehaviour> Behaviours => this.behaviours;

        private HashSet<IBehaviour> behaviours;

        public T GetBehaviour<T>() where T : IBehaviour
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is T tBehaviour)
                {
                    return tBehaviour;
                }
            }

            return default;
        }

        public bool TryGetBehaviour<T>(out T result) where T : IBehaviour
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is T tBehaviour)
                {
                    result = tBehaviour;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool HasBehaviour(IBehaviour behaviour)
        {
            return this.behaviours.Contains(behaviour);
        }

        public bool HasBehaviour<T>() where T : IBehaviour
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is T)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddBehaviour<T>() where T : IBehaviour, new()
        {
            return this.AddBehaviour(new T());
        }

        public bool AddBehaviour(IBehaviour behaviour)
        {
            if (!this.behaviours.Add(behaviour))
            {
                return false;
            }

            if (this.initialized && behaviour is IInitBehaviour initBehaviour)
            {
                initBehaviour.Init(this);

                if (this.IsVisible)
                {
                    if (behaviour is IShowBehaviour showBehaviour)
                    {
                        showBehaviour.Show(this);
                    }

                    if (behaviour is IUpdateBehaviour updateBehaviour)
                    {
                        ViewUpdateManager.AddBehaviour(updateBehaviour);
                    }
                }
            }

            this.OnBehaviourAdded?.Invoke(behaviour);
            return true;
        }

        public bool DelBehaviour<T>() where T : IBehaviour
        {
            T behaviour = this.GetBehaviour<T>();
            if (behaviour == null)
            {
                return false;
            }

            return this.DelBehaviour(behaviour);
        }

        public bool DelBehaviour(IBehaviour behaviour)
        {
            if (!this.behaviours.Remove(behaviour))
            {
                return false;
            }

            if (this.IsVisible)
            {
                if (behaviour is IUpdateBehaviour updateBehaviour)
                {
                    ViewUpdateManager.RemoveBehaviour(updateBehaviour);
                }

                if (behaviour is IHideBehaviour showBehaviour)
                {
                    showBehaviour.Hide(this);
                }
            }

            if (this.initialized && behaviour is IDisposeBehaviour disposeBehaviour)
            {
                disposeBehaviour.Dispose(this);
            }

            this.OnBehaviourRemoved?.Invoke(behaviour);
            return true;
        }

        #endregion

        #region Debug

        //TODO:

        #endregion
    }
}