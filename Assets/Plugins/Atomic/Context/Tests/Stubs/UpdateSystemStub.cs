namespace Atomic.Contexts
{
    public sealed class UpdateSystemStub : IUpdateSystem
    {
        public bool updated;

        public void Update(IContext context, float deltaTime)
        {
            this.updated = true;
        }
    }
}