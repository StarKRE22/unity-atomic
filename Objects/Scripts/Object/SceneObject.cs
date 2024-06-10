using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Objects/Scene Object")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public sealed class SceneObject : MonoBehaviour, IObject
    {
        private IObject _object;

        #region Main

        public event Action<IObject, string> OnNameChanged
        {
            add => _object.OnNameChanged += value;
            remove => _object.OnNameChanged -= value;
        }

        public event Action<IObject> OnEnabled
        {
            add => _object.OnEnabled += value;
            remove => _object.OnEnabled -= value;
        }

        public event Action<IObject> OnDisabled
        {
            add => _object.OnDisabled += value;
            remove => _object.OnDisabled -= value;
        }

        public int Id => _object?.Id ?? -1;
        
        public bool Alive => _object?.Alive ?? false;

        bool IObject.Constructed
        {
            get { return _object?.Constructed ?? false; }
            set { _object.Constructed = value; }
        }

        public bool Enabled
        {
            get { return _object?.Enabled ?? false; }
            set { _object.Enabled = value; }
        }
        
        public string Name
        {
            get { return _object?.Name ?? "undefined"; }
            set { _object.Name = value; }
        }

#if ODIN_INSPECTOR
        [DisableInPlayMode]
        [PropertyOrder(-100)]
        [PropertySpace(SpaceAfter = 2)]
#endif
        [Tooltip("Need to turn on/off the real game object when OnEnable/OnDisable happens?")]
        [SerializeField]
        private bool controlState = true;

#if ODIN_INSPECTOR
        [PropertyOrder(-100)]
        [DisableInPlayMode]
        [PropertySpace(SpaceAfter = 8)]
#endif
        [Tooltip("The list of ordered installers who will compose a real object")]
        [SerializeField]
        private List<SceneObjectInstallerBase> installPipeline;

        public void Install(IObject obj)
        {
            _object = obj;

            if (this.installPipeline is {Count: > 0})
            {
                for (int i = 0, count = this.installPipeline.Count; i < count; i++)
                {
                    var installer = this.installPipeline[i];
                    if (installer != null)
                    {
                        installer.Install(_object);
                    }
                }
            }
        }

        #endregion

        #region Tags

        public event Action<IObject, int> OnTagAdded
        {
            add => _object.OnTagAdded += value;
            remove => _object.OnTagAdded -= value;
        }

        public event Action<IObject, int> OnTagDeleted
        {
            add => _object.OnTagDeleted += value;
            remove => _object.OnTagDeleted -= value;
        }

        public bool DelTag(int tag)
        {
            return _object.DelTag(tag);
        }

        void IObject.Dispose()
        {
            _object.Dispose();
        }

        public IReadOnlyCollection<int> Tags
        {
            get { return _object.Tags; }
        }

        public bool HasTag(int tag)
        {
            return _object.HasTag(tag);
        }

        public bool AddTag(int tag)
        {
            return _object.AddTag(tag);
        }

        #endregion

        #region Values

        public event Action<IObject, int, object> OnValueAdded
        {
            add => _object.OnValueAdded += value;
            remove => _object.OnValueAdded -= value;
        }

        public event Action<IObject, int, object> OnValueDeleted
        {
            add => _object.OnValueDeleted += value;
            remove => _object.OnValueDeleted -= value;
        }

        public event Action<IObject, int, object> OnValueChanged
        {
            add => _object.OnValueChanged += value;
            remove => _object.OnValueChanged -= value;
        }

        public IReadOnlyDictionary<int, object> Values
        {
            get { return _object.Values; }
        }

        public T GetValue<T>(int id) where T : class
        {
            return _object.GetValue<T>(id);
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            return _object.TryGetValue(id, out value);
        }

        public object GetValue(int id)
        {
            return _object.GetValue(id);
        }

        public void SetValue(int id, object value)
        {
            _object.SetValue(id, value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return _object.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return _object.AddValue(id, value);
        }

        public bool DelValue(int id)
        {
            return _object.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return _object.DelValue(id, out removed);
        }

        public bool HasValue(int id)
        {
            return _object.HasValue(id);
        }

        #endregion

        #region Logic

        public event Action<IObject, ILogic> OnLogicAdded
        {
            add => _object.OnLogicAdded += value;
            remove => _object.OnLogicAdded -= value;
        }

        public event Action<IObject, ILogic> OnLogicDeleted
        {
            add => _object.OnLogicDeleted += value;
            remove => _object.OnLogicDeleted -= value;
        }

        public IReadOnlyList<ILogic> Logics
        {
            get { return _object.Logics; }
        }

        public T GetLogic<T>() where T : ILogic
        {
            return _object.GetLogic<T>();
        }

        public bool TryGetLogic<T>(out T logic) where T : ILogic
        {
            return _object.TryGetLogic(out logic);
        }

        public bool AddLogic(ILogic logic)
        {
            return _object.AddLogic(logic);
        }

        public bool AddLogic<T>() where T : ILogic, new()
        {
            return _object.AddLogic<T>();
        }

        public bool DelLogic(ILogic logic)
        {
            return _object.DelLogic(logic);
        }

        public bool DelLogic<T>() where T : ILogic
        {
            return _object.DelLogic<T>();
        }

        public bool HasLogic(ILogic logic)
        {
            return _object.HasLogic(logic);
        }

        public bool HasLogic<T>() where T : ILogic
        {
            return _object.HasLogic<T>();
        }

        #endregion

        #region Unity

        private void OnEnable()
        {
            if (this.controlState && _object != null)
            {
                _object.Enabled = true;
            }
        }

        private void OnDisable()
        {
            if (this.controlState && _object != null)
            {
                _object.Enabled = false;
            }
        }

        #endregion

        #region Debug

        public static ITagNameFormatter TagNameFormatter;
        public static IValueNameFormatter ValueNameFormatter;

        public interface ITagNameFormatter
        {
            string GetName(int id);
        }

        public interface IValueNameFormatter
        {
            string GetName(int id);
        }

#if UNITY_EDITOR && ODIN_INSPECTOR
        
        ///Main
        
        [FoldoutGroup("Debug")]
        [PropertySpace(4)]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Id")]
        private int IdDebug
        {
            get { return _object?.Id ?? -1; }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Name")]
        private string NameDebug
        {
            get { return _object?.Name ?? "undefined"; }
            set
            {
                if (_object != null) _object.Name = value;
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Alive")]
        private bool AliveDebug
        {
            get { return _object?.Alive ?? false; }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Constructed")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode]
        private bool ConstructedDebug
        {
            get { return _object?.Constructed ?? false; }
            set
            {
                if (_object != null) _object.Constructed = value;
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Enabled")]
        [HideInEditorMode]
        private bool EnabledDebug
        {
            get { return _object?.Enabled ?? false; }
            set
            {
                if (_object != null) _object.Enabled = value;
            }
        }

        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Test Install"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
        private void TestInstall()
        {
            this.Install(Object.Dummy);
        }

        ///Tags
        private static readonly List<TagElement> _tagElememtsCache = new();

        private struct TagElement
        {
            [ShowInInspector, ReadOnly]
            internal string name;
            internal readonly int id;

            public TagElement(string name, int id)
            {
                this.name = name;
                this.id = id;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Tags")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveTagElement),
            CustomRemoveIndexFunction = nameof(RemoveTagElementAt),
            HideAddButton = true
        )]
        private List<TagElement> TagElememts
        {
            get
            {
                _tagElememtsCache.Clear();

                IReadOnlyCollection<int> tags = _object?.Tags;
                if (tags == null)
                {
                    return _tagElememtsCache;
                }

                foreach (int tag in tags)
                {
                    string name = TagNameFormatter?.GetName(tag) ?? tag.ToString();
                    _tagElememtsCache.Add(new TagElement(name, tag));
                }

                return _tagElememtsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveTagElement(TagElement tagElement)
        {
            if (_object != null) this.DelTag(tagElement.id);
        }

        private void RemoveTagElementAt(int index)
        {
            if (_object != null) this.DelTag(TagElememts[index].id);
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
        private List<ValueElement> ValueElements
        {
            get
            {
                _valueElementsCache.Clear();

                IReadOnlyDictionary<int, object> values = _object?.Values;
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
            if (_object != null) this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            if (_object != null) this.DelValue(ValueElements[index].id);
        }


        ///Logics
        private static readonly List<LogicElement> _logicElementsCache = new();

        [InlineProperty]
        private struct LogicElement
        {
            [ShowInInspector, ReadOnly]
            public string name;
            internal readonly ILogic value;

            public LogicElement(string name, ILogic value)
            {
                this.name = name;
                this.value = value;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Logics")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveLogicElement),
            CustomRemoveIndexFunction = nameof(RemoveLogicElementAt),
            HideAddButton = true
        )]
        private List<LogicElement> LogicElements
        {
            get
            {
                _logicElementsCache.Clear();

                IReadOnlyList<ILogic> logics = _object?.Logics;
                if (logics == null)
                {
                    return _logicElementsCache;
                }

                for (int i = 0, count = logics.Count; i < count; i++)
                {
                    ILogic logic = logics[i];
                    string name = logic.GetType().Name;
                    _logicElementsCache.Add(new LogicElement(name, logic));
                }

                return _logicElementsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveLogicElement(LogicElement logicElement)
        {
            if (_object != null) this.DelLogic(logicElement.value);
        }

        private void RemoveLogicElementAt(int index)
        {
            if (_object != null) this.DelLogic(Logics[index]);
        }

        ///Add Element 
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button("Add Element")]
        [ShowInInspector, PropertyOrder(100), HideInEditorMode]
        private void OnAddElement(IObjectInstaller installer)
        {
            installer.Install(this);
        }
#endif
        #endregion
    }
}