using System;

namespace GOAPModule
{
    [Serializable]
    public struct GoapCondition
    {
        [WorldStateKey]
        public ushort key;
        
        public bool value;
    }
}