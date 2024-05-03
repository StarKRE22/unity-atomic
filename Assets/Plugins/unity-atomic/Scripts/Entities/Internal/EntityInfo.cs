namespace Atomic
{
    internal sealed class EntityInfo
    {
        public readonly int[] types;
        public readonly ValueInfo[] values;

        internal EntityInfo(int[] types, ValueInfo[] values)
        {
            this.types = types;
            this.values = values;
        }
    }
}