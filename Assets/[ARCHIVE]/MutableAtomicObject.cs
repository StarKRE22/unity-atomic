// using System.Collections.Generic;
//
// namespace Atomic
// {
//     public class MutableAtomicObject : MutableAtomicEntity, IMutableAtomicObject
//     {
//         // [SerializeField]
//         // private LogicParameter[] initialLogics;
//         
//         
//         
//         // private protected override void Compose()
//         // {
//         //     base.Compose();
//         //     this.kernel = new AtomicKernel();
//         //
//         //     for (int i = 0, count = this.initialLogics.Length; i < count; i++)
//         //     {
//         //         LogicParameter parameter = this.initialLogics[i];
//         //         IAtomicLogic logic = parameter.provider.Provide(this);
//         //         this.kernel.AddLogic(logic);
//         //
//         //         if (!string.IsNullOrEmpty(parameter.key)  && this.keyMode != KeyMode.NONE)
//         //         {
//         //             this.dataKeys[parameter.key] = logic;
//         //         }
//         //
//         //         if (parameter.index >= 0 && this.indexMode != IndexMode.NONE)
//         //         {
//         //             this.dataIndexes[parameter.index] = logic;
//         //         }
//         //     }
//         // }
//         
// //         public interface ILogicProvider
// //         {
// //             IAtomicLogic Provide(IAtomicObject entity);
// //         }
// //
// //         [Serializable]
// //         private sealed class LogicParameter
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
// //             public ILogicProvider provider = default;
// //         }
//         
//         public IAtomicLogic[] GetAllLogic()
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public IReadOnlyList<IAtomicLogic> GetAllLogicReadOnly()
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public int GetAllLogicNonAlloc(IAtomicLogic[] results)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void AddLogic(IAtomicLogic target)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void RemoveLogic(IAtomicLogic target)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }


//
// public struct Bundle
// {
//     private readonly ISetProvider<string> typeKeys;
//     private readonly ISetProvider<int> typeIndexes;
//     private readonly IMapProvider<string, object> dataKeys;
//     private readonly IMapProvider<int, object> dataIndexes;
//
//     internal Bundle(
//         ISetProvider<string> typeKeys,
//         ISetProvider<int> typeIndexes,
//         IMapProvider<string, object> dataKeys,
//         IMapProvider<int, object> dataIndexes
//     )
//     {
//         this.typeIndexes = typeIndexes;
//         this.typeKeys = typeKeys;
//         this.dataKeys = dataKeys;
//         this.dataIndexes = dataIndexes;
//     }
//
//     public void AddType(int index)
//     {
//         this.typeIndexes?.Add(index);
//     }
//     
//     public void AddType(string key)
//     {
//         this.typeKeys?.Add(key);
//     }
//
//     public void AddValue(int index, object value)
//     {
//         this.dataIndexes.Add(index, value);
//     }
//     
//     public void AddValue(string key, object value)
//     {
//         this.dataKeys.Add(index, value)
//
//     }
// }