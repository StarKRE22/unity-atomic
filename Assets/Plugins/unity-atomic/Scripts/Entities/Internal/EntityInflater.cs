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

            if (entity.keyMode != AtomicEntity.KeyMode.NONE)
            {
                //Register type keys:
                entity.typeKeys.UnionWith(entityInfo.typeKeys);

                //Register data map:
                ValueKeyInfo[] dataKeys = entityInfo.dataKeys;
                for (int i = 0, count = dataKeys.Length; i < count; i++)
                {
                    ValueKeyInfo valueInfo = dataKeys[i];
                    object value = valueInfo.valueFunc(source);
                    entity.fieldsKeys.Add(valueInfo.key, value);
                }
            }

            if (entity.indexMode != AtomicEntity.IndexMode.NONE)
            {
                //Register type indexes:
                int[] typeIndexes = entityInfo.typeIndexes;
                for (int i = 0, count = typeIndexes.Length; i < count; i++)
                {
                    int index = typeIndexes[i];
                    entity.typeIndexes.Add(index);
                }

                //Register data array:
                ValueIndexInfo[] dataIndexes = entityInfo.dataIndexes;
                for (int i = 0, count = dataIndexes.Length; i < count; i++)
                {
                    ValueIndexInfo valueInfo = dataIndexes[i];
                    object value = valueInfo.valueFunc(source);
                    entity.fieldsIndexes[valueInfo.index] = value;
                }
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}