using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class AtomicObjectExtensions
    {
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
        public static void ApplyAspect(this IAtomicObject obj, IAtomicObject.IAspect aspect)
        {
            aspect.Apply(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DiscardAspect(this IAtomicObject obj, IAtomicObject.IAspect aspect)
        {
            aspect.Discard(obj);
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