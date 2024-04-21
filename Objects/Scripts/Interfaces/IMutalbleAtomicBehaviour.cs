namespace Atomic.Objects
{
    public interface IMutalbleAtomicBehaviour : IAtomicBehaviour
    {
        void AddLogic(IAtomicLogic target);

        void RemoveLogic(IAtomicLogic target);
    }
}