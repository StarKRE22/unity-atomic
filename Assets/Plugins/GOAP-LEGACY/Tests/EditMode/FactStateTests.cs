// using NUnit.Framework;
//
// namespace AI.GOAP
// {
//     public sealed class FactStateTests
//     {
//         [Test]
//         public void EqualsStatesWithSameVariablesAndValues()
//         {
//             //Arrange:
//             var variableA = new Fact("A", true);
//             var variableB = new Fact("B", true);
//             var state1 = new FactState(variableA, variableB);
//             var state2 = new FactState(variableA, variableB);
//
//             //Assert:
//             Assert.True(state1.EqualsTo(state1));
//             Assert.True(state2.EqualsTo(state2));
//             Assert.True(state1.EqualsTo(state2));
//             Assert.True(state2.EqualsTo(state1));
//         }
//
//         [Test]
//         public void NotEqualsStatesWithSameVariables()
//         {
//             //Arrange:
//             var state1 = new FactState(
//                 new Fact("A", true),
//                 new Fact("B", true)
//             );
//             var state2 = new FactState(
//                 new Fact("A", true),
//                 new Fact("B", false)
//             );
//
//             //Assert:
//             Assert.False(state1.EqualsTo(state2));
//             Assert.False(state2.EqualsTo(state1));
//         }
//
//         [Test]
//         public void EqualsFirstToSecondButNotViceVersa()
//         {
//             //Arrange:
//             var state1 = new FactState(
//                 new Fact("A", true),
//                 new Fact("B", true)
//             );
//             var state2 = new FactState(
//                 new Fact("A", true),
//                 new Fact("B", true),
//                 new Fact("C", true)
//             );
//
//             //Assert:
//             Assert.True(state1.EqualsTo(state2));
//             Assert.False(state2.EqualsTo(state1));
//         }
//     }
// }