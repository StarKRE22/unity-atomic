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

        private Context context;

        [SerializeField]
        private bool controlState = true;

        [Space]
        [SerializeField]
        public SceneContext initialParent;

        [SerializeField]
        public List<SceneContext> initialChildren = new();

        [Space]
        [SerializeField]
        public List<SceneContextInstallerBase> installers = new();

        public void Install()
        {
            context = new Context(this.name, this.initialParent, this.initialChildren);

            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneContextInstallerBase installer = installers[i];
                if (installer != null)
                {
                    installer.Install(context);
                }
            }
        }

        #endregion

        #region Unity

        private void Awake()
        {
            if (this.controlState)
            {
                this.Install();
                context.Initialize();
            }
            else
            {
                this.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (this.controlState)
            {
                context.Enable();
            }
        }

        private void OnDisable()
        {
            if (this.controlState)
            {
                context.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.controlState)
            {
                context.Dispose();
            }
        }

        private void Update()
        {
            if (this.controlState)
            {
                context.Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.controlState)
            {
                context.Update(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.controlState)
            {
                context.LateUpdate(Time.deltaTime);
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

        #region Common
        
        public string Name
        {
            get => context.Name;
            set => context.Name = value;
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

        public event Action<ContextState> OnStateChanged
        {
            add => context.OnStateChanged += value;
            remove => context.OnStateChanged -= value;
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

        public ContextState State => context.State;

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

        #region Parent

        public IContext Parent
        {
            get => context.Parent;
            set => context.Parent = value;
        }

        public ICollection<IContext> Children => context.Children;

        public bool IsChild(IContext context)
        {
            return context.IsChild(context);
        }

        public bool IsParent(IContext context)
        {
            return context.IsParent(context);
        }

        public bool AddChild(IContext child)
        {
            return context.AddChild(child);
        }

        public bool DelChild(IContext child)
        {
            return context.DelChild(child);
        }

        #endregion

        #region Debug

        public static IValueNameFormatter ValueNameFormatter;

        public interface IValueNameFormatter
        {
            string GetName(int id);
        }

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
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Alive")]
        private ContextState StateDebug
        {
            get { return this.context?.State ?? default; }
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
                    string name = ValueNameFormatter?.GetName(id) ?? id.ToString();
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

            public SystemElement(string name, ISystem value)
            {
                this.name = name;
                this.value = value;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Logics")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveSystemElement),
            CustomRemoveIndexFunction = nameof(RemoveSystemElementAt),
            HideAddButton = true
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
                    string name = system.GetType().Name;
                    _systemElementsCache.Add(new SystemElement(name, system));
                }

                return _systemElementsCache;
            }
            set
            {
                /** noting... **/
            }
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