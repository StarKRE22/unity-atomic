using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class AtomicObjectExtensions
    {
        public static IAtomicObject.IBehaviour SubscribeOnEnable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicEnable(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnDisable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicDisable(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }

        public static IAtomicObject.IBehaviour SubcribeOnFixedUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicFixedUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }

        public static IAtomicObject.IBehaviour SubscribeOnLateUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicLateUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }

#if UNITY_EDITOR
        //Don't wrap UNITY_EDITOR
        public static IAtomicObject.IBehaviour SubscribeOnDrawGizmos(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicLateUpdate(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
#endif
        
        public static IAtomicObject.IBehaviour SubscribeOnTriggerEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicTriggerEnter(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnTriggerExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicTriggerExit(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnCollisionEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicCollisionEnter(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnCollisionExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicCollisionExit(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        
        public static IAtomicObject.IBehaviour SubscribeOnTriggerEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicTriggerEnter2D(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnTriggerExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicTriggerExit2D(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnCollisionEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicCollisionEnter2D(action);
            obj.AddBehaviour(behaviour);
            return behaviour;
        }
        
        public static IAtomicObject.IBehaviour SubscribeOnCollisionExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            IAtomicObject.IBehaviour behaviour = new AtomicCollisionExit2D(action);
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