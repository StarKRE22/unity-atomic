using System;
using System.Collections.Generic;
using System.Linq;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using UnityEngine.Profiling;

namespace Atomic.Objects
{
    public class AtomicEntity : MonoBehaviour, IMutableAtomicEntity, ISerializationCallbackReceiver
    {
#if ODIN_INSPECTOR
        [Title("Data"), PropertySpace]
        [ShowInInspector, HideInEditorMode, PropertyOrder(100)]
#endif
        private ISet<string> typeSet;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode, PropertyOrder(101)]
#endif
        private IDictionary<string, object> dataMap;

        public bool Is(string type)
        {
            return this.typeSet.Contains(type);
        }

        public bool Is(params string[] types)
        {
            for (int i = 0, count = types.Length; i < count; i++)
            {
                string type = types[i];

                if (!this.typeSet.Contains(type))
                {
                    return false;
                }
            }

            return true;
        }

        public T Get<T>(string key) where T : class
        {
            if (this.dataMap.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            if (this.dataMap.TryGetValue(key, out var value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public object Get(string key)
        {
            if (this.dataMap.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public bool TryGet(string key, out object result)
        {
            return this.dataMap.TryGetValue(key, out result);
        }

        public string[] AllTypes()
        {
            return this.typeSet.ToArray();
        }

        public KeyValuePair<string, object>[] AllData()
        {
            return this.dataMap.ToArray();
        }

        public bool AddData(string key, object value)
        {
            return this.dataMap.TryAdd(key, value);
        }

        public void SetData(string key, object value)
        {
            this.dataMap[key] = value;
        }

        public bool RemoveData(string key)
        {
            return this.dataMap.Remove(key);
        }

        public bool RemoveData(string key, out object value)
        {
            return this.dataMap.Remove(key, out value);
        }

        public void OverrideData(string key, object value, out object prevValue)
        {
            this.dataMap.TryGetValue(key, out prevValue);
            this.dataMap[key] = value;
        }

        public bool AddType(string type)
        {
            return this.typeSet.Add(type);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            this.typeSet.UnionWith(types);
        }

        public bool RemoveType(string type)
        {
            return this.typeSet.Remove(type);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            foreach (var type in types)
            {
                this.typeSet.Remove(type);
            }
        }

        public ISet<string> AllTypesUnsafe()
        {
            return this.typeSet;
        }

        public IDictionary<string, object> AllDataUnsafe()
        {
            return this.dataMap;
        }

        public int AllTypesNonAlloc(string[] results)
        {
            int i = 0;

            foreach (var type in this.typeSet)
            {
                results[i++] = type;
            }

            return i;
        }

        public int AllDataNonAlloc(KeyValuePair<string, object>[] results)
        {
            int i = 0;

            foreach (var property in this.dataMap)
            {
                results[i++] = property;
            }

            return i;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.OnAfterDeserialize();
            this.Compile();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.OnBeforeSerialize();
        }

        private void Compile()
        {
            this.typeSet = new HashSet<string>();
            this.dataMap = new Dictionary<string, object>();

            Type myType = this.GetType();

            //Use auto scan of child class:
            if (myType != null && myType != typeof(AtomicEntity) && myType != typeof(AtomicObject))
            {
#if UNITY_EDITOR
                Profiler.BeginSample("AtomicCompiler", this);
#endif
                AtomicEntityInfo entityInfo = AtomicCompiler.CompileEntity(myType);

                //Register types:
                this.typeSet.UnionWith(entityInfo.types);

                //Register values:
                foreach (ValueInfo valueInfo in entityInfo.values)
                {
                    string id = valueInfo.id;
                    object value = valueInfo.value(this);
                    this.dataMap.Add(id, value);
                }
                
#if UNITY_EDITOR
                Profiler.EndSample();
#endif
            }
        }
        
        protected virtual void OnAfterDeserialize()
        {
        }
        
        protected virtual void OnBeforeSerialize()
        {
        }
    }
}