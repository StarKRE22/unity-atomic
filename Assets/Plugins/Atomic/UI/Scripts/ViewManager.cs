using System.Collections.Generic;

namespace Atomic.UI
{
    internal sealed class ViewUpdateManager
    {
        private static ViewUpdateManager _instance;
        
        
        
        public static void AddBehaviours(IEnumerable<IUpdateBehaviour> handlers)
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IShowBehaviour showBehaviour)
                {
                    showBehaviour.Show(this);
                }
            }
        }
        
        public static void AddBehaviour(IUpdateBehaviour behaviour)
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IShowBehaviour showBehaviour)
                {
                    showBehaviour.Show(this);
                }
            }
        }

        public static void RemoveBehaviours(IEnumerable<IBehaviour> handlers)
        {
            foreach (IBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IHideBehaviour hideBehaviour)
                {
                    hideBehaviour.Hide(this);
                }
            }
        }

        public static void RemoveBehaviour(IUpdateBehaviour updateBehaviour)
        {
            throw new System.NotImplementedException();
        }
    }
}