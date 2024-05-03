using System.Collections.Generic;
using UnityEngine;

namespace GOAPModule
{
    //TODO:
    [CreateAssetMenu(
        fileName = "AStarPlanner",
        menuName = "Goap/New AStarPlanner"
    )]
    internal sealed class AStarPlanner : GoapPlanner
    {
        protected internal override bool MakePlan(
            IReadOnlyDictionary<ushort, bool> goal,
            IReadOnlyDictionary<ushort, bool> worldState,
            IEnumerable<GoapAction> actions,
            out List<GoapAction> plan
        )
        {
            plan = default;
            return false;
        }
    }
}