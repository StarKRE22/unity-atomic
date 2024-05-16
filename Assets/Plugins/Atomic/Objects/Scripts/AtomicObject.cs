using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Atomic Object")]
    [DisallowMultipleComponent]
    public sealed class AtomicObject : MonoBehaviour, IAtomicObject, IDisposable
    {
        #region Tags

        private ITagCollection tags;

        public bool DelTag(int tag)
        {
            return this.tags.Remove(tag);
        }

        public bool HasTag(int tag)
        {
            return this.tags.Contains(tag);
        }

        public bool AddTag(int tag)
        {
            return this.tags.Add(tag);
        }

        #endregion

        #region References

        private IReferenceCollection references;

        public T GetReference<T>(int id) where T : class
        {
            if (this.references.TryGetValue(id, out object value))
            {
                return value as T;
            }

            return null;
        }

        public bool TryGetReference<T>(int id, out T value) where T : class
        {
            if (this.references.TryGetValue(id, out object field))
            {
                value = field as T;
                return true;
            }

            value = default;
            return false;
        }

        public object GetReference(int id)
        {
            return this.references[id];
        }

        public void SetReference(int id, object value)
        {
            this.DelReference(id);
            this.AddReference(id, value);
        }

        public bool TryGetReference(int id, out object value)
        {
            return this.references.TryGetValue(id, out value);
        }

        public bool AddReference(int id, object value)
        {
            return this.references.TryAdd(id, value);
        }

        public bool DelReference(int id)
        {
            return this.references.Remove(id);
        }

        public bool DelReference(int id, out object removed)
        {
            return this.references.Remove(id, out removed);
        }

        #endregion

        #region Behaviours

        private List<IAtomicObject.IBehaviour> behaviours;

        private List<IAtomicObject.IUpdate> updates;
        private List<IAtomicObject.IFixedUpdate> fixedUpdates;
        private List<IAtomicObject.ILateUpdate> lateUpdates;

        private List<IAtomicObject.IUpdate> _updateCache;
        private List<IAtomicObject.IFixedUpdate> _fixedUpdateCache;
        private List<IAtomicObject.ILateUpdate> _lateUpdateCache;

#if UNITY_EDITOR
        private List<IAtomicObject.IDrawGizmos> drawGizmoses;
#endif
        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
        }

        public bool AddBehaviour(IAtomicObject.IBehaviour behaviour)
        {
            if (behaviour == null)
            {
                return false;
            }

            if (this.behaviours.Contains(behaviour))
            {
                return false;
            }

            this.behaviours.Add(behaviour);

            if (_enabled)
            {
                if (behaviour is IAtomicObject.IAwake awake)
                {
                    awake.OnAwake(this);
                }

                if (behaviour is IAtomicObject.IEnable enable)
                {
                    enable.Enable(this);
                }
            }

            if (behaviour is IAtomicObject.IUpdate update)
            {
                this.updates.Add(update);
            }

            if (behaviour is IAtomicObject.IFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (behaviour is IAtomicObject.ILateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }

#if UNITY_EDITOR
            if (behaviour is IAtomicObject.IDrawGizmos gizmos)
            {
                this.drawGizmoses.Add(gizmos);
            }
#endif
            return true;
        }

        public bool AddBehaviour<T>() where T : IAtomicObject.IBehaviour, new()
        {
            return this.AddBehaviour(new T());
        }

        public bool DelBehaviour(IAtomicObject.IBehaviour behaviour)
        {
            if (behaviour == null)
            {
                return false;
            }

            if (!this.behaviours.Remove(behaviour))
            {
                return false;
            }

            if (behaviour is IAtomicObject.IUpdate tickable)
            {
                this.updates.Remove(tickable);
            }

            if (behaviour is IAtomicObject.IFixedUpdate fixedTickable)
            {
                this.fixedUpdates.Remove(fixedTickable);
            }

            if (behaviour is IAtomicObject.ILateUpdate lateTickable)
            {
                this.lateUpdates.Remove(lateTickable);
            }

#if UNITY_EDITOR
            if (behaviour is IAtomicObject.IDrawGizmos gizmos)
            {
                this.drawGizmoses.Remove(gizmos);
            }
#endif
            if (_enabled)
            {
                if (behaviour is IAtomicObject.IDisable disable)
                {
                    disable.Disable(this);
                }

                if (behaviour is IAtomicObject.IDisposable destroy)
                {
                    destroy.Dispose(this);
                }
            }

            return true;
        }

        public bool DelBehaviour<T>() where T : IAtomicObject.IBehaviour
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is T behaviour)
                {
                    return this.DelBehaviour(behaviour);
                }
            }

            return false;
        }

        [ContextMenu("Awake")]
        public void OnAwake()
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var behaviour = this.behaviours[i];
                if (behaviour is IAtomicObject.IAwake awake)
                {
                    awake.OnAwake(this);
                }
            }
        }

        [ContextMenu("OnEnable")]
        public void Enable()
        {
            _enabled = true;

            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var behaviour = this.behaviours[i];
                if (behaviour is IAtomicObject.IEnable enable)
                {
                    enable.Enable(this);
                }
            }
        }

        [ContextMenu("OnDisable")]
        public void Disable()
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var behaviour = this.behaviours[i];
                if (behaviour is IAtomicObject.IDisable disable)
                {
                    disable.Disable(this);
                }
            }

            _enabled = false;
        }

        [ContextMenu("OnDestroy")]
        public void Destroy()
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var element = this.behaviours[i];
                if (element is IAtomicObject.IDisposable destroy)
                {
                    destroy.Dispose(this);
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.updates.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updates);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IAtomicObject.IUpdate update = _updateCache[i];
                update.OnUpdate(this, deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.fixedUpdates.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedUpdates);

            for (int i = 0, count = _fixedUpdateCache.Count; i < count; i++)
            {
                IAtomicObject.IFixedUpdate fixedUpdate = _fixedUpdateCache[i];
                fixedUpdate.OnFixedUpdate(this, deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (this.lateUpdates.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateUpdates);

            for (int i = 0, count = _lateUpdateCache.Count; i < count; i++)
            {
                IAtomicObject.ILateUpdate lateUpdate = _lateUpdateCache[i];
                lateUpdate.OnLateUpdate(this, deltaTime);
            }
        }

        public void TriggerEnter(Collider collider)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var element = this.behaviours[i];
                if (element is IAtomicObject.ITriggerEnter trigger)
                {
                    trigger.TriggerEnter(this, collider);
                }
            }
        }

        public void TriggerExit(Collider collider)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                var element = this.behaviours[i];
                if (element is IAtomicObject.ITriggerExit trigger)
                {
                    trigger.TriggerExit(this, collider);
                }
            }
        }

        public void TriggerEnter2D(Collider2D collider)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ITriggerEnter2D trigger)
                {
                    trigger.TriggerEnter2D(this, collider);
                }
            }
        }

        public void TriggerExit2D(Collider2D collider)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ITriggerExit2D trigger)
                {
                    trigger.TriggerExit2D(this, collider);
                }
            }
        }

        public void CollisionEnter(Collision collision)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ICollisionEnter handler)
                {
                    handler.CollisionEnter(this, collision);
                }
            }
        }

        public void CollisionExit(Collision collision)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ICollisionExit handler)
                {
                    handler.CollisionExit(this, collision);
                }
            }
        }

        public void CollisionEnter2D(Collision2D collision)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ICollisionEnter2D handler)
                {
                    handler.CollisionEnter2D(this, collision);
                }
            }
        }

        public void CollisionExit2D(Collision2D collision)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IAtomicObject.ICollisionExit2D handler)
                {
                    handler.CollisionExit2D(this, collision);
                }
            }
        }


#if UNITY_EDITOR
        public void OnGizmosDraw()
        {
            foreach (IAtomicObject.IDrawGizmos gizmos in this.drawGizmoses)
            {
                gizmos.OnGizmosDraw(this);
            }
        }
#endif

        #endregion

        #region Setup
        
        [Tooltip("Will the Unity callbacks control the behavior of the object?")]
        [PropertyOrder(80)]
        [HideInPlayMode]
        [ShowIf(nameof(hasBehaviours))]
        [SerializeField]
        private bool unityControl = true;
        
        [Space]
        [PropertyOrder(80)]
        [Tooltip("Do need to compose the object on Awake?")]
        [HideInPlayMode]
        [SerializeField]
        private bool composeOnAwake = true;

        [Tooltip("Do need to dispose the object on Destroy?")]
        [HideInPlayMode]
        [PropertyOrder(80)]
        [SerializeField]
        private bool disposeOnDestroy = true;


        [Title("Installing")]
        [PropertyOrder(90)]
        [HideInPlayMode]
        [SerializeField]
        private List<MonoBehaviour> monoInstallers;

        [PropertyOrder(90)]
        [HideInPlayMode]
        [SerializeField]
        private List<ScriptableObject> scriptableInstallers;

        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private bool hasBehaviours = true;

        [Space]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private TagsStorageType tagStorageType;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(tagStorageType), TagsStorageType.HASHSET)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int tagCapacity;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(tagStorageType), Value = TagsStorageType.ARRAY)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int tagArraySize;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(tagStorageType), TagsStorageType.SEGMENT)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int tagSegmentStart;

        [EnableIf(nameof(tagStorageType), TagsStorageType.SEGMENT)]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int tagSegmentEnd;

        [Space]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private ReferenceStorageType referenceStorageType;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(referenceStorageType), ReferenceStorageType.DICTIONARY)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int referenceCapacity;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(referenceStorageType), ReferenceStorageType.ARRAY)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int referenceArraySize;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(referenceStorageType), ReferenceStorageType.SEGMENT)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int referenceSegmentStart;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(referenceStorageType), ReferenceStorageType.SEGMENT)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int referenceSegmentEnd;

        [Serializable]
        public enum TagsStorageType
        {
            HASHSET = 0,
            ARRAY = 1,
            SEGMENT = 2
        }

        [Serializable]
        public enum ReferenceStorageType
        {
            DICTIONARY = 0,
            ARRAY = 1,
            SEGMENT = 2
        }

        [ContextMenu("Compose")]
        public void Compose()
        {
            this.tags = this.tagStorageType switch
            {
                TagsStorageType.HASHSET => new TagHashSet(this.tagCapacity),
                TagsStorageType.ARRAY => new TagArray(this.tagArraySize),
                TagsStorageType.SEGMENT => new TagSegment(this.tagSegmentStart, this.tagSegmentEnd),
                _ => throw new ArgumentOutOfRangeException()
            };

            this.references = this.referenceStorageType switch
            {
                ReferenceStorageType.DICTIONARY => new ReferenceDictionary(this.referenceCapacity),
                ReferenceStorageType.ARRAY => new ReferenceArray(this.referenceArraySize),
                ReferenceStorageType.SEGMENT => new ReferenceSegment(this.referenceSegmentStart, this.referenceSegmentEnd),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (this.hasBehaviours)
            {
                this.behaviours = new List<IAtomicObject.IBehaviour>();
                this.updates = new List<IAtomicObject.IUpdate>();
                this.fixedUpdates = new List<IAtomicObject.IFixedUpdate>();
                this.lateUpdates = new List<IAtomicObject.ILateUpdate>();

                _updateCache = new List<IAtomicObject.IUpdate>();
                _fixedUpdateCache = new List<IAtomicObject.IFixedUpdate>();
                _lateUpdateCache = new List<IAtomicObject.ILateUpdate>();

#if UNITY_EDITOR
                this.drawGizmoses = new List<IAtomicObject.IDrawGizmos>();
#endif
            }

            if (this.monoInstallers is {Count: > 0})
            {
                for (int i = 0, count = this.monoInstallers.Count; i < count; i++)
                {
                    var source = this.monoInstallers[i];
                    if (source == null)
                    {
                        continue;
                    }

                    if (source is IAtomicObject.IComposable composable)
                    {
                        composable.Compose(this);
                    }

                    ObjectInflater.InflateFrom(this, source);

                    if (source is IAtomicObject.IBehaviour behaviour)
                    {
                        this.AddBehaviour(behaviour);
                    }
                }
            }

            if (this.scriptableInstallers is {Count: > 0})
            {
                for (int i = 0, count = this.scriptableInstallers.Count; i < count; i++)
                {
                    var source = this.scriptableInstallers[i];
                    if (source == null)
                    {
                        continue;
                    }

                    if (source is IAtomicObject.IComposable composable)
                    {
                        composable.Compose(this);
                    }

                    ObjectInflater.InflateFrom(this, source);

                    if (source is IAtomicObject.IBehaviour behaviour)
                    {
                        this.AddBehaviour(behaviour);
                    }
                }
            }
        }

        [ContextMenu("Dispose")]
        public void Dispose()
        {
            if (this.monoInstallers is {Count: > 0})
            {
                for (int i = 0, count = this.monoInstallers.Count; i < count; i++)
                {
                    var source = this.monoInstallers[i];
                    if (source == null)
                    {
                        continue;
                    }

                    if (source is IDisposable composable)
                    {
                        composable.Dispose();
                    }
                }
            }

            if (this.scriptableInstallers is {Count: > 0})
            {
                for (int i = 0, count = this.scriptableInstallers.Count; i < count; i++)
                {
                    var source = this.scriptableInstallers[i];
                    if (source == null)
                    {
                        continue;
                    }

                    if (source is IDisposable composable)
                    {
                        composable.Dispose();
                    }
                }
            }
        }

        #endregion

        #region Unity

        private void Awake()
        {
            if (this.composeOnAwake)
            {
                this.Compose();
            }

            if (this.unityControl)
            {
                this.OnAwake();
            }
            else
            {
                this.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (this.unityControl)
            {
                this.Enable();
            }
        }

        private void Update()
        {
            if (this.unityControl)
            {
                this.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.unityControl)
            {
                this.OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.unityControl)
            {
                this.OnLateUpdate(Time.deltaTime);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (this.unityControl)
            {
                this.OnGizmosDraw();
            }
        }
#endif
        private void OnDisable()
        {
            if (this.unityControl)
            {
                this.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.unityControl)
            {
                this.Destroy();
            }

            if (this.disposeOnDestroy)
            {
                this.Dispose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (this.unityControl)
            {
                this.TriggerEnter(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (this.unityControl)
                this.TriggerExit(other);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (this.unityControl)
                this.TriggerEnter2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (this.unityControl)
                this.TriggerExit2D(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (this.unityControl)
                this.CollisionEnter(collision);
        }

        private void OnCollisionExit(Collision other)
        {
            if (this.unityControl)
                this.CollisionEnter(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (this.unityControl)
                this.CollisionEnter2D(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (this.unityControl)
                this.CollisionExit2D(other);
        }

        private void OnValidate()
        {
            if (!this.hasBehaviours)
            {
                this.unityControl = false;
            }
        }

        private void Reset()
        {
            this.hasBehaviours = true;
            this.unityControl = true;
            this.composeOnAwake = true;
            this.disposeOnDestroy = true;

            this.monoInstallers = new List<MonoBehaviour>();

            foreach (var monoBehaviour in this.GetComponentsInChildren<MonoBehaviour>())
            {
                if (monoBehaviour is IAtomicObject.IComposable or IAtomicObject.IBehaviour)
                {
                    this.monoInstallers.Add(monoBehaviour);
                }
            }

            this.scriptableInstallers = new List<ScriptableObject>();
        }

        #endregion

        #region Editor

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [Button, HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        [ContextMenu("Compute Capacity", isValidateFunction:false, priority:1000002)]
        private void ComputeCapacity()
        {
            var prevTagType = this.tagStorageType;
            var prevRefType = this.referenceStorageType;

            this.tagStorageType = TagsStorageType.HASHSET;
            this.referenceStorageType = ReferenceStorageType.DICTIONARY;
            
            //Make compose for compute capacity!
            this.Compose();

            HashSet<int> tags = (HashSet<int>) this.tags;
            this.tagCapacity = tags.Count;
            this.tagSegmentStart = tags.Min(it => it);
            this.tagSegmentEnd = tags.Max(it => it);
            this.tagArraySize = this.tagSegmentEnd + 1;

            Dictionary<int, object> reference = (Dictionary<int, object>) this.references;
            this.referenceCapacity = reference.Count;
            this.referenceSegmentStart = reference.Min(it => it.Key);
            this.referenceSegmentEnd = reference.Max(it => it.Key);
            this.referenceArraySize = this.referenceSegmentEnd + 1;

            this.tagStorageType = prevTagType;
            this.referenceStorageType = prevRefType;
        }
#endif

        #endregion

        #region Debug

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Tags")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(OnRemoveTagDebugById),
            CustomRemoveIndexFunction = nameof(OnRemoveTagDebugByIndex),
            HideAddButton = true
        )]
        private List<TagDebug> TagsDebug
        {
            get
            {
                var result = new List<TagDebug>();
                if (this.tags == null)
                {
                    return result;
                }

                TagCatalog catalog = CatalogTools.GetTagCatalog();
                if (catalog == null)
                {
                    return result;
                }

                foreach (var tag in this.tags)
                {
                    result.Add(new TagDebug(catalog.FindTypeById(tag) + $" ({tag})", tag));
                }

                return result;
            }
            set { /** noting... **/ }
        }
        
        private struct TagDebug
        {
            [ShowInInspector, ReadOnly]
            public string name;
            
            internal readonly int id;

            public TagDebug(string name, int id)
            {
                this.name = name;
                this.id = id;
            }
        }
        
        private void OnRemoveTagDebugById(TagDebug tagDebug)
        {
            this.DelTag(tagDebug.id);
        }
        
        private void OnRemoveTagDebugByIndex(int index)
        {
            var tags = this.tags.ToArray();
            for (int i = 0, count = tags.Length; i < count; i++)
            {
                if (i == index)
                {
                    this.DelTag(tags[i]);
                }
            }
        }
#endif

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("References")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(OnRemoveReferenceByValue),
            CustomRemoveIndexFunction = nameof(OnRemoveReferenceByIndex),
            HideAddButton = true
        )]
        private List<ReferenceDebug> ReferencesDebug
        {
            get
            {
                var result = new List<ReferenceDebug>();
                if (this.references == null)
                {
                    return result;
                }

                var catalog = CatalogTools.GetReferenceCatalog();
                if (catalog == null)
                {
                    return result;
                }

                foreach (var (id, value) in this.references)
                {
                    string fullName = catalog.GetFullItemNameById(id);
                    result.Add(new ReferenceDebug(fullName, value, id));
                }

                return result;
            }

            set { /** noting... **/ }
        }

        private struct ReferenceDebug
        {
            [HorizontalGroup(200)]
            [ShowInInspector, ReadOnly]
            [HideLabel]
            public string name;

            [HorizontalGroup]
            [ShowInInspector]
            [HideLabel]
            public object value;
            
            internal readonly int id;

            public ReferenceDebug(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }
        }
        
        private void OnRemoveReferenceByValue(ReferenceDebug referenceDebug)
        {
            this.DelReference(referenceDebug.id);
        }
        
        private void OnRemoveReferenceByIndex(int index)
        {
            KeyValuePair<int,object>[] keyValuePairs = this.references.ToArray();
            for (int i = 0, count = keyValuePairs.Length; i < count; i++)
            {
                if (i == index)
                {
                    this.DelReference(keyValuePairs[i].Key);
                }
            }
        }
#endif

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(OnRemoveBehaviourByValue),
            CustomRemoveIndexFunction = nameof(OnRemoveBehaviourByIndex),
            HideAddButton = true
        )]
        private List<BehaviourDebug> BehavioursDebug
        {
            get
            {
                if (this.behaviours == null)
                {
                    return new List<BehaviourDebug>();
                }

                return this.behaviours.Select(it => new BehaviourDebug(it.GetType().Name)).ToList();
            }
            set { /** noting... **/ }
        }
        
        [InlineProperty]
        private struct BehaviourDebug
        {
            [ShowInInspector, ReadOnly]
            public string value;

            public BehaviourDebug(string value)
            {
                this.value = value;
            }
        }

        
        private void OnRemoveBehaviourByValue(BehaviourDebug behaviourDebug)
        {
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                IAtomicObject.IBehaviour behaviour = this.behaviours[i];
                if (behaviour.GetType().Name == behaviourDebug.value)
                {
                    this.DelBehaviour(behaviour);
                    return;
                }
            }
        }

    

        private void OnRemoveBehaviourByIndex(int index)
        {
            this.DelBehaviour(this.behaviours[index]);
        }
        
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button("Add Element")]
        [ShowInInspector, PropertyOrder(100), HideInEditorMode]
        private void OnAddElement(IAtomicObject.IComposable composable)
        {
            composable.Compose(this);
        }
#endif
#endif

        #endregion

        #region Tools

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake<T>() where T : IAtomicObject
        {
            ObjectBaker.Bake(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(Type type)
        {
            ObjectBaker.Bake(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(params Type[] types)
        {
            foreach (var type in types)
            {
                ObjectBaker.Bake(type);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                ObjectBaker.Bake(type);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Inflate(IAtomicObject entity)
        {
            ObjectInflater.Inflate(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InflateFrom(IAtomicObject entity, object from)
        {
            ObjectInflater.InflateFrom(entity, from);
        }

        #endregion
    }
}


// [Button("Add Tag")]
// [FoldoutGroup("Debug")]
// [ShowInInspector, PropertyOrder(100), HideInEditorMode]
// private void OnAddTag([TagId] int tag)
// {
//     this.AddTag(tag);
// }
//
// [Button("Add Reference")]
// [FoldoutGroup("Debug")]
// [ShowInInspector, PropertyOrder(100), HideInEditorMode]
// private void OnAddReference([ReferenceId] int id, object value)
// {
//     this.AddReference(id, value);
// }
//
// [Button("Add Behaviour")]
// [FoldoutGroup("Debug")]
// [ShowInInspector, PropertyOrder(100), HideInEditorMode]
// private void OnAddBehaviour(IAtomicObject.IBehaviour behaviour)
// {
//     this.AddBehaviour(behaviour);
// }
//
// [Button("Add Element")]
// [FoldoutGroup("Debug")]
// [ShowInInspector, PropertyOrder(100), HideInEditorMode]
// private void OnAddElement([ReferenceId] int id, IAtomicObject.IBehaviour value)
// {
//     this.AddReference(id, value);
//     this.AddBehaviour(value);
// }