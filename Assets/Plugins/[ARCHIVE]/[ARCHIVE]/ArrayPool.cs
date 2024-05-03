// using System.Collections.Generic;
//
// namespace Atomic.Objects
// {
//     internal static class ArrayPool
//     {
//         private static readonly Queue<bool[]> typeQueue = new();
//         private static readonly Queue<object[]> dataQueue = new();
//
//         internal static bool[] TakeTypeArray()
//         {
//             if (typeQueue.TryDequeue(out bool[] result))
//             {
//                 return result;
//             }
//             
//             return new bool[AtomicSettings.Instance.TypeIndexCount];
//         }
//         
//         internal static object[] TakeDataPool()
//         {
//             if (dataQueue.TryDequeue(out object[] result))
//             {
//                 return result;
//             }
//             
//             return new object[AtomicSettings.Instance.DataIndexCount];
//         }
//
//         public static void ReleaseTypeArray(bool[] typeArray)
//         {
//             typeQueue.Enqueue(typeArray);
//         }
//
//         public static void ReleaseDataArray(object[] typeArray)
//         {
//             dataQueue.Enqueue(typeArray);
//         }
//     }
// }