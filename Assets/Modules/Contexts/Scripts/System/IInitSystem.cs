namespace Modules.Contexts
{
    public interface IInitSystem : ISystem
    {
        void Init(IContext context);
    }
}