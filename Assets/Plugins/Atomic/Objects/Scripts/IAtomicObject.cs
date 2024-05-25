using JetBrains.Annotations;

namespace Atomic.Objects
{
    public interface IAtomicObject
    {
        bool HasTag(int tag);
        bool AddTag(int tag);
        bool DelTag(int tag);

        [CanBeNull]
        T GetValue<T>(int id) where T : class;

        [CanBeNull]
        object GetValue(int id);

        bool TryGetValue<T>(int id, out T value) where T : class;
        bool TryGetValue(int id, out object value);

        bool AddValue(int id, object value);
        void SetValue(int id, object value);
        bool DelValue(int id);
        bool DelValue(int id, out object removed);

        bool AddLogic(IAtomicLogic logic);
        bool AddLogic<T>() where T : IAtomicLogic, new();
        bool DelLogic(IAtomicLogic logic);
        bool DelLogic<T>() where T : IAtomicLogic;
    }
}