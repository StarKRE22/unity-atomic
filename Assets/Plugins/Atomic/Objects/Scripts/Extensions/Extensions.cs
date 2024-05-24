using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddElement(this IAtomicObject obj, int id, ILogic element)
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
                obj.DelLogic(removed as ILogic);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetElement(this IAtomicObject obj, int id, ILogic element)
        {
            obj.DelElement(id);
            obj.AddElement(id, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Compose(this IAtomicObject obj, IComposable aspect)
        {
            aspect.Compose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dispose(this IAtomicObject obj, IDisposable aspect)
        {
            aspect.Dispose(obj);
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

        public static ILogic SubscribeOnEnable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new EnableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnDisable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new DisableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new UpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubcribeOnFixedUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new FixedUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubscribeOnLateUpdate(
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
        public static ILogic SubscribeOnDrawGizmos(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
#endif
        
        public static ILogic SubscribeOnTriggerEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            ILogic logic = new TriggerEnterLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            ILogic logic = new TriggerExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            ILogic logic = new CollisionLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            ILogic logic = new CollisionExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        
        public static ILogic SubscribeOnTriggerEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            ILogic logic = new TriggerEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            ILogic logic = new TriggerExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            ILogic logic = new CollisionEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            ILogic logic = new CollisionExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
    }
}