using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class ObjectTest : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;

        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        private JumpComponent jumpComponent;
        
        private void Start()
        {
            //Накатываем компоненты
            this.character.AddElement(CommonAPI.MoveComponent, moveComponent);
            this.character.AddReference(CommonAPI.JumpComponent, jumpComponent);
            
            //Откатываем компоненты
            this.character.DelElement(CommonAPI.MoveComponent);
            this.character.DelReference(CommonAPI.JumpComponent);
        }

        //
        // [Button]
        // public void AddJumpAspect()
        // {
        //     JumpAspect.Compose(this.character, 5);
        // }
        //
        // [Button]
        // public void RemoveJumpAspect()
        // {
        //     JumpAspect.Dispose(this.character);
        // }
        //
        // [Button]
        // public void AddMoveComponent()
        // {
        //
        //
        //
        //     this.character.MoveAPI = ;
        //     this.character.AddMechanics(moveComponent);
        //     
        //     foreach (var character in FindObjectsOfType<JumpingEnemy>())
        //     {
        //         MoveAspect.Compose(character, 5);
        //     }
        // }
        //
        // [Button]
        // public void RemoveMoveComponent()
        // {
        //     foreach (var character in FindObjectsOfType<JumpingEnemy>())
        //     {
        //         MoveAspect.Dispose(character);
        //     }
        // }
        //
        // [Button]
        // public void AddFlipMechanics()
        // {
        //     this.character.AddLogic(new FlipMechanicsDynamic(this.character));
        // }
        //
        // [Button]
        // public void RemoveFlipMechanics()
        // {
        //     this.character.RemoveLogic<FlipMechanicsDynamic>(); //O(N)
        // }
    }
}

// IAtomicValue<float> moveDirection = this.character.GetValue<float>(ObjectAPI.MoveDirection);
// Transform transform = this.character.Get<Transform>(ObjectAPI.Transform);
//
// if (moveDirection == null || transform == null)
// {
//     return;
// }