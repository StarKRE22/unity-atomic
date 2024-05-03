namespace Atomic
{
    public interface IAtomicValuePool
    {
        object this[int index] { get; set; }

        object Get(int index);
        void Set(int entity, object value);
        
        bool Put(int entity, object value);
        bool Del(int entity);
        bool Has(int entity);

        object[] All();
    }
}