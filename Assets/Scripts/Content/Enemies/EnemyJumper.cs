using Atomic.Objects;
using GameEngine;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Sample
{
    public sealed class EnemyJumper : MonoBehaviour, IFixedUpdate
    {
        [Value(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody => this.GetComponent<Rigidbody2D>();

        [Value(CommonAPI.Transform)]
        public Transform Transform => this.transform;
        
        [Value(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Value(CommonAPI.JumpComponent)]
        public JumpComponent jumpComponent;
        
        [Logic]
        public GroundedComponent groundedComponent;

        [Logic]
        private KillCharacterMechanics killMechanics = new();

        [SerializeField]
        private bool aiEnabled = true;
        
        public void OnFixedUpdate(IObject obj, float deltaTime)
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