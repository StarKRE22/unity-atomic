using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contexts
{
    [AddComponentMenu("Atomic/Context/Scene Context")]
    [DefaultExecutionOrder(-1000)]
    public sealed class SceneContext : MonoBehaviour, IContext
    {
        #region Main

        [SerializeField]
        private bool controlState = true;

        [ShowIf(nameof(controlState))]
        [SerializeField]
        private bool dependencyInjection = true;

        [Space]
        [SerializeField]
        public SceneContext initialParent;

        [Space]
        [SerializeField]
        public List<SceneContextInstallerBase> installers = new();

        public string Name
        {
            get => this.context.Name;
            set => this.context.Name = value;
        }

        public IContext Parent
        {
            get => this.context.Parent;
            set => this.context.Parent = value;
        }

        private Context context;

        public void Install()
        {
            this.context = new Context(this.name, this.initialParent);

            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneContextInstallerBase installer = this.installers[i];
                if (installer != null)
                {
                    installer.Install(this.context);
                }
            }
        }

        public bool IsParent(IContext context)
        {
            return context.IsParent(context);
        }

        #endregion

        #region Unity

        private void Awake()
        {
            if (this.controlState)
            {
                this.Install();
            }
            else
            {
                this.enabled = false;
            }
        }

        private void Start()
        {
            if (this.controlState)
            {
                if (this.dependencyInjection)
                {
                    this.context.Construct();
                }
                
                this.context.Initialize();
                this.context.Enable();
            }
        }

        private void OnEnable()
        {
            if (this.controlState && this.context.Initialized && !this.context.Enabled)
            {
                this.context.Enable();
            }
        }

        private void OnDisable()
        {
            if (this.controlState && this.context.Initialized && this.context.Enabled)
            {
                this.context.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.controlState && this.context.Initialized)
            {
                this.context.Dispose();
            }
        }

        private void Update()
        {
            if (this.controlState && this.context.Enabled)
            {
                this.context.Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.controlState && this.context.Enabled)
            {
                this.context.Update(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.controlState && this.context.Enabled)
            {
                this.context.LateUpdate(Time.deltaTime);
            }
        }

        private void OnValidate()
        {
            try
            {
                if (!EditorApplication.isPlaying)
                {
                    this.Install();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion

        #region Values

        public event Action<int, object> OnValueAdded
        {
            add => context.OnValueAdded += value;
            remove => context.OnValueAdded -= value;
        }

        public event Action<int, object> OnValueDeleted
        {
            add => context.OnValueDeleted += value;
            remove => context.OnValueDeleted -= value;
        }

        public event Action<int, object> OnValueChanged
        {
            add => context.OnValueChanged += value;
            remove => context.OnValueChanged -= value;
        }

        public IReadOnlyDictionary<int, object> Values => context.Values;

        public bool AddValue(int key, object value)
        {
            return context.AddValue(key, value);
        }

        public void SetValue(int key, object value)
        {
            context.SetValue(key, value);
        }

        public bool DelValue(int key)
        {
            return context.DelValue(key);
        }

        public bool DelValue(int key, out object removed)
        {
            return context.DelValue(key, out removed);
        }

        public bool HasValue(int key)
        {
            return context.HasValue(key);
        }

        public T GetValue<T>(int key) where T : class
        {
            return context.GetValue<T>(key);
        }

        public object GetValue(int key)
        {
            return context.GetValue(key);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return context.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return context.TryGetValue(id, out value);
        }

        #endregion

        #region Systems

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


        public IReadOnlyCollection<ISystem> Systems => context.Systems;

        public T GetSystem<T>() where T : ISystem
        {
            return context.GetSystem<T>();
        }

        public bool TryGetSystem<T>(out T result) where T : ISystem
        {
            return context.TryGetSystem(out result);
        }

        public bool AddSystem(ISystem system)
        {
            return context.AddSystem(system);
        }

        public bool AddSystem<T>() where T : ISystem, new()
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

        #endregion

        #region Lifecycle

        public event Action OnInitiazized
        {
            add => context.OnInitiazized += value;
            remove => context.OnInitiazized -= value;
        }

        public event Action OnEnabled
        {
            add => context.OnEnabled += value;
            remove => context.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => context.OnDisabled += value;
            remove => context.OnDisabled -= value;
        }

        public event Action OnDisposed
        {
            add => context.OnDisposed += value;
            remove => context.OnDisposed -= value;
        }

        public event Action<float> OnUpdate
        {
            add => context.OnUpdate += value;
            remove => context.OnUpdate -= value;
        }

        public event Action<float> OnFixedUpdate
        {
            add => context.OnFixedUpdate += value;
            remove => context.OnFixedUpdate -= value;
        }

        public event Action<float> OnLateUpdate
        {
            add => context.OnLateUpdate += value;
            remove => context.OnLateUpdate -= value;
        }

        public bool Initialized => context.Initialized;

        public bool Enabled => context.Enabled;

        public void ManualInitialize()
        {
            context.Initialize();
        }

        public void ManualEnable()
        {
            context.Enable();
        }

        public void ManualDisable()
        {
            context.Disable();
        }

        public void ManualDispose()
        {
            context.Dispose();
        }

        public void ManualUpdate(float deltaTime)
        {
            context.Update(deltaTime);
        }

        public void ManualFixedUpdate(float deltaTime)
        {
            context.FixedUpdate(deltaTime);
        }

        public void ManualLateUpdate(float deltaTime)
        {
            context.LateUpdate(deltaTime);
        }

        #endregion

        #region Debug

#if UNITY_EDITOR && ODIN_INSPECTOR

        ///Main

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Name")]
        private string NameDebug
        {
            get { return this.context?.Name ?? "undefined"; }
            set
            {
                if (this.context != null) this.context.Name = value;
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, PropertySpace]
        [HideInEditorMode, LabelText("Initialized")]
        private bool InitializedDebug
        {
            get { return this.context?.Initialized ?? default; }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Enabled")]
        private bool EnabledDebug
        {
            get { return this.context?.Enabled ?? default; }
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

                IReadOnlyDictionary<int, object> values = this.context?.Values;
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
            if (this.context != null) this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            if (this.context != null) this.DelValue(ValuesDebug[index].id);
        }

        ///Logics
        private static readonly List<SystemElement> _systemElementsCache = new();

        [InlineProperty]
        private struct SystemElement
        {
            [ShowInInspector, ReadOnly, HideLabel]
            public string name;

            internal readonly ISystem value;

            public SystemElement(ISystem value)
            {
                this.name = value.GetType().Name;
                this.value = value;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Systems")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveSystemElement),
            CustomRemoveIndexFunction = nameof(RemoveSystemElementAt)
        )]
        private List<SystemElement> SystemsDebug
        {
            get
            {
                _systemElementsCache.Clear();

                var logics = this.context?.Systems;
                if (logics == null)
                {
                    return _systemElementsCache;
                }

                foreach (var system in logics)
                {
                    _systemElementsCache.Add(new SystemElement(system));
                }

                return _systemElementsCache;
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
            this.Install();
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add System"), HideInEditorMode]
        private void AddSystemDebug(ISystem system)
        {
            this.context.AddSystem(system);
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add Value"), HideInEditorMode]
        private void AddValueDebug(int key, object value)
        {
            this.context.AddValue(key, value);
        }

        private void RemoveSystemElement(SystemElement systemElement)
        {
            if (this.context != null) this.DelSystem(systemElement.value);
        }

        private void RemoveSystemElementAt(int index)
        {
            if (this.context != null) this.DelSystem(SystemsDebug[index].value);
        }
#endif

        #endregion
    }
}