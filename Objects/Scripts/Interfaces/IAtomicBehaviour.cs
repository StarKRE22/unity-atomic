using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IAtomicBehaviour
    {
        IAtomicLogic[] AllLogic();

        int AllLogicNonAlloc(IAtomicLogic[] results);

        IList<IAtomicLogic> AllLogicUnsafe();
    }
}