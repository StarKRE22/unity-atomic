using System;
using UnityEngine.Profiling;

namespace Atomic
{
    internal static class EntityInflater
    {
        internal static void Inflate(AtomicEntity entity)
        {
            InflateFrom(entity, entity);
        }
        
        internal static void InflateFrom(AtomicEntity entity, object source)
        {
            Type sourceType = source.GetType();

            if (sourceType == typeof(AtomicEntity) || sourceType == typeof(AtomicObject))
            {
                return;
            }

#if UNITY_EDITOR
            Profiler.BeginSample($"Inflate Atomic Entity: {sourceType.Name}", entity);
#endif
            EntityInfo entityInfo = EntityBaker.Bake(sourceType);

            //Register type indexes:
            int[] typeIndexes = entityInfo.types;
            for (int i = 0, count = typeIndexes.Length; i < count; i++)
            {
                int index = typeIndexes[i];
                entity.Mark(index);
            }

            //Register data array:
            ValueInfo[] dataIndexes = entityInfo.values;
            for (int i = 0, count = dataIndexes.Length; i < count; i++)
            {
                ValueInfo valueInfo = dataIndexes[i];
                object value = valueInfo.valueFunc(source);
                entity.Put(valueInfo.index, value);
            }
            
#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}