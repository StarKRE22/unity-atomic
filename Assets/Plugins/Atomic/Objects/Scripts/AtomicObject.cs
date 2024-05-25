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
    public sealed class AtomicObject : MonoBehaviour, IAtomicObject
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

        #region Values

        private IValueCollection values;

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
            this.DelValue(id);
            this.AddValue(id, value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return this.values.TryAdd(id, value);
        }

        public bool DelValue(int id)
        {
            return this.values.Remove(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return this.values.Remove(id, out removed);
        }

        #endregion

        #region Logic

        private List<IAtomicLogic> logics;

        private List<IUpdate> updates;
        private List<IAtomicFixedUpdate> fixedUpdates;
        private List<ILateUpdate> lateUpdates;

        private List<IUpdate> _updateCache;
        private List<IAtomicFixedUpdate> _fixedUpdateCache;
        private List<ILateUpdate> _lateUpdateCache;

#if UNITY_EDITOR
        private List<IDrawGizmos> drawGizmoses;
#endif
        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
        }

        public bool AddLogic(IAtomicLogic logic)
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

            if (_enabled)
            {
                if (logic is IAtomicEnable enable)
                {
                    enable.Enable(this);
                }
            }

            if (logic is IUpdate update)
            {
                this.updates.Add(update);
            }

            if (logic is IAtomicFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (logic is ILateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }

#if UNITY_EDITOR
            if (logic is IDrawGizmos gizmos)
            {
                this.drawGizmoses.Add(gizmos);
            }
#endif
            return true;
        }

        public bool AddLogic<T>() where T : IAtomicLogic, new()
        {
            return this.AddLogic(new T());
        }

        public bool DelLogic(IAtomicLogic logic)
        {
            if (logic == null)
            {
                return false;
            }

            if (!this.logics.Remove(logic))
            {
                return false;
            }

            if (logic is IUpdate tickable)
            {
                this.updates.Remove(tickable);
            }

            if (logic is IAtomicFixedUpdate fixedTickable)
            {
                this.fixedUpdates.Remove(fixedTickable);
            }

            if (logic is ILateUpdate lateTickable)
            {
                this.lateUpdates.Remove(lateTickable);
            }

#if UNITY_EDITOR
            if (logic is IDrawGizmos gizmos)
            {
                this.drawGizmoses.Remove(gizmos);
            }
#endif
            if (_enabled)
            {
                if (logic is IAtomicDisable disable)
                {
                    disable.Disable(this);
                }
            }

            return true;
        }

        public bool DelLogic<T>() where T : IAtomicLogic
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

        [ContextMenu("OnEnable")]
        public void Enable()
        {
            _enabled = true;

            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                var behaviour = this.logics[i];
                if (behaviour is IAtomicEnable enable)
                {
                    enable.Enable(this);
                }
            }
        }

        [ContextMenu("OnDisable")]
        public void Disable()
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                var behaviour = this.logics[i];
                if (behaviour is IAtomicDisable disable)
                {
                    disable.Disable(this);
                }
            }

            _enabled = false;
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
                IUpdate update = _updateCache[i];
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
                IAtomicFixedUpdate atomicFixedUpdate = _fixedUpdateCache[i];
                atomicFixedUpdate.OnFixedUpdate(this, deltaTime);
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
                ILateUpdate lateUpdate = _lateUpdateCache[i];
                lateUpdate.OnLateUpdate(this, deltaTime);
            }
        }

        public void TriggerEnter(Collider collider)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                var element = this.logics[i];
                if (element is ITriggerEnter trigger)
                {
                    trigger.TriggerEnter(this, collider);
                }
            }
        }

        public void TriggerExit(Collider collider)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                var element = this.logics[i];
                if (element is ITriggerExit trigger)
                {
                    trigger.TriggerExit(this, collider);
                }
            }
        }

        public void TriggerEnter2D(Collider2D collider)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ITriggerEnter2D trigger)
                {
                    trigger.TriggerEnter2D(this, collider);
                }
            }
        }

        public void TriggerExit2D(Collider2D collider)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ITriggerExit2D trigger)
                {
                    trigger.TriggerExit2D(this, collider);
                }
            }
        }

        public void CollisionEnter(Collision collision)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ICollisionEnter handler)
                {
                    handler.CollisionEnter(this, collision);
                }
            }
        }

        public void CollisionExit(Collision collision)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ICollisionExit handler)
                {
                    handler.CollisionExit(this, collision);
                }
            }
        }

        public void CollisionEnter2D(Collision2D collision)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ICollisionEnter2D handler)
                {
                    handler.CollisionEnter2D(this, collision);
                }
            }
        }

        public void CollisionExit2D(Collision2D collision)
        {
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                if (this.logics[i] is ICollisionExit2D handler)
                {
                    handler.CollisionExit2D(this, collision);
                }
            }
        }


#if UNITY_EDITOR
        public void OnGizmosDraw()
        {
            foreach (IDrawGizmos gizmos in this.drawGizmoses)
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
        [ShowIf(nameof(hasLogic))]
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
        private List<MonoSection> monoSections;

        [PropertyOrder(90)]
        [HideInPlayMode]
        [SerializeField]
        private List<ScriptableSection> scriptableSections;

        [PropertyOrder(90)]
        [HideInPlayMode]
        [SerializeField]
        private List<MonoBehaviour> customSections; 

        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private bool hasLogic = true;

        [FoldoutGroup("Optimization")]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private bool inflateSections = true;

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
        private ValueStorageType valueStorageType;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(valueStorageType), ValueStorageType.DICTIONARY)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int valueCapacity;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(valueStorageType), ValueStorageType.ARRAY)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int valueArraySize;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(valueStorageType), ValueStorageType.SEGMENT)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int valueSegmentStart;

        [FoldoutGroup("Optimization")]
        [EnableIf(nameof(valueStorageType), ValueStorageType.SEGMENT)]
        [PropertyOrder(95)]
        [HideInPlayMode]
        [SerializeField]
        private int valueSegmentEnd;

        [Serializable]
        public enum TagsStorageType
        {
            HASHSET = 0,
            ARRAY = 1,
            SEGMENT = 2
        }

        [Serializable]
        public enum ValueStorageType
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

            this.values = this.valueStorageType switch
            {
                ValueStorageType.DICTIONARY => new ValueDictionary(this.valueCapacity),
                ValueStorageType.ARRAY => new ValueArray(this.valueArraySize),
                ValueStorageType.SEGMENT => new ValueSegment(this.valueSegmentStart, this.valueSegmentEnd),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (this.hasLogic)
            {
                this.logics = new List<IAtomicLogic>();
                this.updates = new List<IUpdate>();
                this.fixedUpdates = new List<IAtomicFixedUpdate>();
                this.lateUpdates = new List<ILateUpdate>();

                _updateCache = new List<IUpdate>();
                _fixedUpdateCache = new List<IAtomicFixedUpdate>();
                _lateUpdateCache = new List<ILateUpdate>();

#if UNITY_EDITOR
                this.drawGizmoses = new List<IDrawGizmos>();
#endif
            }

            if (this.monoSections is {Count: > 0})
            {
                for (int i = 0, count = this.monoSections.Count; i < count; i++)
                {
                    MonoSection source = this.monoSections[i];
                    if (source != null)
                    {
                        source.Compose(this);
                    }
                }
            }

            if (this.scriptableSections is {Count: > 0})
            {
                for (int i = 0, count = this.scriptableSections.Count; i < count; i++)
                {
                    ScriptableSection source = this.scriptableSections[i];
                    if (source != null)
                    {
                        source.Compose(this);
                    }
                }
            }
            
            if (this.customSections is {Count: > 0})
            {
                for (int i = 0, count = this.customSections.Count; i < count; i++)
                {
                    MonoBehaviour source = this.customSections[i];
                    if (source == null)
                    {
                        continue;
                    }

                    if (source is IAtomicAspect composable)
                    {
                        composable.Compose(this);
                    }

                    if (this.inflateSections)
                    {
                        ObjectInflater.InflateFrom(this, source);
                    }

                    if (source is IAtomicLogic behaviour)
                    {
                        this.AddLogic(behaviour);
                    }
                }
            }
        }

        [ContextMenu("Dispose")]
        public void Dispose()
        {
            if (this.monoSections is {Count: > 0})
            {
                for (int i = 0, count = this.monoSections.Count; i < count; i++)
                {
                    MonoSection source = this.monoSections[i];
                    if (source != null)
                    {
                        source.Dispose(this);
                    }
                }
            }
            
            if (this.scriptableSections is {Count: > 0})
            {
                for (int i = 0, count = this.scriptableSections.Count; i < count; i++)
                {
                    ScriptableSection source = this.scriptableSections[i];
                    if (source != null)
                    {
                        source.Dispose(this);
                    }
                }
            }
            
            if (this.customSections is {Count: > 0})
            {
                for (int i = 0, count = this.customSections.Count; i < count; i++)
                {
                    MonoBehaviour source = this.customSections[i];
                    if (source is IAtomicLogic behaviour)
                    {
                        this.DelLogic(behaviour);
                    }

                    if (source is IAtomicAspect disposable)
                    {
                        disposable.Dispose(this);
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

            if (!this.unityControl)
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
                this.CollisionExit(other);
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
            if (!this.hasLogic)
            {
                this.unityControl = false;
            }
        }

        private void Reset()
        {
            this.hasLogic = true;
            this.unityControl = true;
            this.composeOnAwake = true;
            this.disposeOnDestroy = true;

            this.monoSections = new List<MonoSection>(this.GetComponentsInChildren<MonoSection>());
            this.scriptableSections = new List<ScriptableSection>();
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
            var prevRefType = this.valueStorageType;

            this.tagStorageType = TagsStorageType.HASHSET;
            this.valueStorageType = ValueStorageType.DICTIONARY;
            
            //Make compose for compute capacity!
            this.Compose();

            HashSet<int> tags = (HashSet<int>) this.tags;
            this.tagCapacity = tags.Count;
            this.tagSegmentStart = tags.Min(it => it);
            this.tagSegmentEnd = tags.Max(it => it);
            this.tagArraySize = this.tagSegmentEnd + 1;

            Dictionary<int, object> value = (Dictionary<int, object>) this.values;
            this.valueCapacity = value.Count;
            this.valueSegmentStart = value.Min(it => it.Key);
            this.valueSegmentEnd = value.Max(it => it.Key);
            this.valueArraySize = this.valueSegmentEnd + 1;

            this.tagStorageType = prevTagType;
            this.valueStorageType = prevRefType;
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
                if (this.values == null)
                {
                    return result;
                }

                var catalog = CatalogTools.GetReferenceCatalog();
                if (catalog == null)
                {
                    return result;
                }

                foreach (var (id, value) in this.values)
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
        
        private void OnRemoveReferenceByValue(ReferenceDebug valueDebug)
        {
            this.DelValue(valueDebug.id);
        }
        
        private void OnRemoveReferenceByIndex(int index)
        {
            KeyValuePair<int,object>[] keyValuePairs = this.values.ToArray();
            for (int i = 0, count = keyValuePairs.Length; i < count; i++)
            {
                if (i == index)
                {
                    this.DelValue(keyValuePairs[i].Key);
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
                if (this.logics == null)
                {
                    return new List<BehaviourDebug>();
                }

                return this.logics.Select(it => new BehaviourDebug(it.GetType().Name)).ToList();
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
            for (int i = 0, count = this.logics.Count; i < count; i++)
            {
                IAtomicLogic logic = this.logics[i];
                if (logic.GetType().Name == behaviourDebug.value)
                {
                    this.DelLogic(logic);
                    return;
                }
            }
        }

    

        private void OnRemoveBehaviourByIndex(int index)
        {
            this.DelLogic(this.logics[index]);
        }
        
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button("Add Element")]
        [ShowInInspector, PropertyOrder(100), HideInEditorMode]
        private void OnAddElement(IAtomicAspect aspect)
        {
            aspect.Compose(this);
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