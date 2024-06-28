using UnityEngine;

namespace Atomic.Contexts
{
    public sealed class SceneContext : MonoBehaviour, IContext
    {
        private Context context;

        public bool AddData(int key, object value)
        {
            return context.AddData(key, value);
        }

        public T GetData<T>(int key) where T : class
        {
            return context.GetData<T>(key);
        }

        public bool AddSystem(ISystem system)
        {
            return context.AddSystem(system);
        }

        public bool AddSystem<T>() where T : ISystem
        {
            return context.AddSystem<T>();
        }

        public bool AddSystem(int key, ISystem logic)
        {
            return context.AddSystem(key, logic);
        }

        public void AddComponent(int key, ISystem component)
        {
            context.AddComponent(key, component);
        }
    }
}