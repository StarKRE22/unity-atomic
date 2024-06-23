using System;
using UnityEngine;

namespace Atomic.Objects
{
    public static class LogicExtensions
    {
        public static ILogic WhenConstruct(this IObject obj, Action action)
        {
            var behaviour = new ConstructableLogic(_ => action.Invoke());
            obj.AddLogic(behaviour);
            return behaviour;
        }
        
        public static ILogic WhenEnable(this IObject obj, Action action)
        {
            var behaviour = new EnableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic WhenDisable(this IObject obj, Action action)
        {
            var behaviour = new DisableLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic WhenUpdate(this IObject obj, Action<float> action)
        {
            var behaviour = new UpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic WhenFixedUpdate(this IObject obj, Action<float> action)
        {
            var behaviour = new FixedUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }

        public static ILogic WhenLateUpdate(this IObject obj, Action<float> action)
        {
            var behaviour = new LateUpdateLogic(action);
            obj.AddLogic(behaviour);
            return behaviour;
        }
    }
}