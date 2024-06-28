namespace Atomic.Contexts
{
    public sealed class Context : IContext
    {
        public bool AddData(int key, object value)
        {
            throw new System.NotImplementedException();
        }

        public T GetData<T>(int key) where T : class
        {
            throw new System.NotImplementedException();
        }

        public bool AddSystem(ISystem system)
        {
            throw new System.NotImplementedException();
        }

        public bool AddSystem<T>() where T : ISystem
        {
            throw new System.NotImplementedException();
        }

        public bool AddSystem(int key, ISystem logic)
        {
            throw new System.NotImplementedException();
        }

        public void AddComponent(int key, ISystem obj)
        {
            throw new System.NotImplementedException();
        }
    }
}