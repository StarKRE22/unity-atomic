namespace Atomic.Contexts
{
    public interface IDisableSystem : ISystem
    {
        void Disable(IContext context);
    }
}