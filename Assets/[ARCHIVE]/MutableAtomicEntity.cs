// using System.Collections.Generic;
//
// namespace Atomic
// {
//     public class MutableAtomicEntity : AtomicEntity, IMutableAtomicEntity
//     {
//         
//         // [Title("Initial Data")]
//         // [HideIf(nameof(indexMode), IndexMode.NONE)]
//         // [SerializeField, Space]
//         // private TypeKeyParameter[] initialTypeKeys;
//         //
//         // [HideIf(nameof(indexMode), IndexMode.NONE)]
//         // [SerializeField, Space]
//         // private TypeIndexParameter[] initialTypeIndexes;
//         //
//         // [HideIf(nameof(indexMode), IndexMode.NONE)]
//         // [SerializeField]
//         // private ValueIndexParameter[] initialValueIndexes;
//         
// //         
// //         private void ComposeSerialized()
// //         {
// //             if (this.keyMode != KeyMode.NONE)
// //             {
// //                 //Setup types:
// //                 for (int i = 0, count = this.initialTypeIndexes.Length; i < count; i++)
// //                 {
// //                     TypeIndexParameter typeIndexParameter = this.initialTypeIndexes[i];
// //                     string type = typeIndexParameter.key;
// //                     if (!string.IsNullOrEmpty(type))
// //                     {
// //                         this.typeKeys.Add(type);
// //                     }
// //                 }
// //
// //                 //Setup: values:
// //                 for (int i = 0, count = this.initialValueIndexes.Length; i < count; i++)
// //                 {
// //                     ValueIndexParameter valueIndexParameter = this.initialValueIndexes[i];
// //                     object value = valueIndexParameter.provider.Provide(this);
// //                     string key = valueIndexParameter.key;
// //                     if (!string.IsNullOrEmpty(key))
// //                     {
// //                         this.dataKeys[key] = value;
// //                     }
// //                 }
// //             }
// //
// //             if (this.indexMode != IndexMode.NONE)
// //             {
// //                 //Setup types:
// //                 for (int i = 0, count = this.initialTypeIndexes.Length; i < count; i++)
// //                 {
// //                     TypeIndexParameter typeIndexParameter = this.initialTypeIndexes[i];
// //                     int typeIndex = typeIndexParameter.index;
// //                     if (typeIndex >= 0)
// //                     {
// //                         this.typeIndexes.Add(typeIndex);
// //                     }
// //                 }
// //
// //                 //Setup: values:
// //                 for (int i = 0, count = this.initialValueIndexes.Length; i < count; i++)
// //                 {
// //                     ValueIndexParameter valueIndexParameter = this.initialValueIndexes[i];
// //                     object value = valueIndexParameter.provider.Provide(this);
// //                     int index = valueIndexParameter.index;
// //                     if (index >= 0)
// //                     {
// //                         this.dataIndexes[index] = value;
// //                     }
// //                 }
// //             }
// //         }
// //         
// //         [Serializable]
// //         private sealed class TypeKeyParameter
// //         {
// //             
// //         }
// //         
// //         [Serializable]
// //         private sealed class ValueKeyParameter
// //         {
// //             
// //         }
// //         
// //         [Serializable]
// //         private sealed class TypeIndexParameter
// //         {
// //             [SerializeField]
// //             private IdMode mode = IdMode.KEY;
// //             
// // #if UNITY_CONTRACTS
// //             [ContractType]
// // #endif
// //             [ShowIf(nameof(mode), IdMode.KEY)]
// //             public string key = "";
// //             
// // #if UNITY_CONTRACTS
// //             [ContractType]
// // #endif
// //             [ShowIf(nameof(mode), IdMode.INDEX)]
// //             public int index = -1;
// //         }
// //
// //        
// //         
// //         [Serializable]
// //         private sealed class ValueIndexParameter
// //         {
// //             [SerializeField]
// //             private IdMode mode = IdMode.KEY;
// //             
// // #if UNITY_CONTRACTS
// //             [ContractValue]
// // #endif
// //             [ShowIf(nameof(mode), IdMode.KEY)]
// //             public string key = "";
// //             
// // #if UNITY_CONTRACTS
// //             [ContractValue]
// // #endif
// //             [ShowIf(nameof(mode), IdMode.INDEX)]
// //             public int index = -1;
// //
// //             [SerializeReference]
// //             public IValueProvider provider = default;
// //         }
//     }
// }