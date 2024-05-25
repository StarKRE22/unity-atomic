/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Objects;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Sample;
using Atomic.Elements;

namespace GameEngine
{
    public static class AIAPI
    {
        ///Keys
        public const int PatrolPoints = 9; // Transform[]


        ///Extensions
        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform[] GetPatrolPoints(this IAtomicObject obj) => obj.GetValue<Transform[]>(PatrolPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPatrolPoints(this IAtomicObject obj, out Transform[] value) => obj.TryGetValue(PatrolPoints, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddPatrolPoints(this IAtomicObject obj, Transform[] value) => obj.AddValue(PatrolPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelPatrolPoints(this IAtomicObject obj) => obj.DelValue(PatrolPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPatrolPoints(this IAtomicObject obj, Transform[] value) => obj.SetValue(PatrolPoints, value);
    }
}
