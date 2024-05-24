using Atomic;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class CharacterSystem : MonoBehaviour
    {
        [SerializeField]
        private MonoObject[] characters;

        private MoveController moveController;
        private JumpController jumpController;

        private void Start()
        {
            this.moveController = new MoveController(new AtomicSetter<float>(value =>
            {
                foreach (MonoObject character in this.characters)
                {
                    IAtomicVariable<float> currentDirection = character.GetMoveComponent()?.CurrentDirection;
                    
                    if (currentDirection != null)
                    {
                        currentDirection.Value = value;
                    }
                }
            }));

            this.jumpController = new JumpController(new AtomicAction(() =>
            {
                foreach (MonoObject character in this.characters)
                {
                    character.GetJumpComponent()?.JumpAction?.Invoke();
                }
            }));
        }

        private void Update()
        {
            this.moveController.Update();
            this.jumpController.Update();
        }
    }
}