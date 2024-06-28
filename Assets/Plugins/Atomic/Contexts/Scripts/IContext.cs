namespace Atomic.Contexts
{
    public interface IContext
    {
        bool AddData(int key, object value);
        T GetData<T>(int key) where T : class;
        
        bool AddSystem(ISystem system);
        bool AddSystem<T>() where T : ISystem;
    }
}