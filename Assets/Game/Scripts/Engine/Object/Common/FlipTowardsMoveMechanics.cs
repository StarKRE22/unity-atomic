using Atomic;

namespace Game.Content
{
    public sealed class FlipTowardsMoveMechanics : IAtomicFixedUpdate
    {
        private readonly IAtomicValue<float> moveDirection;
        private readonly IAtomicSetter<float> flipDirection;

        public FlipTowardsMoveMechanics(IAtomicValue<float> moveDirection, IAtomicSetter<float> flipDirection)
        {
            this.moveDirection = moveDirection;
            this.flipDirection = flipDirection;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            float moveDirection = this.moveDirection.Value;
            if (moveDirection != 0)
            {
                this.flipDirection.Value = moveDirection;
            }
        }
    }
}