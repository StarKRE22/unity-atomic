namespace Atomic.Contexts
{
    public interface IInitSystem : ISystem
    {
        void Init(IContext context);
    }
}