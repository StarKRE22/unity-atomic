using System.Collections.Generic;

namespace Atomic
{
    public interface IAtomicObject : IAtomicEntity
    {
        void AddLogic(IAtomicLogic target);
        void RemoveLogic(IAtomicLogic target);
        
        IAtomicLogic[] AllLogic();
        IReadOnlyList<IAtomicLogic> AllLogicReadOnly();
        int AllLogicNonAlloc(IAtomicLogic[] results);
    }
}