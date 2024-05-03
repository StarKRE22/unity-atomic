using System.Runtime.CompilerServices;
using Atomic;

namespace Game.Engine
{
    public static class JumpAspect 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Compose(IAtomicObject target, float force, params IAtomicValue<bool>[] conditions)
        {
            target.AddType(TypeAPI.Jumpable);
            target.AddField(JumpAPI.JumpEnabled, new AtomicAnd(conditions));
            target.AddField(JumpAPI.BaseJumpForce, new AtomicValue<float>(force));
            target.AddField(JumpAPI.JumpAction, new JumpActionDynamic(target));
            target.AddField(JumpAPI.JumpEvent, new AtomicEvent());
        }

        public static void Dispose(IAtomicObject target)
        {
            target.RemoveType(TypeAPI.Jumpable);
            target.RemoveField(JumpAPI.JumpEnabled);
            target.RemoveField(JumpAPI.BaseJumpForce);
            target.RemoveField(JumpAPI.JumpAction);
            target.RemoveField(JumpAPI.JumpEvent);
        }
    }
}