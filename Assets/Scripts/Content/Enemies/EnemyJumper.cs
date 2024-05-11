using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class EnemyJumper : MonoBehaviour, IAtomicObject.IFixedUpdate
    {
        [Reference(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody => this.GetComponent<Rigidbody2D>();

        [Reference(CommonAPI.Transform)]
        public Transform Transform => this.transform;
        
        [Reference(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Reference(CommonAPI.JumpComponent)]
        public JumpComponent jumpComponent;
        
        [Behaviour]
        public GroundedComponent groundedComponent;

        [Behaviour]
        private KillCharacterMechanics killMechanics = new();

        [SerializeField]
        private bool aiEnabled = true;
        
        public void OnFixedUpdate(IAtomicObject obj, float deltaTime)
        {
            if (this.aiEnabled)
            {
                if (this.groundedComponent.isGrounded.Value)
                {
                    this.jumpComponent.JumpAction.Invoke();
                }
            }
        }
    }
}