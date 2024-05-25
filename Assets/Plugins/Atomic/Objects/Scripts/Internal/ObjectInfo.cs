namespace Atomic.Objects
{
    internal sealed class ObjectInfo
    {
        public readonly int[] types;
        public readonly ValueInfo[] values;
        public readonly LogicInfo[] behaviours;

        internal ObjectInfo(int[] types, ValueInfo[] values, LogicInfo[] behaviours)
        {
            this.types = types;
            this.values = values;
            this.behaviours = behaviours;
        }
    }
}