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
        public const int Resource = 3;
        public const int CapturePoint = 4;


        ///Extensions
        public static bool HasCharacterTag(this IAtomicObject obj) => obj.HasTag(Character);
        public static bool AddCharacterTag(this IAtomicObject obj) => obj.AddTag(Character);
        public static bool DelCharacterTag(this IAtomicObject obj) => obj.DelTag(Character);

        public static bool HasEnemyTag(this IAtomicObject obj) => obj.HasTag(Enemy);
        public static bool AddEnemyTag(this IAtomicObject obj) => obj.AddTag(Enemy);
        public static bool DelEnemyTag(this IAtomicObject obj) => obj.DelTag(Enemy);

        public static bool HasResourceTag(this IAtomicObject obj) => obj.HasTag(Resource);
        public static bool AddResourceTag(this IAtomicObject obj) => obj.AddTag(Resource);
        public static bool DelResourceTag(this IAtomicObject obj) => obj.DelTag(Resource);

        public static bool HasCapturePointTag(this IAtomicObject obj) => obj.HasTag(CapturePoint);
        public static bool AddCapturePointTag(this IAtomicObject obj) => obj.AddTag(CapturePoint);
        public static bool DelCapturePointTag(this IAtomicObject obj) => obj.DelTag(CapturePoint);
    }
}
