using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{ 
    [AddComponentMenu("Atomic/Objects/Scene Object World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public sealed class SceneObjectWorld : MonoBehaviour, IObjectWorld
    {
        private static SceneObjectWorld _instance;

        public static SceneObjectWorld Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<SceneObjectWorld>();
                return _instance;
            }
        }

        private readonly ObjectWorld _world = new();

        #region Objects

        public event Action<IObject> OnObjectSpawned
        {
            add => _world.OnObjectSpawned += value;
            remove => _world.OnObjectSpawned -= value;
        }

        public event Action<IObject> OnObjectUnspawned
        {
            add => _world.OnObjectUnspawned += value;
            remove => _world.OnObjectUnspawned -= value;
        }

        [ShowInInspector, ReadOnly]
        public IReadOnlyList<IObject> Objects
        {
            get { return _world.Objects; }
        }

        public IObject GetObject(int objectId)
        {
            return _world.GetObject(objectId);
        }

        public IObject SpawnObject(string name = null, bool enabled = true, bool autoRun = true)
        {
            return _world.SpawnObject(name, enabled, autoRun);
        }

        public bool UnspawnObject(int objectId)
        {
            return _world.UnspawnObject(objectId);
        }

        public bool UnspawnObject(IObject obj)
        {
            return _world.UnspawnObject(obj);
        }

        public void ConstructObject(IObject obj)
        {
            _world.ConstructObject(obj);
        }

        public void OnEnableObject(IObject obj)
        {
            _world.OnEnableObject(obj);
        }

        public void OnDisableObject(IObject obj)
        {
            _world.OnDisableObject(obj);
        }

        public void OnDisposeObject(IObject obj)
        {
            _world.DisposeObject(obj);
        }

        #endregion

        #region Lifecycle

        [ShowInInspector, ReadOnly, PropertyOrder(-100)]
        public bool Enabled
        {
            get { return _world.Enabled; }
        }

        [ShowInInspector, ReadOnly, PropertyOrder(-100)]
        public bool Constructed
        {
            get { return _world.Constructed; }
        }

        public void Run()
        {
            this.Install();
            this.Construct();
            this.Enable();
        }

        public void RunObject(SceneObject sceneObject)
        {
            this.InstallObject(sceneObject);
            this.ConstructObject(sceneObject);
            this.OnEnableObject(sceneObject);
        }

        public void Install()
        {
            SceneObject[] sceneObjects = FindObjectsOfType<SceneObject>(includeInactive: true);

            int count = sceneObjects.Length;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                this.InstallObject(sceneObjects[i]);
            }
        }

        public void InstallObject(SceneObject sceneObject)
        {
            string name = sceneObject.name;
            bool enabled = sceneObject.enabled && sceneObject.gameObject.activeInHierarchy;
            IObject pureObject = _world.SpawnObject(name, enabled, false);
            sceneObject.Install(pureObject);
        }

        public void Construct()
        {
            _world.Construct();
        }

        public void Dispose()
        {
            _world.Dispose();
        }

        public void Enable()
        {
            _world.Enable();
        }

        public void Disable()
        {
            _world.Disable();
        }

        public void Tick(float deltaTime)
        {
            _world.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _world.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            _world.LateTick(deltaTime);
        }

        public IObject GetObjectWithTag(int tag)
        {
            return _world.GetObjectWithTag(tag);
        }

        public IObject[] GetObjectsWithTag(int tag)
        {
            return _world.GetObjectsWithTag(tag);
        }

        #endregion

        #region Unity

        [HideInPlayMode]
        [SerializeField]
        private Settings settings = new()
        {
            installOnAwake = true,
            composeOnAwake = true,
            disposeOnDestroy = true,
            unityControl = true
        };

        private void Awake()
        {
            if (this.settings.installOnAwake)
            {
                this.Install();
            }

            if (this.settings.composeOnAwake)
            {
                this.Construct();
            }

            if (!this.settings.unityControl)
            {
                this.enabled = false;
            }
        }

        private void OnDestroy()
        {
            if (this.settings.disposeOnDestroy)
            {
                this.Dispose();
            }
        }

        private void OnEnable()
        {
            if (this.settings.unityControl)
            {
                this.Enable();
            }
        }

        private void OnDisable()
        {
            if (this.settings.unityControl)
            {
                this.Disable();
            }
        }

        private void Update()
        {
            if (this.settings.unityControl)
            {
                this.Tick(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.settings.unityControl)
            {
                this.FixedTick(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.settings.unityControl)
            {
                this.LateTick(Time.deltaTime);
            }
        }

        #endregion

        [Serializable]
        public struct Settings
        {
            [SerializeField]
            public bool installOnAwake;

            [SerializeField]
            public bool composeOnAwake;

            [SerializeField]
            public bool disposeOnDestroy;

            [SerializeField]
            public bool unityControl;
        }

        public static SceneObjectWorld Create(Settings settings)
        {
            var go = new GameObject("Object World");
            go.SetActive(false);

            SceneObjectWorld world = go.AddComponent<SceneObjectWorld>();
            world.settings = settings;

            if (_instance == null)
            {
                _instance = world;
            }

            go.SetActive(true);
            return world;
        }
    }
}