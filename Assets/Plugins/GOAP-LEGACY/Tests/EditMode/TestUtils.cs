// using System.Collections.Generic;
//
// namespace AI.GOAP
// {
//     public static class TestUtils
//     {
//         public static bool EqualsPlans(IActor[] expected, IActor[] actual)
//         {
//             if (expected.Length != actual.Length)
//             {
//                 return false;
//             }
//
//             for (int i = 0, count = expected.Length; i < count; i++)
//             {
//                 if (expected[i] != actual[i])
//                 {
//                     return false;
//                 }
//             }
//
//             return true;
//         }
//         
//         public static bool EqualsPlans(List<IActor> expected, List<IActor> actual)
//         {
//             if (expected.Count != actual.Count)
//             {
//                 return false;
//             }
//
//             for (int i = 0, count = expected.Count; i < count; i++)
//             {
//                 if (expected[i] != actual[i])
//                 {
//                     return false;
//                 }
//             }
//
//             return true;
//         }
//     }
// }