namespace Modules.Contexts
{
    public interface IDisableSystem : ISystem
    {
        void Disable(IContext context);
    }
}