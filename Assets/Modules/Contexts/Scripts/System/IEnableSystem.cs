namespace Modules.Contexts
{
    public interface IEnableSystem : ISystem
    {
        void Enable(IContext context);
    }
}