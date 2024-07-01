using System.Collections.Generic;
using UnityEngine;

namespace Modules.AI
{
    public sealed class BehaviourGroup
    {
        private readonly IBlackboard blackboard;
        private bool started;
        
        private readonly List<IBehaviour> behaviours = new();
        private readonly List<IUpdateBehaviour> updateBehaviours = new();
        private readonly List<IUpdateBehaviour> _updateCache = new();

        public bool IsStarted => this.started;
        public IReadOnlyList<IBehaviour> AllBehaviours => this.behaviours;
        
        public BehaviourGroup(IBlackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public void OnStart()
        {
            if (this.started)
            {
                return;
            }

            this.started = true;
            
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IStartBehaviour behaviour)
                {
                    behaviour.OnStart(this.blackboard);
                }
            }
        }

        public void OnStop()
        {
            if (!this.started)
            {
                return;
            }
            
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IStopBehaviour behaviour)
                {
                    behaviour.OnStop(this.blackboard);
                }
            }
            
            this.started = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!this.started)
            {
                Debug.LogWarning("Behaviour System is not started!");
                return;
            }
            
            if (this.updateBehaviours.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updateBehaviours);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IUpdateBehaviour logic = _updateCache[i];
                logic.OnUpdate(this.blackboard, deltaTime);
            }
        }
        
        public bool AddBehaviour(IBehaviour target)
        {
            if (target == null)
            {
                return false;
            }

            if (this.behaviours.Contains(target))
            {
                return false;
            }

            this.behaviours.Add(target);

            if (this.started && target is IStartBehaviour behaviour)
            {
                behaviour.OnStart(this.blackboard);
            }
            
            if (target is IUpdateBehaviour updateBehaviour)
            {
                this.updateBehaviours.Add(updateBehaviour);
            }
            
            return true;
        }

        public bool DelBehaviour(IBehaviour target)
        {
            if (target == null)
            {
                return false;
            }

            if (!this.behaviours.Remove(target))
            {
                return false;
            }
            
            if (target is IUpdateBehaviour updateBehaviour)
            {
                this.updateBehaviours.Remove(updateBehaviour);
            }
            
            if (this.started && target is IStopBehaviour behaviour)
            {
                behaviour.OnStop(this.blackboard);
            }
            
            return true;
        }
    }
}