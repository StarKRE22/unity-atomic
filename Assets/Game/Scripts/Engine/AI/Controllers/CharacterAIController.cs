// using AIModule;
// using Atomic;
// using UnityEngine;
//
// namespace Game.Engine
// {
//     public sealed class CharacterAIController : MonoBehaviour, 
//         IAtomicEnable,
//         IAtomicDisable,
//         IAtomicFixedUpdate
//     {
//         [SerializeField]
//         private AtomicObject character;
//
//         [SerializeField]
//         private Blackboard blackboard;
//
//         [SerializeField]
//         private AIBehaviour behaviour;
//
//         private void Awake()
//         {
//             this.blackboard.SetObject(BlackboardAPI.Character, this.character);
//         }
//
//         private void OnEnable()
//         {
//             this.character.AddLogic(this);
//         }
//
//         private void OnDisable()
//         {
//             this.character.RemoveLogic(this);
//         }
//
//         void IAtomicFixedUpdate.OnFixedUpdate(float deltaTime)
//         {
//             this.behaviour.OnUpdate(deltaTime);
//         }
//
//         void IAtomicEnable.Enable()
//         {
//             this.behaviour.OnStart();
//         }
//
//         void IAtomicDisable.Disable()
//         {
//             this.behaviour.OnStop();
//         }
//     }
// }