using UnityEngine;

namespace Atomic
{
    public static partial class AtomicEntities
    {
        //Code gen... count of values
        static AtomicEntities()
        {
            typePools = new AtomicTypePool[]
            {
                new(),
                new()
            };

            valuesPools = new[]
            {
                CreatePool(typeof(Transform).FullName),
                CreatePool(typeof(GameObject).FullName),
            };
        }
    }
}