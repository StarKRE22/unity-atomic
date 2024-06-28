namespace Atomic.Contexts
{
    public enum ContextState : byte
    {
        OFF = 0,
        INITIALIZED = 1,
        ENABLED = 2,
        DISABLED = 3,
        DESTROYED = 4
    }
}