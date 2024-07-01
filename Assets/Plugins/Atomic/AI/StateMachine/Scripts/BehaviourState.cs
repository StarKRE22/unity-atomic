namespace Modules.AI
{
    public sealed class BehaviourState
    {
        public IState CurrentState => this.state;
        
        private readonly IBlackboard blackboard;
        private IState state;

        public BehaviourState(IBlackboard blackboard, IState state)
        {
            this.blackboard = blackboard;
            this.state = state;
        }

        public void SwitchState(IState state)
        {
            this.state?.OnExit(this.blackboard);
            this.state = state;
            this.state?.OnEnter(this.blackboard);
        }

        public void OnEnter()
        {
            this.state?.OnEnter(this.blackboard);
        }

        public void OnExit()
        {
            this.state?.OnExit(this.blackboard);
        }

        public void OnUpdate(float deltaTime)
        {
            this.state?.OnUpdate(this.blackboard, deltaTime);
        }
    }
}