namespace Atomic.Objects
{
    internal sealed class ObjectInfo
    {
        public readonly int[] types;
        public readonly ValueInfo[] references;
        public readonly LogicInfo[] behaviours;

        internal ObjectInfo(int[] types, ValueInfo[] references, LogicInfo[] behaviours)
        {
            this.types = types;
            this.references = references;
            this.behaviours = behaviours;
        }
    }
}