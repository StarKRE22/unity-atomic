namespace Atomic.Contexts
{
    public static class ContextExtensions
    {
        public static bool IsOff(this IContext context)
        {
            return context.State == ContextState.OFF;
        }

        public static bool IsIntialized(this IContext context)
        {
            return context.State == ContextState.INITIALIZED;
        }

        public static bool IsEnabled(this IContext context)
        {
            return context.State == ContextState.ENABLED;
        }

        public static bool IsDisabled(this IContext context)
        {
            return context.State == ContextState.DISABLED;
        }

        public static bool IsDestroyed(this IContext context)
        {
            return context.State == ContextState.DESTROYED;
        }

        public static T GetValueInParent<T>(
            this IContext context,
            int key,
            bool includeSelf = true
        )
            where T : class
        {
            if (includeSelf)
            {
                T data = context.GetValue<T>(key);
                if (data != null)
                {
                    return data;
                }
            }

            while (true)
            {
                context = context.Parent;
                if (context == null)
                {
                    return null;
                }

                T data = context.GetValue<T>(key);

                if (data != null)
                {
                    return data;
                }
            }
        }

        public static T GetValueInChildren<T>(
            this IContext context,
            int key,
            bool includeSelf = true
        ) where T : class
        {
            if (includeSelf)
            {
                T data = context.GetValue<T>(key);
                if (data != null)
                {
                    return data;
                }
            }
            
            foreach (IContext child in context.Children)
            {
                T value = child.GetValueInChildren<T>(key, includeSelf: true);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}


// public static bool BindParent(this IContext child, IContext parent)
// {
//     IContext prevParent = child.Parent;
//     if (prevParent == parent)
//     {
//         return false;
//     }
//
//     prevParent?.Children.Remove(child);
//     child.Parent = parent;
//     parent?.Children.Add(child);
//     return true;
// }
//
// public static bool BindChild(this IContext parent, IContext child)
// {
//     if (parent.AddChild(child))
//     {
//         child.Parent = parent;
//     }
//     
//     ;
//     
//     if (!)
//     {
//         return false;
//     }
//     
//     
//     
//     IContext prevParent = child.Parent;
//     if (prevParent == parent)
//     {
//         return false;
//     }
//
//     prevParent?.Children.Remove(child);
//     child.Parent = parent;
//     parent?.Children.Add(child);
//     return true;
// }
//
// public static bool UnbindChild(this IContext parent, IContext child)
// {
//     IContext prevParent = child.Parent;
//     if (prevParent == parent)
//     {
//         return false;
//     }
//
//     prevParent?.Children.Remove(child);
//     child.Parent = parent;
//     parent?.Children.Add(child);
//     return true;
// }

// public static T GetValueInParentAndSelf<T>(this IContext context, int key) where T : class
// {
//     T data = context.GetValue<T>(key);
//     if (data != null)
//     {
//         return data;
//     }
//
//     return context.GetValueInParent<T>(key);
// }

// private static T GetValueInChildren<T>(IEnumerable<IContext> children, int key) where T : class
// {
//     foreach (IContext child in children)
//     {
//         if (child.TryGetValue(key, out T value))
//         {
//             return value;
//         }
//     }
//
//     foreach (IContext child in children)
//     {
//         T value = GetValueInChildren<T>(child.Children, key);
//         if (value != null)
//         {
//             return value;
//         }
//     }
//
//     return null;
// }