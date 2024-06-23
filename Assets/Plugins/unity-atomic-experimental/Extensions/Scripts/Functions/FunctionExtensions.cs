using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class FunctionExtensions
    {
        public static void AddFunctionsOn<T, R>(
            this IObject obj,
            IEnumerable<IFunctionBuilder<T, R>> functions,
            IAtomicExpression<T, R> expression
        )
        {
            if (functions == null || !functions.Any())
            {
                return;
            }

            obj.WhenConstruct(() =>
            {
                foreach (IFunctionBuilder<T, R> builder in functions)
                {
                    if (builder != null)
                    {
                        Func<T, R> func = builder.Build(obj);
                        expression.Append(func);
                    }
                }
            });
        }
    }
}