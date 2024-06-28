// using UnityEngine;
// using Zenject;
//
// namespace SampleGame
// {
//     public sealed class PlayerInstaller : MonoInstaller
//     {
//         [SerializeField]
//         private Character characterPrefab;
//         
//         [SerializeField]
//         private CameraConfig cameraConfig;
//
//         [SerializeField]
//         private new Camera camera;
//
//         [SerializeField]
//         private string characterName;
//
//         [SerializeField]
//         private Transform spawnPoint;
//
//         [SerializeField]
//         private Transform worldTransform;
//
//    
//
//         private SignalBus signalBus;
//         
//         public override void InstallBindings()
//         {
//            
//             this.Container
//                 .Bind<ICharacter>()
//                 .FromComponentInNewPrefab(this.characterPrefab, new GameObjectCreationParameters
//                 {
//                     Name = this.characterName,
//                     ParentTransform = this.worldTransform,
//                     Position = this.spawnPoint.position,
//                     Rotation = this.spawnPoint.rotation
//                 })
//                 .AsSingle()
//                 .NonLazy();
//
//             this.Container
//                 .BindInterfacesTo<CharacterMoveSystem>()
//                 .AsCached()
//                 .NonLazy();
//
//
//             this.Container
//                 .BindInterfacesTo<CharacterDeathSystem>()
//                 .AsCached();
//
//             this.Container
//                 .BindInterfacesTo<MoveInput>()
//                 .AsSingle()
//                 .WithArguments(this.inputConfig);
//
//             this.Container
//                 .Bind<Camera>()
//                 .FromInstance(this.camera);
//             
//             this.Container
//                 .BindInterfacesTo<CameraFollowSystem>()
//                 .AsCached()
//                 .WithArguments(this.cameraConfig.cameraOffset);
//             
//             //Use signals:
//             // this.Container
//             //     .BindSignal<GameStartEvent>()
//             //     .ToMethod<IGameStartListener>(it => it.OnStartGame)
//             //     .FromResolveAll();
//             //
//             // this.Container
//             //     .BindSignal<GameFinishEvent>()
//             //     .ToMethod<IGameFinishListener>(it => it.OnFinishGame)
//             //     .FromResolveAll();
//
//         }
//     }
// }
//
//
// // this.Container
// //     .BindInterfaces<>()
// //     .OfStateMachine()
// //     .To<GamePlayingState>()
// //     .FromResolve<MoveController>();