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

        [Space]
        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent initEvent;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent showEvent;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent hideEvent;

        [FoldoutGroup("Events")]
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
            this.enabled = false;
            this.gameObject.SetActive(false);
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

        private void OnValidate()
        {
            this.gameObject.SetActive(this.enabled);
            this.enabled = this.gameObject.activeSelf;
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
                    ViewUpdateManager.AddBehaviour(this, updateBehaviour);
                }
            }

            this.showEvent.Invoke();
            this.OnShown?.Invoke(this);
        }

        private void OnHide()
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IUpdateBehaviour updateBehaviour)
                {
                    ViewUpdateManager.RemoveBehaviour(this, updateBehaviour);
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
                        ViewUpdateManager.AddBehaviour(this, updateBehaviour);
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
                    ViewUpdateManager.RemoveBehaviour(this, updateBehaviour);
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

#if UNITY_EDITOR && ODIN_INSPECTOR

        ///Main
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, PropertySpace]
        [HideInEditorMode, LabelText("Initialized")]
        private bool InitializedDebug
        {
            get { return this.initialized; }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Visible")]
        private bool VisibleDebug
        {
            get { return this.IsVisible; }
        }

        ///Values
        private static readonly List<ValueElement> _valueElementsCache = new();

        private struct ValueElement
        {
            [HorizontalGroup(200), ShowInInspector, ReadOnly, HideLabel]
            public string name;

            [HorizontalGroup, ShowInInspector, HideLabel]
            public object value;

            internal readonly int id;

            public ValueElement(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Values")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveValueElement),
            CustomRemoveIndexFunction = nameof(RemoveValueElementAt),
            HideAddButton = true
        )]
        private List<ValueElement> ValuesDebug
        {
            get
            {
                _valueElementsCache.Clear();

                if (values == null)
                {
                    return _valueElementsCache;
                }

                foreach ((int id, object value) in values)
                {
                    string name = DebugUtils.ConvertToName(id);
                    _valueElementsCache.Add(new ValueElement(name, value, id));
                }

                return _valueElementsCache;
            }

            set
            {
                /** noting... **/
            }
        }

        private void RemoveValueElement(ValueElement valueElement)
        {
            this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            this.DelValue(ValuesDebug[index].id);
        }

        ///Logics
        private static readonly List<BehaviourElement> _behaviourElementsCache = new();

        [InlineProperty]
        private struct BehaviourElement
        {
            [ShowInInspector, ReadOnly, HideLabel]
            public string name;

            internal readonly IBehaviour value;

            public BehaviourElement(IBehaviour value)
            {
                this.name = value.GetType().Name;
                this.value = value;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveBehaviourElement),
            CustomRemoveIndexFunction = nameof(RemoveBehaviourElementAt)
        )]
        private List<BehaviourElement> BehavioursDebug
        {
            get
            {
                _behaviourElementsCache.Clear();

                if (this.behaviours == null)
                {
                    return _behaviourElementsCache;
                }

                foreach (var behaviour in this.behaviours)
                {
                    _behaviourElementsCache.Add(new BehaviourElement(behaviour));
                }

                return _behaviourElementsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Refresh"), HideInPlayMode]
        private void Refresh()
        {
            this.OnInstall();
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add Behaviour"), HideInEditorMode]
        private void AddBehaviourDebug(IBehaviour behaviour)
        {
            this.AddBehaviour(behaviour);
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add Value"), HideInEditorMode]
        private void AddValueDebug(int key, object value)
        {
            this.AddValue(key, value);
        }

        private void RemoveBehaviourElement(BehaviourElement behaviourElement)
        {
            this.DelBehaviour(behaviourElement.value);
        }

        private void RemoveBehaviourElementAt(int index)
        {
            this.DelBehaviour(BehavioursDebug[index].value);
        }
#endif

        #endregion
    }
}