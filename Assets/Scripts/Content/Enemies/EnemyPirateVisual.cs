using System;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class EnemyPirateVisual : MonoBehaviour, IObject.IComposable
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        [Value(VisualAPI.Animator)]
        public Animator animator;

        [Value(VisualAPI.SpriteRenderer)]
        public SpriteRenderer spriteRenderer;

        [SerializeField]
        private EnemyPirate pirate;

        [Logic]
        [SerializeField]
        private MoveAnimMechanics moveMechanics;

        public void Compose(IObject obj)
        {
            this.moveMechanics.Compose(this.animator, pirate.moveComponent.IsMoving, IsMoving);
        }
    }
}