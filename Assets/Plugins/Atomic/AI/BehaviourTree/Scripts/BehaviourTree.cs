namespace Modules.AI
{
    public sealed class BehaviourTree : IBehaviourTree
    {
        private BTNode root;
        private IBlackboard blackboard;

        public BehaviourTree(BTNode root, IBlackboard blackboard)
        {
            this.root = root;
            this.blackboard = blackboard;
        }

        public void SetBlackboard(IBlackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public void SetRoot(BTNode root)
        {
            this.root = root;
        }

        public void OnUpdate(float deltaTime)
        {
            this.root?.Run(this.blackboard, deltaTime);
        }

        public void Abort()
        {
            this.root?.Abort(this.blackboard);
        }

        public bool FindChild(string name, out BTNode result)
        {
            if (string.IsNullOrEmpty(name))
            {
                result = default;
                return false;
            }

            if (this.root == null)
            {
                result = default;
                return false;
            }

            if (name.Equals(this.root.Name))
            {
                result = this.root;
                return true;
            }

            if (this.root is not IBTNodeParent parent)
            {
                result = default;
                return false;
            }

            return parent.FindChild(name, out result);
        }
    }
}