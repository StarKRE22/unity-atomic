using System;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class EnemyPirateVisual : MonoBehaviour, IAtomicObject.IComposable
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        [Reference(VisualAPI.Animator)]
        public Animator animator;

        [Reference(VisualAPI.SpriteRenderer)]
        public SpriteRenderer spriteRenderer;

        [SerializeField]
        private EnemyPirate pirate;

        [Behaviour]
        [SerializeField]
        private MoveAnimMechanics moveMechanics;

        public void Compose(IAtomicObject obj)
        {
            this.moveMechanics.Compose(this.animator, pirate.moveComponent.IsMoving, IsMoving);
        }
    }
}