// using System;
// using System.Collections.Generic;
// using SampleGame;
//
// namespace Plugins
// {
//     public interface IGameState
//     {
//         event Action<float> OnUpdated;
//         
//         void OnEnter();
//         void OnUpdate(float deltaTime);
//         void OnExit();
//     }
//
//     public sealed class GameStateMachine
//     {
//         private Dictionary<Type, IGameState> states;
//
//         public void SwitchState<T>() where T : IGameState
//         {
//         }
//     }
//     
//     
//     //OCP
//     public sealed class GamePlayingState : IGameState
//     {
//         // private MoveInput moveInput;
//         // private CameraFollower cameraFollower;
//         /// EnemyManager enemyManager;
//
//         private List<IGameTickable> tickables;
//
//         public void OnEnter()
//         {
//             
//         }
//
//         public void OnUpdate(float deltaTime)
//         {
//             foreach (var tickable in this.tickables)
//             {
//                 tickable.Tick(deltaTime);
//             }
//         }
//
//         public void OnExit()
//         {
//         }
//     }
//     
//     
//     public sealed class GamePauseState : IGameState
//     {
//         private List<IGameTickable> tickables;
//         
//         // private MoveInput moveInput;
//         // private CameraFollower cameraFollower;
//         /// EnemyManager enemyManager;
//         
//         
//         
//         public void OnEnter()
//         {
//             
//         }
//
//         public void OnUpdate(float deltaTime)
//         {
//             this.moveInput.Tick(deltaTime);
//             this.cameraFollower.LateTick(deltaTime);
//             //EnemyManager enemyManager;
//         }
//
//         public void OnExit()
//         {
//         }
//     }
// }