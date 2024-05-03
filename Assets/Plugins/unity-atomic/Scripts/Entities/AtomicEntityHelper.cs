using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic
{
    public static class AtomicEntityHelper 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake<T>() where T : IAtomicEntity
        {
            EntityBaker.Bake(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(Type type)
        {
            EntityBaker.Bake(type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(params Type[] types)
        {
            foreach (var type in types)
            {
                EntityBaker.Bake(type);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bake(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                EntityBaker.Bake(type);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Inflate(AtomicEntity entity)
        {
            EntityInflater.Inflate(entity);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InflateFrom(AtomicEntity entity, object from)
        {
            EntityInflater.InflateFrom(entity, from);
        }
    }
}