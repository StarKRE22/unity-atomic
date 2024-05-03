using System.Collections.Generic;
using AIModule;
using UnityEngine;

namespace GOAPModule
{
    public abstract class GoapSensor : ScriptableObject
    {
        protected internal abstract void GenerateFacts(IBlackboard blackboard, IDictionary<ushort, bool> worldState);
    }
}