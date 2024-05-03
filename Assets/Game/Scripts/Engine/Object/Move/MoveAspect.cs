using Atomic;

namespace Game.Engine
{
    public static class MoveAspect
    {
        public static void Compose(IAtomicObject entity, float speed = 0)
        {
            var moveDirection = new AtomicVariable<float>();
            var moveEnabled = new AtomicAnd();
            
            entity.AddField(MovementAPI.BaseMoveSpeed, new AtomicValue<float>(speed));
            entity.AddField(MovementAPI.MoveDirection, moveDirection);
            entity.AddField(MovementAPI.MoveEnabled, moveEnabled);
            entity.AddField(MovementAPI.IsMoving, new AtomicFunction<bool>(
                () => moveDirection.Value != 0 && moveEnabled.Invoke()));
            
            entity.AddLogic(new MovementMechanicsDynamic(entity));
        }

        public static void Dispose(IAtomicObject entity)
        {
            entity.RemoveField(MovementAPI.BaseMoveSpeed);
            entity.RemoveField(MovementAPI.MoveDirection);
            entity.RemoveField(MovementAPI.MoveEnabled);
            entity.RemoveField(MovementAPI.IsMoving);
            
            entity.RemoveLogic<MovementMechanicsDynamic>();
        }
    }
}