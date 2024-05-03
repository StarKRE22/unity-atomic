// using System.Collections.Generic;
// using System.Linq;
//
// namespace AI.GOAP
// {
//     public sealed class GreedyPlanner : IPlanner
//     {
//         private IActor[] actions;
//         private IFactState worldState;
//         
//         public bool MakePlan(IFactState worldState, IFactState goal, IActor[] actions, out List<IActor> plan)
//         {
//             if (goal.EqualsTo(worldState))
//             {
//                 plan = new List<IActor>();
//                 return true;
//             }
//
//             this.worldState = worldState;
//             this.actions = actions;
//
//             return this.MakePlanRecursively(goal, baseNode: null, out plan);
//         }
//
//         private bool MakePlanRecursively(IFactState goal, Node baseNode, out List<IActor> plan)
//         {
//             var neighbours = this.FindNeighbours(goal);
//             var orderedNeighbours = neighbours.OrderBy(it => it.EvaluateCost());
//
//             foreach (var action in orderedNeighbours)
//             {
//                 var node = new Node
//                 {
//                     baseNode = baseNode,
//                     action = action
//                 };
//                 
//                 var requiredState = action.RequiredState;
//                 if (requiredState.EqualsTo(this.worldState))
//                 {
//                     plan = this.CreatePlan(node);
//                     return true;
//                 }
//
//                 if (this.MakePlanRecursively(requiredState, node, out plan))
//                 {
//                     return true;
//                 }
//             }
//
//             plan = null;
//             return false;
//         }
//
//         private List<IActor> FindNeighbours(IFactState goal)
//         {
//             var result = new List<IActor>();
//             
//             foreach (var action in this.actions)
//             {
//                 if (PlannerUtils.MatchesAction(action, goal, this.worldState))
//                 {
//                     result.Add(action);
//                 }
//             }
//
//             return result;
//         }
//
//         private List<IActor> CreatePlan(Node endNode)
//         {
//             var plan = new List<IActor>();
//
//             while (endNode != null)
//             {
//                 plan.Add(endNode.action);
//                 endNode = endNode.baseNode;
//             }
//             
//             return plan;
//         }
//
//         private sealed class Node
//         {
//             public Node baseNode;
//             public IActor action;
//         }
//     }
// }
