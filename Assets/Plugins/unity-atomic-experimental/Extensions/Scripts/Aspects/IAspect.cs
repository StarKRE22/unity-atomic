using Atomic.Objects;

namespace Atomic.Extensions
{
    public interface IAspect
    {
        void Apply(IObject obj);
        void Discard(IObject obj);
    }
}