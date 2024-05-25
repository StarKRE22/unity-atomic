using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace Atomic.Objects
{
    internal static class ObjectInflater
    {
        internal static void Inflate(IAtomicObject entity)
        {
            InflateFrom(entity, entity);
        }
        
        internal static void InflateFrom(IAtomicObject entity, object source)
        {
            Type sourceType = source.GetType();

#if UNITY_EDITOR
            Profiler.BeginSample($"Inflate Atomic Object: {sourceType.Name}", entity as MonoBehaviour);
#endif
            ObjectInfo objectInfo = ObjectBaker.Bake(sourceType);
            
            //Register types:
            int[] types = objectInfo.types;
            for (int i = 0, count = types.Length; i < count; i++)
            {
                int index = types[i];
                entity.AddTag(index);
            }

            //Register references:
            ValueInfo[] references = objectInfo.values;
            for (int i = 0, count = references.Length; i < count; i++)
            {
                ValueInfo valueInfo = references[i];
                object value = valueInfo.valueFunc(source);
                entity.SetValue(valueInfo.index, value);
            }
            
            //Register behaviours:
            LogicInfo[] behaviours = objectInfo.behaviours;
            for (int i = 0, count = behaviours.Length; i < count; i++)
            {
                LogicInfo logicInfo = behaviours[i];
                var behaviour = (ILogic) logicInfo.valueFunc(source);
                entity.AddLogic(behaviour);
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}