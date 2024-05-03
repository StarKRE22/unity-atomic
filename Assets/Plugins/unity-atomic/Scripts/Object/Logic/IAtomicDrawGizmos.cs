#if UNITY_EDITOR
namespace Atomic
{
    //Don't forget wrap #if UNITY_EDITOR!
    public interface IAtomicDrawGizmos
    {
        void OnGizmosDraw();
    }
}
#endif