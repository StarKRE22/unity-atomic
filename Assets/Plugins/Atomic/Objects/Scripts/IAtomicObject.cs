namespace Atomic.Objects
{
    public interface IAtomicObject
    {
        bool HasTag(int tag);
        bool AddTag(int tag);
        bool DelTag(int tag);

        T GetValue<T>(int id) where T : class;
        object GetValue(int id);
        
        bool TryGetValue<T>(int id, out T value) where T : class;
        bool TryGetValue(int id, out object value);

        bool AddValue(int id, object value);
        void SetValue(int id, object value);
        bool DelValue(int id);
        bool DelValue(int id, out object removed);
        bool HasValue(int id);

        bool AddLogic(ILogic logic);
        bool AddLogic<T>() where T : ILogic, new();
        bool DelLogic(ILogic logic);
        bool DelLogic<T>() where T : ILogic;
        bool HasLogic(ILogic logic);
    }
}