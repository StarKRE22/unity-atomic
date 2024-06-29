using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contexts
{
    [AddComponentMenu("Atomic/Context/Scene Context")]
    public sealed class SceneContext : MonoBehaviour, IContext
    {
        #region Main

        private Context context;
        
        [SerializeField]
        private SceneContext initialParent;

        [SerializeField]
        private SceneContext[] initialChildren;

        [SerializeField]
        private SceneContextInstallerBase[] installers;

        public void Install()
        {
            this.context = new Context(this.name, this.initialParent, this.initialChildren);

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    SceneContextInstallerBase installer = this.installers[i];
                    if (installer != null)
                    {
                        installer.Install(this);
                    }
                }
            }
        }

        #endregion

        #region Unity
        
        [SerializeField]
        private bool controlState;
        

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

        // private void Start()
        // {
        //     if (this.controlState.)
        //     {
        //         
        //     }
        // }

        private void OnValidate()
        {
            if (!EditorApplication.isPlaying)
            {
                this.context = new Context(this.name, this.initialParent);
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

        public ContextState State => context.State;

        public void ManualInitialize()
        {
            this.context.Initialize();
        }

        public void ManualEnable()
        {
            this.context.Enable();
        }

        public void ManualDisable()
        {
            this.context.Disable();
        }

        public void ManualDestroy()
        {
            this.context.Destroy();
        }

        public void ManualUpdate(float deltaTime)
        {
            this.context.Update(deltaTime);
        }

        public void ManualFixedUpdate(float deltaTime)
        {
            this.context.FixedUpdate(deltaTime);
        }

        public void ManualLateUpdate(float deltaTime)
        {
            this.context.LateUpdate(deltaTime);
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
            return this.context.IsChild(context);
        }

        public bool IsParent(IContext context)
        {
            return this.context.IsParent(context);
        }
        

        public bool AddChild(IContext child)
        {
            return this.context.AddChild(child);
        }

        public bool DelChild(IContext child)
        {
            return this.context.DelChild(child);
        }

        #endregion
    }
}