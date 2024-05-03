// namespace AI.GOAP
// {
//     public interface IActor
//     {
//         IFactState ResultState { get; }
//         
//         IFactState RequiredState { get; }
//
//         bool IsPlaying { get; }
//         
//         int EvaluateCost();
//
//         bool IsValid();
//
//         void Play(Callback callback);
//
//         void Cancel();
//
//         public interface Callback
//         {
//             void Invoke(IActor action, bool success);
//         }
//     }
// }