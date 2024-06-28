// using System;
// using SampleGame;
// using UnityEngine;
//
// namespace DefaultNamespace
// {
//     public interface IGameKernel
//     {
//         void OnUpdate(float deltaTime);
//     }
//     
//     public sealed class OptimizationCameraComponent : MonoBehaviour, IGameTickable
//     {
//         private AtomicBehaviour behaviour;
//
//         private float deltaTime;
//         
//         private int counter;
//
//         private bool optimized;
//
//         private void OnBecameVisible()
//         {
//             this.optimized = false;
//         }
//
//         private void OnBecameInvisible()
//         {
//             this.optimized = true;
//         }
//
//         public void Update()
//         {
//             this.deltaTime += Time.deltaTime;
//             
//             if (this.optimized)
//             {
//                 this.counter++;
//
//                 if (counter < 5)
//                 {
//                     return;
//                 }
//             }
//
//             behaviour.OnUpdate(this.deltaTime);
//             this.deltaTime = 0;
//         }
//     }
// }