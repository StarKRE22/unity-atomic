/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Objects;
using JetBrains.Annotations;
using Sample;

namespace GameEngine
{
    public static class TagAPI
    {
        ///Keys
        public const int Character = 1;
        public const int Enemy = 2;
        public const int Barn = 3;


        ///Extensions
        public static bool HasCharacterTag(this IObject obj) => obj.HasTag(Character);
        public static bool AddCharacterTag(this IObject obj) => obj.AddTag(Character);
        public static bool DelCharacterTag(this IObject obj) => obj.DelTag(Character);

        public static bool HasEnemyTag(this IObject obj) => obj.HasTag(Enemy);
        public static bool AddEnemyTag(this IObject obj) => obj.AddTag(Enemy);
        public static bool DelEnemyTag(this IObject obj) => obj.DelTag(Enemy);

        public static bool HasBarnTag(this IObject obj) => obj.HasTag(Barn);
        public static bool AddBarnTag(this IObject obj) => obj.AddTag(Barn);
        public static bool DelBarnTag(this IObject obj) => obj.DelTag(Barn);
    }
}
