namespace Modules.AI
{
    public interface IBehaviourTree
    {
        void SetRoot(BTNode root);
        void Abort();
        bool FindChild(string name, out BTNode result);
    }
}