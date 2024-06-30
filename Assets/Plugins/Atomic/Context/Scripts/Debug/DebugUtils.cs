using System;

namespace Atomic.Contexts
{
    public static class DebugUtils
    {
        public static Func<int, string> ValueNameFormatter;

        public static string ConvertToName(int id)
        {
            return ValueNameFormatter?.Invoke(id) ?? id.ToString();
        }
    }
}