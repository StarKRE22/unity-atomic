namespace Modules.AI
{
    public interface IState : IStartBehaviour, IUpdateBehaviour, IStopBehaviour
    {
        public string Name { get; }

        void OnEnter(IBlackboard blackboard);
        void OnExit(IBlackboard blackboard);

        void IStartBehaviour.OnStart(IBlackboard blackboard) => this.OnEnter(blackboard);
        void IStopBehaviour.OnStop(IBlackboard blackboard) => this.OnExit(blackboard);
    }
}