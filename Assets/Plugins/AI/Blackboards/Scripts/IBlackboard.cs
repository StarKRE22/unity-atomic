using System.Collections.Generic;
using UnityEngine;

namespace AIModule
{
    public interface IBlackboard
    {
        bool HasKey(ushort key);

        bool GetBool(ushort key);
        int GetInt(ushort key);
        float GetFloat(ushort key);
        T GetObject<T>(ushort key) where T : class;
        Vector2 GetVector2(ushort key);
        Vector3 GetVector3(ushort key);
        Quaternion GetQuaternion(ushort key);

        bool TryGetBool(ushort key, out bool value);
        bool TryGetInt(ushort key, out int value);
        bool TryGetFloat(ushort key, out float value);
        bool TryGetObject<T>(ushort key, out T value);
        bool TryGetVector2(ushort key, out Vector2 value);
        bool TryGetVector3(ushort key, out Vector3 value);
        bool TryGetQuaternion(ushort key, out Quaternion value);

        void SetBool(ushort key, bool value);
        void SetInt(ushort key, int value);
        void SetFloat(ushort key, float value);
        void SetObject(ushort key, object value);
        void SetVector2(ushort key, Vector2 value);
        void SetVector3(ushort key, Vector3 value);
        void SetQuaternion(ushort key, Quaternion value);

        void DeleteBool(ushort key);
        void DeleteInt(ushort key);
        void DeleteFloat(ushort key);
        void DeleteObject(ushort key);
        void DeleteVector2(ushort key);
        void DeleteVector3(ushort key);
        void DeleteQuaternion(ushort key);
        
        IReadOnlyDictionary<ushort, bool> BoolValues();
        IReadOnlyDictionary<ushort, int> IntValues();
        IReadOnlyDictionary<ushort, float> FloatValues();
        IReadOnlyDictionary<ushort, object> ObjectValues();
        IReadOnlyDictionary<ushort, Vector2> Vector2Values();
        IReadOnlyDictionary<ushort, Vector3> Vector3Values();
        IReadOnlyDictionary<ushort, Quaternion> QuaternionValues();
    }
}