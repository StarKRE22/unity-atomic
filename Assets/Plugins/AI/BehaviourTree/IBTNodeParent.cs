namespace AIModule
{
    public interface IBTNodeParent
    {
        bool FindChild(string name, out BTNode result);
    }
}