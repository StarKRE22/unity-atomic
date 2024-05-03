using System.Collections.Generic;
using UnityEngine;

namespace GOAPModule
{
    //TODO:
    [CreateAssetMenu(
        fileName = "DijkstraPlanner",
        menuName = "Goap/New DijkstraPlanner"
    )]
    internal sealed class DijkstraPlanner : GoapPlanner
    {
        protected internal override bool MakePlan(
            IReadOnlyDictionary<ushort, bool> goal,
            IReadOnlyDictionary<ushort, bool> worldState, 
            IEnumerable<GoapAction> actions,
            out List<GoapAction> plan)
        {
            plan = default;
            return false;
        }
    }
}