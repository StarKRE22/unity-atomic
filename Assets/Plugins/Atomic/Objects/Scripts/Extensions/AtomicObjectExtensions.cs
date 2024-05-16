using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class AtomicObjectExtensions
    {
        public static IAtomicObject.IBehaviour SubscribeOnUpdate(this IAtomicObject obj, Action<float> action)
        {
            
        }
        
        public static IAtomicObject.IBehaviour SubcribeOnFixedUpdate(this IAtomicObject obj, Action<float> action)
        {
            IAtomicObject.IBehaviour behaviour = new AtomicFixedUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnLateUpdate(this IAtomicObject obj, Action<float> action)
        {
            
        }

        //TODO: Добавить реактивщину
        public static void SubscribeOnTriggerEnter2D(this IAtomicObject obj, Action<Collider2D> action)
        {
            IAtomicObject.IBehaviour behaviour = new AtomicFixedUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddElement(this IAtomicObject obj, int id, IAtomicObject.IBehaviour element)
        {
            if (obj.AddReference(id, element))
            {
                obj.AddBehaviour(element);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelElement(this IAtomicObject obj, int id)
        {
            if (obj.DelReference(id, out var removed))
            {
                obj.DelBehaviour(removed as IAtomicObject.IBehaviour);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetElement(this IAtomicObject obj, int id, IAtomicObject.IBehaviour element)
        {
            obj.DelElement(id);
            obj.AddElement(id, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Compose(this IAtomicObject obj, IAtomicObject.IComposable aspect)
        {
            aspect.Compose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dispose(this IAtomicObject obj, IAtomicObject.IDisposable aspect)
        {
            aspect.Dispose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this GameObject gameObject, out IAtomicObject obj)
        {
            return gameObject.TryGetComponent(out obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Component component, out IAtomicObject obj)
        {
            return component.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Collision2D collision2D, out IAtomicObject obj)
        {
            return collision2D.gameObject.TryGetComponent(out obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Collision collision, out IAtomicObject obj)
        {
            return collision.gameObject.TryGetComponent(out obj);
        }
    }
}