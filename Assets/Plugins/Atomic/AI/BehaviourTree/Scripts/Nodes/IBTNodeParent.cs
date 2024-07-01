namespace Modules.AI
{
    public interface IBTNodeParent
    {
        public bool FindChild(string name, out BTNode result);
    }
}