// using System;
// using UnityEngine;
//
// namespace Atomic.Elements
// {
//     public interface IAtomicComponent : ISerializationCallbackReceiver, IDisposable
//     {
//         void Initialize();
//
//         void ISerializationCallbackReceiver.OnBeforeSerialize()
//         {
//         }
//
//         void ISerializationCallbackReceiver.OnAfterDeserialize()
//         {
//             this.Initialize();
//         }
//     }
// }