// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using static AI.GOAP.SubstituteActions;
// using static AI.GOAP.TestUtils;
//
// namespace AI.GOAP
// {
//     public class GreedyPlannerTests
//     {
//         private readonly GreedyPlanner planner = new();
//         
//         [Test]
//         public void MakeHealPlanTest()
//         {
//             //Arrange:
//             var worldState = new FactState(
//                 new Fact("isInjured", true)
//             );
//
//             var goal = new FactState(
//                 new Fact("isInjured", false)
//             );
//
//             var actions = new[]
//             {
//                 MakeHealAction
//             };
//
//             //Act:
//             var success = this.planner.MakePlan(worldState, goal, actions, out var actualPlan);
//
//             //Assert:
//             Assert.True(success);
//
//             var expectedPlan = new[]
//             {
//                 MakeHealAction
//             };
//             Assert.True(EqualsPlans(expectedPlan, actualPlan.ToArray()));
//         }
//
//         [Test]
//         public void MeleeCombatPlanTest()
//         {
//             //Arrange:
//             var worldState = new FactState(
//                 new Fact("enemyExists", true),
//                 new Fact("nearEnemy", false),
//                 new Fact("atEnemy", false),
//                 new Fact("isInjured", true),
//                 new Fact("hasAmmo", true)
//             );
//
//             var goal = new FactState(
//                 new Fact("enemyExists", false)
//             );
//
//             var actions = new[]
//             {
//                 SwordCombatAction,
//                 MoveAtEnemyAction,
//                 MakeHealAction
//             };
//
//             //Act:
//             var actionPlanner = new GreedyPlanner();
//             var success = actionPlanner.MakePlan(worldState, goal, actions, out var actualPlan);
//
//             //Assert:
//             Assert.True(success);
//
//             var expectedPlan = new List<IActor>
//             {
//                 MoveAtEnemyAction,
//                 SwordCombatAction
//             };
//             Assert.True(EqualsPlans(expectedPlan, actualPlan));
//         }
//
//         [Test]
//         public void RangeCombatPlanTest()
//         {
//             //Arrange:
//             var worldState = new FactState(
//                 new Fact("enemyExists", true),
//                 new Fact("nearEnemy", false),
//                 new Fact("atEnemy", false),
//                 new Fact("isInjured", true),
//                 new Fact("arrowsExists", true)
//             );
//
//             var goal = new FactState(
//                 new Fact("enemyExists", false)
//             );
//
//             var actions = new[]
//             {
//                 SwordCombatAction,
//                 BowCombatAction,
//                 MoveNearEnemyAction,
//                 MoveAtEnemyAction,
//                 MoveToResourceAction,
//                 MakeHealAction
//             };
//
//             //Act:
//             var actionPlanner = new GreedyPlanner();
//             var success = actionPlanner.MakePlan(worldState, goal, actions, out var actualPlan);
//
//             //Assert:
//             Assert.True(success);
//
//             var expectedPlan = new List<IActor>
//             {
//                 MoveNearEnemyAction,
//                 BowCombatAction
//             };
//
//             Debug.Log(string.Join(',', actualPlan));
//             
//             Assert.True(EqualsPlans(expectedPlan, actualPlan));
//         }
//
//         [Test]
//         public void DrinkBeerTest()
//         {
//             //Arrange:
//             var worldState = new FactState(
//                 new Fact("enemyExists", true),
//                 new Fact("nearEnemy", false),
//                 new Fact("atEnemy", false),
//                 new Fact("isInjured", true),
//                 new Fact("hasAmmo", true),
//                 new Fact("moneyEnough", false),
//                 new Fact("resourceEnough", false),
//                 new Fact("atResource", false)
//             );
//
//             var goal = new FactState(
//                 new Fact("happy", true)
//             );
//
//             var actions = new[]
//             {
//                 SwordCombatAction,
//                 MoveAtEnemyAction,
//                 MakeHealAction,
//                 MoveToResourceAction,
//                 HarvestResourceAction,
//                 GoToMarketAction,
//                 GoBeerAction
//             };
//
//             //Act:
//             var actionPlanner = new GreedyPlanner();
//             var success = actionPlanner.MakePlan(worldState, goal, actions, out var actualPlan);
//
//             //Assert:
//             Assert.True(success);
//
//             var expectedPlan = new List<IActor>
//             {
//                 MoveToResourceAction,
//                 HarvestResourceAction,
//                 GoToMarketAction,
//                 GoBeerAction
//             };
//             Assert.True(EqualsPlans(expectedPlan, actualPlan));
//         }
//     }
// }