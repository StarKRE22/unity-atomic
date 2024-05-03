using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic
{
    //TODO: ECS WORLD OPTIMIZATION
    public static partial class AtomicEntities
    {
        private static readonly AtomicTypePool[] typePools;
        private static readonly IAtomicValuePool[] valuesPools;

        private static readonly Dictionary<int, (List<int>, List<int>)> activeEntities = new();
        private static readonly Queue<int> recycledEntities = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsType(int index, int entity)
        {
            return typePools[index].Has(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(int index, int entity)
        {
            var pool = (AtomicValuePool<T>) valuesPools[index];
            return pool.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(int index, int entity, out T result)
        {
            var pool = (AtomicValuePool<T>) valuesPools[index];
            result = pool.Get(entity);
            return result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetValue(int index, int entity)
        {
            return valuesPools[index].Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue(int index, int entity, out object result)
        {
            result = valuesPools[index].Get(entity);
            return result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicTypePool GetTypes(int index)
        {
            return typePools[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicValuePool GetValues(int index)
        {
            return valuesPools[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicValuePool<T> GetValues<T>(int index) where T : class
        {
            return (AtomicValuePool<T>) valuesPools[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PutValue<T>(int index, int entity, T value)
        {
            var pool = (AtomicValuePool<T>) valuesPools[index];
            return pool.Put(entity, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(int index, int entity)
        {
            return valuesPools[index].Del(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T>(int index, int entity, T value)
        {
            var pool = (AtomicValuePool<T>) valuesPools[index];
            pool.Set(entity, value);
        }
        
        
        public static void GetValue<T>(int index, int entity, ref T value)
        {
            var pool = (AtomicValuePool<T>) valuesPools[index];
            pool.Get()
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMarker(int index, int entity)
        {
            return typePools[index].Add(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveMarker(int index, int entity)
        {
            return typePools[index].Del(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int NewEntity()
        {
            if (!recycledEntities.TryDequeue(out var entity))
            {
                entity = activeEntities.Count;
            }

            activeEntities.Add(entity, (new List<int>(), new List<int>()));
            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DelEntity(int entity)
        {
            if (activeEntities.Remove(entity, out (List<int> types, List<int> values) data))
            {
                foreach (var type in data.types)
                {
                    typePools[type].Del(entity);
                }

                foreach (var value in data.values)
                {
                    valuesPools[value].Del(entity);
                }

                recycledEntities.Enqueue(entity);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IAtomicValuePool CreatePool(string typeName)
        {
            //PARSE STRING https://stackoverflow.com/questions/20532691/how-to-parse-c-sharp-generic-type-names
            Type type = TypeResolver.GetType(typeName);
            if (type == null)
            {
                throw new Exception($"Undefined type: {typeName}");
            }

            if (!type.IsClass)
            {
                throw new Exception($"Type should be a class: {typeName}");
            }

            Type poolType = typeof(AtomicValuePool<>).MakeGenericType(type);
            return (IAtomicValuePool) Activator.CreateInstance(poolType);
        }

        public static AtomicEntityFilter GetFilter()
        {
            
        }

    
    }
}