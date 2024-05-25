using System;
using UnityEngine;

namespace Atomic.Objects
{
    public static class LogicExtensions
    {
        
        public static ILogic SubscribeOnEnable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicEnable(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnDisable(
            this IAtomicObject obj,
            Action<IAtomicObject> action
        )
        {
            var behaviour = new AtomicDisable(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic SubscribeOnUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicUpdate(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubcribeOnFixedUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicFixedUpdate(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic SubscribeOnLateUpdate(
            this IAtomicObject obj,
            Action<IAtomicObject, float> action
        )
        {
            var behaviour = new AtomicLateUpdate(action);
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
            var behaviour = new AtomicLateUpdate(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
#endif
        
        public static ILogic SubscribeOnTriggerEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            ILogic logic = new AtomicTriggerEnter(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider> action
        )
        {
            ILogic logic = new AtomicTriggerExit(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            ILogic logic = new AtomicCollisionEnter(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision> action
        )
        {
            ILogic logic = new AtomicCollisionExit(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        
        public static ILogic SubscribeOnTriggerEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            ILogic logic = new AtomicTriggerEnter2D(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnTriggerExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collider2D> action
        )
        {
            ILogic logic = new AtomicTriggerExit2D(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionEnter2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            ILogic logic = new AtomicCollisionEnter2D(action);
            obj.AddLogic(logic);
            return logic;
        }
        
        public static ILogic SubscribeOnCollisionExit2D(
            this IAtomicObject obj,
            Action<IAtomicObject, Collision2D> action
        )
        {
            ILogic logic = new AtomicCollisionExit2D(action);
            obj.AddLogic(logic);
            return logic;
        }
    }
}