using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddElement(this IAtomicObject obj, int id, IAtomicLogic element)
        {
            if (obj.AddValue(id, element))
            {
                obj.AddLogic(element);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelElement(this IAtomicObject obj, int id)
        {
            if (obj.DelValue(id, out var removed))
            {
                obj.DelLogic(removed as IAtomicLogic);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetElement(this IAtomicObject obj, int id, IAtomicLogic element)
        {
            obj.DelElement(id);
            obj.AddElement(id, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Compose(this IAtomicObject obj, IAtomicAspect aspect)
        {
            aspect.Compose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dispose(this IAtomicObject obj, IAtomicAspect aspect)
        {
            aspect.Dispose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InflateFrom(this IAtomicObject entity, object from)
        {
            ObjectInflater.InflateFrom(entity, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this UnityEngine.GameObject gameObject, out IAtomicObject obj)
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

        public static IAtomicLogic SubscribeOnEnable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicEnable(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static IAtomicLogic SubscribeOnDisable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicDisable(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static IAtomicLogic SubscribeOnUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicUpdate(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static IAtomicLogic SubcribeOnFixedUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicFixedUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static IAtomicLogic SubscribeOnLateUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

#if UNITY_EDITOR
        //Don't wrap UNITY_EDITOR
        public static IAtomicLogic SubscribeOnDrawGizmos(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
#endif
        
        public static IAtomicLogic SubscribeOnTriggerEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            IAtomicLogic logic = new TriggerEnterLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnTriggerExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            IAtomicLogic logic = new TriggerExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnCollisionEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            IAtomicLogic logic = new CollisionLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnCollisionExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            IAtomicLogic logic = new CollisionExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        
        public static IAtomicLogic SubscribeOnTriggerEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            IAtomicLogic logic = new TriggerEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnTriggerExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            IAtomicLogic logic = new TriggerExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnCollisionEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            IAtomicLogic logic = new CollisionEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static IAtomicLogic SubscribeOnCollisionExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            IAtomicLogic logic = new CollisionExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
    }
}