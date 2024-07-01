using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Modules.AI
{
    public interface IBlackboard
    {
        bool HasTag(int key);
        bool HasBool(int key);
        bool HasInt(int key);
        bool HasFloat(int key);
        bool HasFloat2(int key);
        bool HasFloat3(int key);
        bool HasQuaternion(int key);
        bool HasObject(int key);
        bool HasStruct(int key);

        bool GetBool(int key);
        int GetInt(int key);
        float GetFloat(int key);
        float2 GetFloat2(int key);
        float3 GetFloat3(int key);
        quaternion GetQuaternion(int key);
        object GetObject(int key);
        T GetObject<T>(int key) where T : class;
        ref T GetStruct<T>(int key) where T : struct;

        bool TryGetBool(int key, out bool value);
        bool TryGetInt(int key, out int value);
        bool TryGetFloat(int key, out float value);
        bool TryGetFloat2(int key, out float2 value);
        bool TryGetFloat3(int key, out float3 value);
        bool TryGetQuaternion(int key, out quaternion value);
        bool TryGetObject(int key, out object value);
        bool TryGetObject<T>(int key, out T value) where T : class;
        bool TryGetStruct<T>(int key, out Ref<T> value) where T : struct;
        
        void SetTag(int key);
        void SetBool(int key, bool value);
        void SetInt(int key, int value);
        void SetFloat(int key, float value);
        void SetFloat2(int key, float2 value);
        void SetFloat3(int key, float3 value);
        void SetQuaternion(int key, quaternion value);
        void SetObject(int key, object value);
        void SetStruct<T>(int key, T value) where T : struct;

        bool DelTag(int key);
        bool DelBool(int key);
        bool DelInt(int key);
        bool DelFloat(int key);
        bool DelObject(int key);
        bool DelStruct(int key);
        bool DelFloat2(int key);
        bool DelFloat3(int key);
        bool DelQuaternion(int key);
        
        IReadOnlyCollection<int> TagValues { get; }
        IReadOnlyDictionary<int, bool> BoolValues { get; }
        IReadOnlyDictionary<int, int> IntValues { get; }
        IReadOnlyDictionary<int, float> FloatValues { get; }
        IReadOnlyDictionary<int, object> ObjectValues { get; }
        IReadOnlyDictionary<int, float2> Float2Values { get; }
        IReadOnlyDictionary<int, float3> Float3Values { get; }
        IReadOnlyDictionary<int, quaternion> QuaternionValues { get; }
        IReadOnlyDictionary<int, IRef> StructValues { get; }
        
        public interface IRef
        {
            object Value { get; }
        }
        
        [Serializable]
        public sealed class Ref<T> : IRef
        {
            public object Value => value;

            [SerializeField]
            public T value;

            internal Ref(T value)
            {
                this.value = value;
            }

            public static implicit operator T(Ref<T> it)
            {
                return it.value;
            }
        }
    }
}