using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class ActionExtensions
    {
        public static void AddActionsOn<T>(
            this IObject obj,
            IReadOnlyCollection<IActionBuilder> actions,
            AtomicEvent<T> targetEvent
        )
        {
            if (actions == null)
            {
                return;
            }

            int count = actions.Count;
            if (count == 0)
            {
                return;
            }
            
            obj.WhenConstruct(() =>
            {
                foreach (var builder in actions)
                {
                    if (builder != null)
                    {
                        Action action = builder.Build(obj);
                        targetEvent.Subscribe(_ => action.Invoke());
                    }
                }
            });
        }
        
        public static void AddActionsOn<T>(
            this IObject obj,
            IReadOnlyCollection<IActionBuilder<T>> actions,
            AtomicEvent<T> targetEvent
        )
        {
            if (actions == null)
            {
                return;
            }

            int count = actions.Count;
            if (count == 0)
            {
                return;
            }
            
            obj.WhenConstruct(() =>
            {
                foreach (var builder in actions)
                {
                    if (builder != null)
                    {
                        var action = builder.Build(obj);
                        targetEvent.Subscribe(action);
                    }
                }
            });
        }

        public static void AddActionsOn(
            this IObject obj,
            IActionBuilder[] actions,
            AtomicEvent targetEvent
        )
        {
            if (actions == null)
            {
                return;
            }

            int count = actions.Length;
            if (count == 0)
            {
                return;
            }
            
            obj.WhenConstruct(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    IActionBuilder actionBuilder = actions[i];
                    if (actionBuilder != null)
                    {
                        Action action = actionBuilder.Build(obj);
                        targetEvent.Subscribe(action);
                    }
                }
            });
        }
    }
}