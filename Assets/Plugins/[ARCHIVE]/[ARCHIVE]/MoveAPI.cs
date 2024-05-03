namespace Plugins.unity_atomic.Codegen
{
    //TODO: сделать категории для API в инспекторе с блекбордой
    //TODO режимы кодогенерации: и ключи / и индексы? добавляеть кастомные префиксы или нет!
    public static class MoveAPI
    {
        public const string kMoveAction = nameof(kMoveAction);
        
        public const int MoveAction = 0; // AtomicAction<Vector3>
    }
}