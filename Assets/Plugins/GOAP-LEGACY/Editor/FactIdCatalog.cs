// #if UNITY_EDITOR
// using UnityEngine;
//
// namespace AI.GOAP.UnityEditor
// {
//     [CreateAssetMenu(
//         fileName = "FactIdCatalog",
//         menuName = "AI/GOAP/FactIdCatalog"
//     )]
//     public sealed class FactIdCatalog : ScriptableObject
//     {
//         [SerializeField]
//         private string[] names;
//
//         private static FactIdCatalog instance;
//
//         public static string[] GetIds()
//         {
//             if (instance == null)
//             {
//                 instance = Resources.Load<FactIdCatalog>(nameof(FactIdCatalog));
//             }
//
//             return instance.names;
//         }
//     }
// }
// #endif