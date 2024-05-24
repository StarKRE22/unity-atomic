using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddElement(this IObject obj, int id, ILogic element)
        {
            if (obj.AddValue(id, element))
            {
                obj.AddLogic(element);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelElement(this IObject obj, int id)
        {
            if (obj.DelValue(id, out var removed))
            {
                obj.DelLogic(removed as ILogic);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetElement(this IObject obj, int id, ILogic element)
        {
            obj.DelElement(id);
            obj.AddElement(id, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Compose(this IObject obj, IComposable aspect)
        {
            aspect.Compose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dispose(this IObject obj, IDisposable aspect)
        {
            aspect.Dispose(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this UnityEngine.GameObject gameObject, out IObject obj)
        {
            return gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Component component, out IObject obj)
        {
            return component.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Collision2D collision2D, out IObject obj)
        {
            return collision2D.gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAtomicObject(this Collision collision, out IObject obj)
        {
            return collision.gameObject.TryGetComponent(out obj);
        }

        public static ILogic SubscribeOnEnable(
            this IObject obj,
            Action<IObject> action
        )
        {
            var behaviour = new EnableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnDisable(
            this IObject obj,
            Action<IObject> action
        )
        {
            var behaviour = new DisableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnUpdate(
            this IObject obj,
            Action<IObject, float> action
        )
        {
            var behaviour = new UpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubcribeOnFixedUpdate(
            this IObject obj,
            Action<IObject, float> action
        )
        {
            var behaviour = new FixedUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubscribeOnLateUpdate(
            this IObject obj,
            Action<IObject, float> action
        )
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

#if UNITY_EDITOR
        //Don't wrap UNITY_EDITOR
        public static ILogic SubscribeOnDrawGizmos(
            this IObject obj,
            Action<IObject, float> action
        )
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
#endif
        
        public static ILogic SubscribeOnTriggerEnter(
            this IObject obj,
            Action<IObject, Collider> action
        )
        {
            ILogic logic = new TriggerEnterLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit(
            this IObject obj,
            Action<IObject, Collider> action
        )
        {
            ILogic logic = new TriggerExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter(
            this IObject obj,
            Action<IObject, Collision> action
        )
        {
            ILogic logic = new CollisionLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit(
            this IObject obj,
            Action<IObject, Collision> action
        )
        {
            ILogic logic = new CollisionExitLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        
        public static ILogic SubscribeOnTriggerEnter2D(
            this IObject obj,
            Action<IObject, Collider2D> action
        )
        {
            ILogic logic = new TriggerEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit2D(
            this IObject obj,
            Action<IObject, Collider2D> action
        )
        {
            ILogic logic = new TriggerExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter2D(
            this IObject obj,
            Action<IObject, Collision2D> action
        )
        {
            ILogic logic = new CollisionEnter2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit2D(
            this IObject obj,
            Action<IObject, Collision2D> action
        )
        {
            ILogic logic = new CollisionExit2DLogic(action);
            obj.AddLogic(logic);
            return logic;
        }
    }
}