using System.Collections.Generic;
using UnityEngine;

namespace GOAPModule
{
    public abstract class GoapPlanner : ScriptableObject
    {
        internal bool MakePlan(
            IReadOnlyDictionary<ushort, bool> worldState,
            GoapGoal goal,
            IEnumerable<GoapAction> actions,
            out List<GoapAction> plan
        )
        {
            return this.MakePlan(worldState, goal.postConditions, actions, out plan);
        }

        protected internal abstract bool MakePlan(
            IReadOnlyDictionary<ushort, bool> worldState,
            IReadOnlyDictionary<ushort, bool> goal,
            IEnumerable<GoapAction> actions,
            out List<GoapAction> plan
        );
    }
}