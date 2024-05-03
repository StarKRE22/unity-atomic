// using System;
//
// namespace AI.GOAP
// {
//     public static class PlannerFactory
//     {
//         public static IPlanner CreatePlanner(PlannerMode mode)
//         {
//             if (mode == PlannerMode.Greedy)
//             {
//                 return new GreedyPlanner();
//             }
//
//             if (mode == PlannerMode.AStar)
//             {
//                 return new AStarPlanner();
//             }
//
//             if (mode == PlannerMode.Dijkstra)
//             {
//                 return new DijkstraPlanner();
//             }
//
//             throw new Exception($"Undefined planner mode {mode}!");
//         } 
//     }
// }