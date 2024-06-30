using System;

namespace Atomic.Contexts
{
    public sealed class DebugUtils
    {
        public static Func<int, string> ValueNameFormatter;

        public static string ConvertToName(int id)
        {
            return ValueNameFormatter?.Invoke(id) ?? id.ToString();
        }
    }
}