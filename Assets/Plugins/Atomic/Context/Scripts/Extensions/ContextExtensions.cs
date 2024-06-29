using System;

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
        
        public static T GetDataInParent<T>(this IContext context, int key) where T : class
        {
            while (true)
            {
                if (context == null)
                {
                    return null;
                }
                
                T data = context.GetData<T>(key);
                
                if (data != null)
                {
                    return data;
                }

                context = context.Parent;
            }
        }
        
        public static T GetDataInChildren<T>(this IContext context, int key) where T : class
        {
            throw new NotImplementedException();
        }
    }
}