// using System.Collections.Generic;
//
// namespace AI.GOAP
// {
//     public sealed class DijkstraPlanner : IPlanner
//     {
//         private IFactState worldState;
//         private IFactState goal;
//         private IActor[] actions;
//         
//         public bool MakePlan(IFactState worldState, IFactState goal, IActor[] actions, out List<IActor> plan)
//         {
//             if (goal == null)
//             {
//                 plan = default;
//                 return false;
//             }
//
//             if (goal.EqualsTo(worldState))
//             {
//                 plan = new List<IActor>(0);
//                 return true;
//             }
//
//             this.worldState = worldState;
//             this.goal = goal;
//             this.actions = actions;
//             
//             return this.MakePlanInternal(out plan);
//         }
//
//         private bool MakePlanInternal(out List<IActor> plan)
//         {
//             var costMap = this.CreateCostMap();
//             var costComparer = new CostComparer(costMap);
//
//             var openList = new List<IActor>(this.actions);
//             var actionGraph = PlannerUtils.CreateActionGraph(this.actions, this.worldState);
//             var actionConnections = new Dictionary<IActor, IActor>();
//             var endActions = new List<IActor>();
//
//             while (openList.Count > 0)
//             {
//                 openList.Sort(costComparer);
//                 
//                 IActor current = openList[0];
//                 
//                 openList.RemoveAt(0);
//
//                 // Check if reached start:
//                 if (current.RequiredState.EqualsTo(this.worldState))
//                 {
//                     endActions.Add(current);
//                     continue;
//                 }
//
//                 // Update distances and sequence:
//                 if (actionGraph.TryGetValue(current, out var neighbours))
//                 {
//                     foreach (var neighbor in neighbours)
//                     {
//                         var cost = costMap[current] + neighbor.EvaluateCost();
//                         if (cost < costMap[neighbor])
//                         {
//                             costMap[neighbor] = cost;
//                             actionConnections[neighbor] = current;
//                         }
//                     }
//                 }
//             }
//
//             //Plan not found:
//             if (endActions.Count <= 0)
//             {
//                 plan = default;
//                 return false;
//             }
//             
//             //Pick end action
//             endActions.Sort(costComparer);
//             IActor endAction = endActions[0];
//             
//             // Construct plan:
//             plan = new List<IActor> {endAction};
//             while (actionConnections.TryGetValue(endAction, out endAction))
//             {
//                 plan.Add(endAction);
//             }
//
//             return true;
//         }
//
//         private Dictionary<IActor, int> CreateCostMap()
//         {
//             var result = new Dictionary<IActor, int>();
//             
//             foreach (var action in this.actions)
//             {
//                 if (PlannerUtils.MatchesAction(action, this.goal, this.worldState))
//                 {
//                     result[action] = action.EvaluateCost();
//                 }
//                 else
//                 {
//                     result[action] = int.MaxValue;
//                 }
//             }
//
//             return result;
//         }
//
//         private sealed class CostComparer : IComparer<IActor>
//         {
//             private readonly Dictionary<IActor, int> costMap;
//
//             public CostComparer(Dictionary<IActor, int> costMap)
//             {
//                 this.costMap = costMap;
//             }
//
//             public int Compare(IActor a, IActor b)
//             {
//                 return this.costMap[a!] - this.costMap[b!];
//             }
//         }
//     }
// }