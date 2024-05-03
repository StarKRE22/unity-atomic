using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace AIModule
{
    //TODO: ПЕРЕПИСАТЬ НА INT32!!! ПОТОМУ ЧТО HASH — INT32
    [AddComponentMenu("AI/Blackboard")]
    public class Blackboard : MonoBehaviour, IBlackboard, ISerializationCallbackReceiver
    {
        [SerializeReference, HideInPlayMode]
        private IBlackboardValue[] initialVariables = Array.Empty<IBlackboardValue>();

        private Dictionary<ushort, bool> boolValues = new();
        private Dictionary<ushort, int> intValues = new();
        private Dictionary<ushort, float> floatValues = new();
        private Dictionary<ushort, object> objValues = new();
        private Dictionary<ushort, Vector2> vector2Values = new();
        private Dictionary<ushort, Vector3> vector3Values = new();
        private Dictionary<ushort, Quaternion> quaternionValues = new();

        private HashSet<ushort> keys;

        public bool HasKey(ushort key) => this.keys.Contains(key);

        public bool GetBool(ushort key) => this.boolValues[key];

        public int GetInt(ushort key) => this.intValues[key];

        public float GetFloat(ushort key) => this.floatValues[key];

        public T GetObject<T>(ushort key) where T : class => this.objValues[key] as T;

        public Vector2 GetVector2(ushort key) => this.vector2Values[key];

        public Vector3 GetVector3(ushort key) => this.vector3Values[key];

        public Quaternion GetQuaternion(ushort key) => this.quaternionValues[key];

        public bool TryGetBool(ushort key, out bool value) => this.boolValues.TryGetValue(key, out value);

        public bool TryGetInt(ushort key, out int value) => this.intValues.TryGetValue(key, out value);

        public bool TryGetFloat(ushort key, out float value) => this.floatValues.TryGetValue(key, out value);
        
        public bool TryGetObject<T>(ushort key, out T value)
        {
            if (this.objValues.TryGetValue(key, out object result))
            {
                value = (T) result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetVector2(ushort key, out Vector2 value) =>
            this.vector2Values.TryGetValue(key, out value);

        public bool TryGetVector3(ushort key, out Vector3 value) =>
            this.vector3Values.TryGetValue(key, out value);

        public bool TryGetQuaternion(ushort key, out Quaternion value) =>
            this.quaternionValues.TryGetValue(key, out value);

        [Title("Debug")]
        [Button, HideInEditorMode]
        public void SetBool([BlackboardKey] ushort key, bool value)
        {
            this.boolValues[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetInt([BlackboardKey] ushort key, int value)
        {
            this.intValues[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetFloat([BlackboardKey] ushort key, float value)
        {
            this.floatValues[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetObject([BlackboardKey] ushort key, object value)
        {
            this.objValues[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetVector2([BlackboardKey] ushort key, Vector2 value)
        {
            this.vector2Values[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetVector3([BlackboardKey] ushort key, Vector3 value)
        {
            this.vector3Values[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void SetQuaternion([BlackboardKey] ushort key, Quaternion value)
        {
            this.quaternionValues[key] = value;
            this.keys.Add(key);
        }

        [Button, HideInEditorMode]
        public void DeleteBool([BlackboardKey] ushort key)
        {
            this.boolValues.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteInt([BlackboardKey] ushort key)
        {
            this.intValues.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteFloat([BlackboardKey] ushort key)
        {
            this.floatValues.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteObject([BlackboardKey] ushort key)
        {
            this.objValues.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteVector2([BlackboardKey] ushort key)
        {
            this.vector2Values.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteVector3([BlackboardKey] ushort key)
        {
            this.vector3Values.Remove(key);
            this.keys.Remove(key);
        }

        [Button, HideInEditorMode]
        public void DeleteQuaternion([BlackboardKey] ushort key)
        {
            this.vector3Values.Remove(key);
            this.keys.Remove(key);
        }

        public IReadOnlyDictionary<ushort, bool> BoolValues() => this.boolValues;

        public IReadOnlyDictionary<ushort, int> IntValues() => this.intValues;

        public IReadOnlyDictionary<ushort, float> FloatValues() => this.floatValues;

        public IReadOnlyDictionary<ushort, object> ObjectValues() => this.objValues;

        public IReadOnlyDictionary<ushort, Vector2> Vector2Values() => this.vector2Values;

        public IReadOnlyDictionary<ushort, Vector3> Vector3Values() => this.vector3Values;
        
        public IReadOnlyDictionary<ushort, Quaternion> QuaternionValues() => this.quaternionValues;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            this.Initialize();
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!EditorApplication.isPlaying)
            {
                this.Initialize();
            }
        }
#endif

        private void Initialize()
        {
            this.keys = new HashSet<ushort>();
            this.boolValues = new Dictionary<ushort, bool>();
            this.intValues = new Dictionary<ushort, int>();
            this.floatValues = new Dictionary<ushort, float>();
            this.objValues = new Dictionary<ushort, object>();
            this.vector2Values = new Dictionary<ushort, Vector2>();
            this.vector3Values = new Dictionary<ushort, Vector3>();
            this.quaternionValues = new Dictionary<ushort, Quaternion>();
            
            foreach (IBlackboardValue value in this.initialVariables)
            {
                value.Apply(this);
            }
        }
    }
}