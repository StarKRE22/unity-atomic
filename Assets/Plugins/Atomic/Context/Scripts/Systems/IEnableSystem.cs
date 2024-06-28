namespace Atomic.Contexts
{
    public interface IEnableSystem : ISystem
    {
        void Enable(IContext context);
    }
}