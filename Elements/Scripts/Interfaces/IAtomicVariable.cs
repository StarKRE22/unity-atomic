namespace Atomic.Elements
{
    public interface IAtomicVariable<T> : IAtomicValue<T>, IAtomicSetter<T>
    {
        new T Value { get; set; }

        T IAtomicValue<T>.Value
        {
            get { return this.Value; }
        }

        T IAtomicSetter<T>.Value
        {
            set => this.Value = value;
        }
    }
}