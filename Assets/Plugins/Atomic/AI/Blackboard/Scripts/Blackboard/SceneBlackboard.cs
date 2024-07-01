using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Modules.AI
{
    [AddComponentMenu("Modules/AI/AI Blackboard")]
    [DisallowMultipleComponent]
    public sealed class SceneBlackboard : MonoBehaviour, IBlackboard, ISerializationCallbackReceiver
    {
        [SerializeReference, HideInPlayMode]
        private IBlackboardInstaller[] installers = Array.Empty<IBlackboardInstaller>();

        private Blackboard blackboard;

        public IReadOnlyCollection<int> TagValues => blackboard.TagValues;
        public IReadOnlyDictionary<int, bool> BoolValues => this.blackboard.BoolValues;
        public IReadOnlyDictionary<int, int> IntValues => this.blackboard.IntValues;
        public IReadOnlyDictionary<int, float> FloatValues => this.blackboard.FloatValues;
        public IReadOnlyDictionary<int, float2> Float2Values => this.blackboard.Float2Values;
        public IReadOnlyDictionary<int, float3> Float3Values => this.blackboard.Float3Values;
        public IReadOnlyDictionary<int, quaternion> QuaternionValues => this.blackboard.QuaternionValues;
        public IReadOnlyDictionary<int, object> ObjectValues => this.blackboard.ObjectValues;
        public IReadOnlyDictionary<int, IBlackboard.IRef> StructValues => this.blackboard.StructValues;

        public bool HasTag(int key) => blackboard.HasTag(key);
        public bool HasBool(int key) => this.blackboard.HasBool(key);
        public bool HasInt(int key) => this.blackboard.HasInt(key);
        public bool HasFloat(int key) => this.blackboard.HasFloat(key);
        public bool HasFloat2(int key) => this.blackboard.HasFloat2(key);
        public bool HasFloat3(int key) => this.blackboard.HasFloat3(key);
        public bool HasQuaternion(int key) => this.blackboard.HasQuaternion(key);
        public bool HasObject(int key) => this.blackboard.HasObject(key);
        public bool HasStruct(int key) => this.blackboard.HasStruct(key);

        public bool GetBool(int key) => this.blackboard.GetBool(key);
        public int GetInt(int key) => this.blackboard.GetInt(key);
        public float GetFloat(int key) => this.blackboard.GetFloat(key);
        public float2 GetFloat2(int key) => this.blackboard.GetFloat2(key);
        public float3 GetFloat3(int key) => this.blackboard.GetFloat3(key);
        public quaternion GetQuaternion(int key) => this.blackboard.GetQuaternion(key);
        public object GetObject(int key) => this.blackboard.GetObject(key);
        public T GetObject<T>(int key) where T : class => this.blackboard.GetObject<T>(key);
        public ref T GetStruct<T>(int key) where T : struct => ref this.blackboard.GetStruct<T>(key);

        public bool TryGetBool(int key, out bool value) =>
            this.blackboard.TryGetBool(key, out value);

        public bool TryGetInt(int key, out int value) =>
            this.blackboard.TryGetInt(key, out value);

        public bool TryGetFloat(int key, out float value) =>
            this.blackboard.TryGetFloat(key, out value);

        public bool TryGetFloat2(int key, out float2 value) =>
            this.blackboard.TryGetFloat2(key, out value);

        public bool TryGetFloat3(int key, out float3 value) =>
            this.blackboard.TryGetFloat3(key, out value);

        public bool TryGetQuaternion(int key, out quaternion value) =>
            this.blackboard.TryGetQuaternion(key, out value);

        public bool TryGetObject(int key, out object value) =>
            this.blackboard.TryGetObject(key, out value);

        public bool TryGetObject<T>(int key, out T value) where T : class =>
            this.blackboard.TryGetObject(key, out value);
        //
        // public bool TryGetStruct<T>(int key, out T value) where T : struct =>
        //     this.blackboard.TryGetStruct(key, out value);

        public bool TryGetStruct<T>(int key, out IBlackboard.Ref<T> value) where T : struct 
            => this.blackboard.TryGetStruct(key, out value);

        public void SetTag(int key) => this.blackboard.SetTag(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetStruct<T>([BlackboardKey] int key, T value) where T : struct =>
            this.blackboard.SetStruct(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetBool([BlackboardKey] int key, bool value) =>
            this.blackboard.SetBool(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetInt([BlackboardKey] int key, int value) =>
            this.blackboard.SetInt(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetFloat([BlackboardKey] int key, float value) =>
            this.blackboard.SetFloat(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetObject([BlackboardKey] int key, object value) =>
            this.blackboard.SetObject(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetFloat2([BlackboardKey] int key, float2 value) =>
            this.blackboard.SetFloat2(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetFloat3([BlackboardKey] int key, float3 value) =>
            this.blackboard.SetFloat3(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetVector3([BlackboardKey] int key, float3 value) =>
            this.blackboard.SetFloat3(key, value);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SetQuaternion([BlackboardKey] int key, quaternion value) =>
            this.blackboard.SetQuaternion(key, value);
        
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelTag([BlackboardKey] int key) => 
            blackboard.DelTag(key);

        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelBool([BlackboardKey] int key) =>
            this.blackboard.DelBool(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelInt([BlackboardKey] int key) =>
            this.blackboard.DelInt(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelFloat([BlackboardKey] int key) =>
            this.blackboard.DelFloat(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelObject([BlackboardKey] int key) =>
            this.blackboard.DelObject(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelStruct([BlackboardKey] int key) =>
            this.blackboard.DelStruct(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelFloat2([BlackboardKey] int key) =>
            this.blackboard.DelFloat2(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelFloat3([BlackboardKey] int key) =>
            this.blackboard.DelFloat3(key);

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelQuaternion([BlackboardKey] int key) =>
            this.blackboard.DelQuaternion(key);
        
        #region Installing

        private void Install()
        {
            this.blackboard = new Blackboard();

            if (this.installers != null)
            {
                for (int i = 0, length = this.installers.Length; i < length; i++)
                {
                    IBlackboardInstaller installer = this.installers[i];
                    installer?.Install(this);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            this.Install();
        }

        public void OnBeforeSerialize()
        {
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!EditorApplication.isPlaying)
            {
                this.Install();
            }
        }
#endif

        #endregion

        #region Debug

        public static Func<int, string> _nameFunc;
        public static readonly HashSet<string> _tagCache = new();
        public static readonly Dictionary<string, object> _valueCache = new();

        [FoldoutGroup("Debug")]
        [LabelText("Tags")]
        [ShowInInspector, ReadOnly]
        public IReadOnlyCollection<string> TagsDebug
        {
            get
            {
                _tagCache.Clear();
                foreach (var tagId in this.blackboard.TagValues)
                {
                    string name = _nameFunc?.Invoke(tagId) ?? tagId.ToString();
                    _tagCache.Add(name);
                }

                return _tagCache;
            }
        }
        
        [FoldoutGroup("Debug")]
        [LabelText("Values")]
        [ShowInInspector, ReadOnly]
        public IReadOnlyDictionary<string, object> ValuesDebug
        {
            get
            {
                _valueCache.Clear();

                foreach ((int key, bool value) in this.blackboard.BoolValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }

                foreach ((int key, int value) in this.blackboard.IntValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }
                
                foreach ((int key, float value) in this.blackboard.FloatValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }

                foreach ((int key, float2 value) in this.blackboard.Float2Values)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }
                
                foreach ((int key, float3 value) in this.blackboard.Float3Values)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }
                
                foreach ((int key, quaternion value) in this.blackboard.QuaternionValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }
                
                foreach ((int key, object value) in this.blackboard.ObjectValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }
                
                foreach ((int key, IBlackboard.IRef value) in this.blackboard.StructValues)
                {
                    string name = _nameFunc?.Invoke(key) ?? key.ToString();
                    _valueCache[name] =  value;
                }

                return _valueCache;
            }
        }

        #endregion
    }
}