// using System.Collections.Generic;
//
// namespace AI.GOAP
// {
//     public static class PlannerUtils
//     {
//         public static bool MatchesAction(IActor action, IFactState goal, IFactState worldState)
//         {
//             foreach (var (conditionName, conditionValue) in goal)
//             {
//                 if (worldState.TryGetFact(conditionName, out var worldValue) &&
//                     worldValue == conditionValue)
//                 {
//                     continue;
//                 }
//
//                 if (!action.ResultState.TryGetFact(conditionName, out var actionValue) ||
//                     actionValue != conditionValue)
//                 {
//                     return false;
//                 }
//             }
//
//             return true;
//         }
//
//         public static int HeuristicDistance(IActor action, IFactState worldState, int defaultDistance)
//         {
//             return PlannerUtils.HeuristicDistance(action.RequiredState, worldState, defaultDistance);
//         }
//
//         public static int HeuristicDistance(IFactState requiredState, IFactState worldState, int defaultDistance)
//         {
//             var result = 0;
//
//             foreach (var (requiredCond, requiredValue) in requiredState)
//             {
//                 if (worldState.TryGetFact(requiredCond, out var currentValue))
//                 {
//                     if (requiredValue != currentValue)
//                     {
//                         result++;
//                     }
//                 }
//                 else
//                 {
//                     result += defaultDistance;
//                 }
//             }
//
//             return result;
//         }
//         
//         public static Dictionary<IActor, List<IActor>> CreateActionGraph(IActor[] actions, IFactState worldState)
//         {
//             var graph = new Dictionary<IActor, List<IActor>>();
//
//             var count = actions.Length;
//             for (var i = 0; i < count; i++)
//             {
//                 var action = actions[i];
//
//                 for (var j = 0; j < count; j++)
//                 {
//                     var other = actions[j];
//                     if (other == action)
//                     {
//                         continue;
//                     }
//
//                     var requiredState = action.RequiredState;
//                     if (!PlannerUtils.MatchesAction(other, requiredState, worldState))
//                     {
//                         continue;
//                     }
//
//                     if (!graph.TryGetValue(action, out var neighbours))
//                     {
//                         neighbours = new List<IActor>();
//                         graph.Add(action, neighbours);
//                     }
//
//                     neighbours.Add(other);
//                 }
//             }
//
//             return graph;
//         }
//     }
// }