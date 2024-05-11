namespace Atomic.Objects
{
    internal sealed class ObjectInfo
    {
        public readonly int[] types;
        public readonly ReferenceInfo[] references;
        public readonly BehaviourInfo[] behaviours;

        internal ObjectInfo(int[] types, ReferenceInfo[] references, BehaviourInfo[] behaviours)
        {
            this.types = types;
            this.references = references;
            this.behaviours = behaviours;
        }
    }
}