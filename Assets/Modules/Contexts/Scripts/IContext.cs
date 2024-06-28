namespace Modules.Contexts
{
    public interface IContext
    {
        bool AddData(int key, object value);
        T GetData<T>(int moveInput) where T : class;
        
        bool AddSystem(ISystem logic);
        bool AddSystem<T>() where T : ISystem;
    }
}