using UnityEngine;

namespace Modules.Contexts
{
    public sealed class SceneContext : MonoBehaviour, IContext
    {
        private Context context;
        public bool AddData(int key, object value)
        {
            return context.AddData(key, value);
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