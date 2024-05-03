// using System.Collections.Generic;
//
// // ReSharper disable MemberCanBePrivate.Local
//
// namespace AI.GOAP
// {
//     public sealed class AStarPlanner : IPlanner
//     {
//         private const int UNDEFINED_HEURISTIC_DISTANCE = 3;
//
//         private static readonly CostComparer costComparer = new();
//
//         private IActor[] actions;
//         private Dictionary<IActor, List<IActor>> actionGraph;
//         private Dictionary<IActor, Node> openList;
//         private HashSet<IActor> closedList;
//
//         private IFactState worldState;
//         private IFactState goal;
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
//             this.openList = new Dictionary<IActor, Node>();
//             this.closedList = new HashSet<IActor>();
//
//             return this.MakePlanInternal(out plan);
//         }
//
//         private bool MakePlanInternal(out List<IActor> plan)
//         {
//             this.VisitStartActions();
//
//             if (this.CheckFinish(out var endNode))
//             {
//                 plan = new List<IActor> {endNode.action};
//                 return true;
//             }
//
//             this.actionGraph = PlannerUtils.CreateActionGraph(this.actions, this.worldState);
//
//             while (this.SelectNextAction(out var node))
//             {
//                 this.closedList.Add(node.action);
//                 this.VisitNeighbourActions(node);
//
//                 if (this.CheckFinish(out endNode))
//                 {
//                     plan = this.CreatePlan(endNode);
//                     return true;
//                 }
//
//                 this.openList.Remove(node.action);
//             }
//
//             plan = default;
//             return false;
//         }
//
//         private void VisitStartActions()
//         {
//             foreach (var action in this.actions)
//             {
//                 if (PlannerUtils.MatchesAction(action, this.goal, this.worldState))
//                 {
//                     this.VisitAction(action, null);
//                 }
//             }
//         }
//
//         private void VisitNeighbourActions(Node node)
//         {
//             if (!this.actionGraph.TryGetValue(node.action, out var neighbours))
//             {
//                 return;
//             }
//
//             foreach (var action in neighbours)
//             {
//                 this.VisitAction(action, node);
//             }
//         }
//
//         private void VisitAction(IActor action, Node baseNode)
//         {
//             if (this.closedList.Contains(action))
//             {
//                 return;
//             }
//
//             var actionCost = action.EvaluateCost();
//             var pathCost = baseNode?.cost + actionCost ?? actionCost;
//
//             if (this.openList.TryGetValue(action, out var node))
//             {
//                 if (node.cost > pathCost)
//                 {
//                     node.baseNode = baseNode;
//                     node.cost = pathCost;
//                 }
//             }
//             else
//             {
//                 var heuristic = PlannerUtils.HeuristicDistance(action, this.worldState, UNDEFINED_HEURISTIC_DISTANCE);
//                 node = new Node(action, baseNode, pathCost, heuristic);
//                 this.openList.Add(action, node);
//             }
//         }
//
//         private bool CheckFinish(out Node result)
//         {
//             var endActions = new List<Node>();
//             foreach (var (action, node) in this.openList)
//             {
//                 if (action.RequiredState.EqualsTo(this.worldState))
//                 {
//                     endActions.Add(node);
//                 }
//             }
//
//             if (endActions.Count <= 0)
//             {
//                 result = default;
//                 return false;
//             }
//
//             endActions.Sort(costComparer);
//
//             result = endActions[0];
//             return true;
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
//         private bool SelectNextAction(out Node result)
//         {
//             result = null;
//             var minWeight = int.MaxValue;
//
//             foreach (var node in this.openList.Values)
//             {
//                 var weight = node.EvaluateWeight();
//                 if (result == null || minWeight > weight)
//                 {
//                     result = node;
//                     minWeight = weight;
//                 }
//             }
//
//             return result != null;
//         }
//
//         
//
//         private sealed class Node
//         {
//             public readonly IActor action;
//             public readonly int heuristic;
//
//             public Node baseNode;
//             public int cost;
//
//             public Node(IActor action, Node baseNode, int cost, int heuristic)
//             {
//                 this.action = action;
//                 this.baseNode = baseNode;
//                 this.cost = cost;
//                 this.heuristic = heuristic;
//             }
//
//             public int EvaluateWeight()
//             {
//                 return this.cost + this.heuristic;
//             }
//         }
//
//         private sealed class CostComparer : IComparer<Node>
//         {
//             public int Compare(Node x, Node y)
//             {
//                 return x.cost.CompareTo(y.cost);
//             }
//         }
//     }
// }