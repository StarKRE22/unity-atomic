using System.Linq;

namespace Atomic
{
    internal sealed class EntityInfo
    {
        public readonly string[] typeKeys;
        public readonly int[] typeIndexes;
        public readonly int startTypeIndex;
        public readonly int endTypeIndex;

        public readonly ValueKeyInfo[] dataKeys;
        public readonly ValueIndexInfo[] dataIndexes;
        public readonly int startDataIndex;
        public readonly int endDataIndex;

        internal EntityInfo(
            string[] typeKeys,
            int[] typeIndexes,
            ValueKeyInfo[] dataKeys,
            ValueIndexInfo[] dataIndexes
        )
        {
            this.typeKeys = typeKeys;
            this.typeIndexes = typeIndexes;

            if (typeIndexes.Length > 0)
            {
                this.startTypeIndex = typeIndexes.Min(it => it);
                this.endTypeIndex = typeIndexes.Max(it => it);
            }
            
            this.dataKeys = dataKeys;
            this.dataIndexes = dataIndexes;

            if (dataIndexes.Length > 0)
            {
                this.startDataIndex = dataIndexes.Min(it => it.index);
                this.endDataIndex = dataIndexes.Max(it => it.index);
            }
        }
    }
}