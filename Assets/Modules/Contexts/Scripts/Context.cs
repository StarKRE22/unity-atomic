namespace Modules.Contexts
{
    public sealed class Context : IContext
    {
        public bool AddData(int key, object value)
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