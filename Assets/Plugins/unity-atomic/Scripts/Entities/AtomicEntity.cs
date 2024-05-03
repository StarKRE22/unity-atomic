using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Entity")]
    [DisallowMultipleComponent]
    public abstract class AtomicEntity : MonoBehaviour, IAtomicEntity, ISerializationCallbackReceiver
    {
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Types (Keys)")]
        [ShowInInspector, ReadOnly, PropertyOrder(100), HideIf(nameof(keyMode), KeyMode.NONE)]
#endif
        internal ISetProvider<string> typeKeys;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Types (Indexes)")]
        [ShowInInspector, ReadOnly, PropertyOrder(100), HideIf(nameof(indexMode), IndexMode.NONE)]
#endif
        internal ISetProvider<int> typeIndexes;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Fields (Keys)")]
        [ShowInInspector, ReadOnly, PropertyOrder(100), HideIf(nameof(keyMode), KeyMode.NONE)]
#endif
        internal IMapProvider<string, object> fieldsKeys;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Fields (Indexes)")]
        [ShowInInspector, ReadOnly, PropertyOrder(100), HideIf(nameof(indexMode), IndexMode.NONE)]
#endif
        internal IMapProvider<int, object> fieldsIndexes;

        public bool Is(string type)
        {
            return this.typeKeys.Contains(type);
        }

        public bool Is(int index)
        {
            return this.typeIndexes.Contains(index);
        }

        public T Get<T>(string key) where T : class
        {
            return this.fieldsKeys[key] as T;
        }

        public T Get<T>(int index) where T : class
        {
            return this.fieldsIndexes[index] as T;
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            if (this.fieldsKeys.TryGetValue(key, out object value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public bool TryGet<T>(int index, out T result) where T : class
        {
            if (this.fieldsIndexes.TryGetValue(index, out object value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public object Get(string key)
        {
            return this.fieldsKeys[key];
        }

        public object Get(int index)
        {
            return this.fieldsIndexes[index];
        }

        public bool TryGet(string key, out object result)
        {
            return this.fieldsKeys.TryGetValue(key, out result);
        }

        public bool TryGet(int index, out object result)
        {
            return this.fieldsIndexes.TryGetValue(index, out result);
        }

        public void Fields(out List<KeyValuePair<string, object>> results)
        {
            results = this.fieldsKeys.GetPairs();
        }

        public void Types(out List<string> results)
        {
            results = this.typeKeys.GetValues();
        }

        public int TypesNonAlloc(string[] results)
        {
            return this.typeKeys.GetValuesNonAlloc(results);
        }

        public int TypesNonAlloc(int[] results)
        {
            return this.typeIndexes.GetValuesNonAlloc(results);
        }

        public void Types(out List<int> results)
        {
            results = this.typeIndexes.GetValues();
        }

        public int FieldsNonAlloc(KeyValuePair<string, object>[] results)
        {
            return this.fieldsKeys.GetPairsNonAlloc(results);
        }

        public void Fields(out List<KeyValuePair<int, object>> results)
        {
            results = this.fieldsIndexes.GetPairs();
        }

        public int FieldsNonAlloc(KeyValuePair<int, object>[] results)
        {
            return this.fieldsIndexes.GetPairsNonAlloc(results);
        }

        public bool AddField(string key, object value)
        {
            return this.fieldsKeys.Add(key, value);
        }

        public bool AddField(int index, object value)
        {
            return this.fieldsIndexes.Add(index, value);
        }

        public void SetField(string key, object value)
        {
            this.fieldsKeys[key] = value;
        }

        public void SetField(int index, object value)
        {
            this.fieldsIndexes[index] = value;
        }

        public bool RemoveField(string key)
        {
            return this.fieldsKeys.Remove(key);
        }

        public bool RemoveField(int index)
        {
            return this.fieldsIndexes.Remove(index);
        }

        public bool RemoveField(string key, out object value)
        {
            return this.fieldsKeys.Remove(key, out value);
        }

        public bool RemoveField(int index, out object value)
        {
            return this.fieldsIndexes.Remove(index, out value);
        }

        public void OverrideField(string key, object value, out object prevValue)
        {
            this.fieldsKeys.TryGetValue(key, out prevValue);
            this.fieldsKeys[key] = value;
        }

        public void OverrideField(int index, object value, out object prevValue)
        {
            prevValue = this.fieldsIndexes[index];
            this.fieldsIndexes[index] = value;
        }

        public bool AddType(string type)
        {
            return this.typeKeys.Add(type);
        }

        public bool AddType(int index)
        {
            return this.typeIndexes.Add(index);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            this.typeKeys.UnionWith(types);
        }

        public void AddTypes(IEnumerable<int> indexes)
        {
            this.typeIndexes.UnionWith(indexes);
        }

        public bool RemoveType(string type)
        {
            return this.typeKeys.Remove(type);
        }

        public bool RemoveType(int index)
        {
            return this.typeIndexes.Remove(index);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            foreach (var type in types)
            {
                this.typeKeys.Remove(type);
            }
        }

        public void RemoveTypes(IEnumerable<int> indexes)
        {
            foreach (int index in indexes)
            {
                this.typeIndexes.Remove(index);
            }
        }
        
        protected virtual void OnAfterDeserialize()
        {
        }

        protected virtual void OnBeforeSerialize()
        {
        }

        #region Internal
        
        internal enum KeyMode
        {
            NONE = 0,
            DEFAULT = 1
        }

        internal enum IndexMode
        {
            NONE = 0,
            DEFAULT = 1,
            SEGMENT = 2,
            ARRAY = 3
        }
        
        [FoldoutGroup("Installers")]
        [PropertyOrder(80)]
        [SerializeReference]
        private IInstaller[] entityInstallers = default;
        
        [FoldoutGroup("Optimization")]
        [PropertyOrder(90)]
        [SerializeField]
        private Object[] inflateSources;

        [FoldoutGroup("Optimization")]
        [PropertyOrder(90)]
        [LabelText("Key Storage")]
        [SerializeField, Space]
        internal KeyMode keyMode = KeyMode.DEFAULT;

        [FoldoutGroup("Optimization")]
        [PropertyOrder(90)]
        [LabelText("Index Storage")]
        [SerializeField]
        internal IndexMode indexMode = IndexMode.NONE;
        
        [FoldoutGroup("Optimization")]
        [PropertyOrder(90)]
        [ShowIf(nameof(indexMode), IndexMode.ARRAY)]
        [SerializeField]
        private int indexTypeSize = 4;
        
        [FoldoutGroup("Optimization")]
        [PropertyOrder(90)]
        [ShowIf(nameof(indexMode), IndexMode.ARRAY)]
        [SerializeField]
        private int indexValueSize = 16;

        [ContextMenu("Compose")]
        private protected virtual void Compose()
        {
            switch (this.keyMode)
            {
                case KeyMode.NONE:
                    this.typeKeys = null;
                    this.fieldsKeys = null;
                    break;
                case KeyMode.DEFAULT:
                    this.typeKeys = new HashSetProvider<string>();
                    this.fieldsKeys = new DictionaryProvider<string, object>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (this.indexMode)
            {
                case IndexMode.NONE:
                    this.typeIndexes = null;
                    this.fieldsIndexes = null;
                    break;
                case IndexMode.DEFAULT:
                    this.typeIndexes = new HashSetProvider<int>();
                    this.fieldsIndexes = new DictionaryProvider<int, object>();
                    break;
                case IndexMode.SEGMENT:
                    this.typeIndexes = new BoolSegmentProvider();
                    this.fieldsIndexes = new ObjectSegmentProvider();
                    break;
                case IndexMode.ARRAY:
                    this.typeIndexes = new BoolArrayProvider(this.indexTypeSize);
                    this.fieldsIndexes = new ObjectArrayProvider(this.indexValueSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (this.inflateSources is {Length: > 0})
            {
                for (int i = 0, count = this.inflateSources.Length; i < count; i++)
                {
                    Object source = this.inflateSources[i];
                    if (source != null)
                    {
                        EntityInflater.InflateFrom(this, source);
                    }
                }
            }

            if (this.entityInstallers is {Length: > 0})
            {
                for (int i = 0, count = this.entityInstallers.Length; i < count; i++)
                {
                    IInstaller installer = this.entityInstallers[i];
                    if (installer != null)
                    {
                        installer.Install(this);
                    }
                }
            }
        }

        public interface IInstaller
        {
            void Install(AtomicEntity entity);
        }
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.OnAfterDeserialize();
            this.Compose();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.OnBeforeSerialize();
        }

        protected virtual void Reset()
        {
            this.inflateSources = new Object[]{this};
        }

        protected virtual void OnValidate()
        {
            this.Compose();
        }
        
        #endregion
    }
}