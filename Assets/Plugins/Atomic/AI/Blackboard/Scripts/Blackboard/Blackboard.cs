using System.Collections.Generic;
using Unity.Mathematics;

namespace Modules.AI
{
    public sealed class Blackboard : IBlackboard
    {
        private readonly HashSet<int> tags = new();
        private readonly Dictionary<int, bool> boolValues = new();
        private readonly Dictionary<int, int> intValues = new();
        private readonly Dictionary<int, float> floatValues = new();
        private readonly Dictionary<int, float2> float2Values = new();
        private readonly Dictionary<int, float3> float3Values = new();
        private readonly Dictionary<int, quaternion> quaternionValues = new();
        private readonly Dictionary<int, object> objValues = new();
        private readonly Dictionary<int, IBlackboard.IRef> structValues = new();

        public IReadOnlyCollection<int> TagValues => this.tags;
        public IReadOnlyDictionary<int, bool> BoolValues => this.boolValues;
        public IReadOnlyDictionary<int, int> IntValues => this.intValues;
        public IReadOnlyDictionary<int, float> FloatValues => this.floatValues;
        public IReadOnlyDictionary<int, object> ObjectValues => this.objValues;
        public IReadOnlyDictionary<int, float2> Float2Values => this.float2Values;
        public IReadOnlyDictionary<int, float3> Float3Values => this.float3Values;
        public IReadOnlyDictionary<int, IBlackboard.IRef> StructValues => this.structValues;
        public IReadOnlyDictionary<int, quaternion> QuaternionValues => this.quaternionValues;

        public bool HasTag(int key) => this.tags.Contains(key);
        public bool HasBool(int key) => this.boolValues.ContainsKey(key);
        public bool HasInt(int key) => this.intValues.ContainsKey(key);
        public bool HasFloat(int key) => this.float2Values.ContainsKey(key);
        public bool HasFloat2(int key) => this.float2Values.ContainsKey(key);
        public bool HasFloat3(int key) => this.float3Values.ContainsKey(key);
        public bool HasQuaternion(int key) => this.quaternionValues.ContainsKey(key);
        public bool HasObject(int key) => this.objValues.ContainsKey(key);
        public bool HasStruct(int key) => this.structValues.ContainsKey(key);

        public bool GetBool(int key) => this.boolValues[key];
        public int GetInt(int key) => this.intValues[key];
        public float GetFloat(int key) => this.floatValues[key];
        public object GetObject(int key) => this.objValues[key];
        public float2 GetFloat2(int key) => this.float2Values[key];
        public float3 GetFloat3(int key) => this.float3Values[key];
        public quaternion GetQuaternion(int key) => this.quaternionValues[key];
        
        public T GetObject<T>(int key) where T : class
        {
            return (T) this.objValues[key];
        }

        public ref T GetStruct<T>(int key) where T : struct
        {
            var @struct = (IBlackboard.Ref<T>) this.structValues[key];
            return ref @struct.value;
        }

        public bool TryGetBool(int key, out bool value) =>
            this.boolValues.TryGetValue(key, out value);

        public bool TryGetInt(int key, out int value) =>
            this.intValues.TryGetValue(key, out value);

        public bool TryGetFloat(int key, out float value) =>
            this.floatValues.TryGetValue(key, out value);

        public bool TryGetObject(int key, out object value) =>
            this.objValues.TryGetValue(key, out value);

        public bool TryGetObject<T>(int key, out T value) where T : class
        {
            if (this.objValues.TryGetValue(key, out object result))
            {
                value = (T) result;
                return true;
            }

            value = default;
            return false;
        }

        // public bool TryGetStruct<T>(int key, out T value) where T : struct
        // {
        //     if (this.structValues.TryGetValue(key, out IBlackboard.IStruct ptr))
        //     {
        //         value = ((IBlackboard.Struct<T>) ptr).value;
        //         return true;
        //     }
        //
        //     value = default;
        //     return false;
        // }

        public bool TryGetStruct<T>(int key, out IBlackboard.Ref<T> value) where T : struct
        {
            if (this.structValues.TryGetValue(key, out IBlackboard.IRef ptr))
            {
                value = (IBlackboard.Ref<T>) ptr;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetFloat2(int key, out float2 value) =>
            this.float2Values.TryGetValue(key, out value);

        public bool TryGetFloat3(int key, out float3 value) =>
            this.float3Values.TryGetValue(key, out value);

        public bool TryGetQuaternion(int key, out quaternion value) =>
            this.quaternionValues.TryGetValue(key, out value);

        public void SetTag(int key) => this.tags.Add(key);
        public void SetBool(int key, bool value) => this.boolValues[key] = value;
        public void SetInt(int key, int value) => this.intValues[key] = value;
        public void SetFloat(int key, float value) => this.floatValues[key] = value;
        public void SetFloat2(int key, float2 value) => this.float2Values[key] = value;
        public void SetFloat3(int key, float3 value) => this.float3Values[key] = value;
        public void SetObject(int key, object value) => this.objValues[key] = value;
        public void SetQuaternion(int key, quaternion value) => this.quaternionValues[key] = value;

        public void SetStruct<T>(int key, T value) where T : struct
        {
            if (this.structValues.TryGetValue(key, out IBlackboard.IRef @struct) &&
                @struct is IBlackboard.Ref<T> tStruct)
            {
                tStruct.value = value;
            }
            else
            {
                this.structValues[key] = new IBlackboard.Ref<T>(value);
            }
        }

        public bool DelTag(int key) => this.tags.Remove(key);
        public bool DelBool(int key) => this.boolValues.Remove(key);
        public bool DelInt(int key) => this.intValues.Remove(key);
        public bool DelFloat(int key) => this.floatValues.Remove(key);
        public bool DelObject(int key) => this.objValues.Remove(key);
        public bool DelStruct(int key) => this.structValues.Remove(key);
        public bool DelFloat2(int key) => this.float2Values.Remove(key);
        public bool DelFloat3(int key) => this.float3Values.Remove(key);
        public bool DelQuaternion(int key) => this.float3Values.Remove(key);
    }
}