namespace Atomic.Elements
{
    public delegate void ActionOut<O>(out O result);
    public delegate void ActionOut<in T, O>(T arg, out O result);
    public delegate void ActionOut<in T1, in T2, O>(T1 arg1, T2 arg2, out O result);
}