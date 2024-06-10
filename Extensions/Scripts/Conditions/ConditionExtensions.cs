using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class ConditionExtensions
    {
         public static void AddConditionsOn(
            this IObject obj,
            IConditionBuilder[] conditions,
            IAtomicExpression<bool> targetExpression
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Length;

            if (count == 0)
            {
                return;
            }

            obj.WhenConstruct(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    IConditionBuilder conditionBuilder = conditions[i];
                    if (conditionBuilder != null)
                    {
                        Func<bool> condition = conditionBuilder.Build(obj);
                        targetExpression.Append(condition);
                    }
                }
            });
        }
        
        
        public static void AddConditionsOn<T>(
            this IObject obj,
            IReadOnlyCollection<IConditionBuilder<T>> conditions,
            IAtomicExpression<T, bool> targetExpression
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Count;

            if (count == 0)
            {
                return;
            }

            obj.WhenConstruct(() =>
            {
                foreach (var builder in conditions)
                {
                    if (builder != null)
                    {
                        Func<T, bool> condition = builder.Build(obj);
                        targetExpression.Append(condition);
                    }
                }
            });
        }
        
        public static void AddConditionsOn<T>(
            this IObject obj,
            IReadOnlyCollection<IConditionBuilder> conditions,
            IAtomicExpression<T, bool> targetExpression
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Count;

            if (count == 0)
            {
                return;
            }

            obj.WhenConstruct(() =>
            {
                foreach (var builder in conditions)
                {
                    if (builder != null)
                    {
                        Func<bool> condition = builder.Build(obj);
                        targetExpression.Append(_ => condition.Invoke());
                    }
                }
            });
        }
    }
}