namespace Atomic
{
    public interface IAtomicEntity
    {
        T Get<T>(int index);
        bool TryGet<T>(int index, out T result);
        void Get<T>(int index, ref T value);

        bool Put<T>(int index, T value);
        void Set<T>(int index, T value);
        bool Del(int index);

        bool Is(int index);
        bool Mark(int index);
        bool Unmark(int index);
    }
}