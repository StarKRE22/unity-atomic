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

            if (sourceType == typeof(AtomicObject))
            {
                return;
            }

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
            ReferenceInfo[] references = objectInfo.references;
            for (int i = 0, count = references.Length; i < count; i++)
            {
                ReferenceInfo referenceInfo = references[i];
                object value = referenceInfo.valueFunc(source);
                entity.SetReference(referenceInfo.index, value);
            }
            
            //Register behaviours:
            BehaviourInfo[] behaviours = objectInfo.behaviours;
            for (int i = 0, count = behaviours.Length; i < count; i++)
            {
                BehaviourInfo behaviourInfo = behaviours[i];
                var behaviour = (IAtomicObject.IBehaviour) behaviourInfo.valueFunc(source);
                entity.AddBehaviour(behaviour);
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}