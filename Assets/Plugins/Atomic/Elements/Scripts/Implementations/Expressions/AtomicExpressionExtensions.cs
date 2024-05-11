namespace Atomic.Elements
{
    public static class AtomicExpressionExtensions
    {
        public static void AppendAll<T>(this IAtomicExpression<T> it, params IAtomicValue<T>[] members)
        {
            for (int i = 0, count = members.Length; i < count; i++)
            {
                IAtomicValue<T> member = members[i];
                it.Append(member);
            }
        }
    }
}