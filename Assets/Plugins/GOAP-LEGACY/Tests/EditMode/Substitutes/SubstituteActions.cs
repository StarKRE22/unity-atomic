// namespace AI.GOAP
// {
//     public static class SubstituteActions
//     {
//         public static IActor MoveAtEnemyAction = new SubstituteActor(
//             id: "moveAtEnemyAction",
//             cost: 10,
//             requiredState: new FactState(
//                 new Fact("enemyExists", true)
//             ),
//             resultState: new FactState(
//                 new Fact("atEnemy", true),
//                 new Fact("nearEnemy", true)
//             )
//         );
//         
//         public static IActor MoveNearEnemyAction = new SubstituteActor(
//             id: "moveNearEnemyAction",
//             cost: 3,
//             requiredState: new FactState(
//                 new Fact("enemyExists", true)
//             ),
//             resultState: new FactState(
//                 new Fact("nearEnemy", true)
//             )
//         );
//
//         public static IActor SwordCombatAction = new SubstituteActor(
//             id: "swordCombatAction",
//             cost: 3,
//             requiredState: new FactState(
//                 new Fact("atEnemy", true)
//             ),
//             resultState: new FactState(
//                 new Fact("enemyExists", false)
//             )
//         );
//         
//         public static IActor BowCombatAction = new SubstituteActor(
//             id: "bowCombatAction",
//             cost: 2,
//             requiredState: new FactState(
//                 new Fact("nearEnemy", true),
//                 new Fact("arrowsExists", true)
//             ),
//             resultState: new FactState(
//                 new Fact("enemyExists", false)
//             )
//         );
//
//         public static IActor MakeHealAction = new SubstituteActor(
//             id: "makeHealAction",
//             cost: 5,
//             resultState: new FactState(new Fact("isInjured", false)),
//             requiredState: new FactState()
//         );
//
//         public static IActor MoveToResourceAction = new SubstituteActor(
//             id: "moveToResourceAction",
//             cost: 12,
//             resultState: new FactState(new Fact("atResource", true)),
//             requiredState: new FactState()
//         );
//
//         public static IActor HarvestResourceAction = new SubstituteActor(
//             id: "harvestResourceAction",
//             cost: 5,
//             resultState: new FactState(new Fact("resourceExists", true)),
//             requiredState: new FactState(new Fact("atResource", true))
//         );
//
//         public static IActor GoToMarketAction = new SubstituteActor(
//             id: "goToMarketAction",
//             cost: 6,
//             resultState: new FactState(new Fact("moneyEnough", true)),
//             requiredState: new FactState(new Fact("resourceExists", true))
//         );
//
//         public static IActor GoBeerAction = new SubstituteActor(
//             id: "goBeerAction",
//             cost: 9,
//             resultState: new FactState(new Fact("happy", true)),
//             requiredState: new FactState(new Fact("moneyEnough", true))
//         );
//     }
// }