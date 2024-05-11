using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class CharacterVisual : MonoBehaviour, IAtomicObject.IComposable
    {
        #region Interface

        [Reference(VisualAPI.Animator)]
        public Animator Animator => this.animator;

        [Reference(VisualAPI.SpriteRenderer)]
        public SpriteRenderer SpriteRenderer => this.spriteRenderer;

        #endregion

        #region Core

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Jump = Animator.StringToHash("Jump");

        [SerializeField]
        private Character character;

        [Space]
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        [Behaviour]
        private MoveAnimMechanics moveMechanics;
        
        [SerializeField]
        [Behaviour]
        private JumpAnimMechanics jumpMechanics;

        [SerializeField]
        [Behaviour]
        private FlyAnimMechanics flyMechanics;

        public void Compose(IAtomicObject obj)
        {
            this.moveMechanics.Compose(this.animator, this.character.isGroundMoving, IsMoving);
            this.jumpMechanics.Compose(this.animator, this.character.jumpComponent.JumpEvent, Jump);
            this.flyMechanics.Compose(this.animator, this.character.verticalSpeed);
        }
        
        #endregion
    }
}