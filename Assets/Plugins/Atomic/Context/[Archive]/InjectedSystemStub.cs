// namespace Atomic.Contexts
// {
//     public sealed class InjectedSystemStub : ISystem
//     {
//         [Inject(0)]
//         public string character;
//
//         [Inject(1)]
//         public string Enemy { get; set; }
//         
//         public string npc;
//         
//         [Construct]
//         public void Construct([Inject(3)] string npc)
//         {
//             this.npc = npc;
//         }
//     }
// }