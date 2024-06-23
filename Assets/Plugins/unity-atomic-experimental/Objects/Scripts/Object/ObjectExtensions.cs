using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Objects
{
    public static class ObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IObject obj, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
            {
                int tag = tags[i];
                if (!obj.HasTag(tag))
                {
                    return false;
                }
            }

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IObject obj, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
            {
                int tag = tags[i];
                if (obj.HasTag(tag))
                {
                    return true;
                }
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSceneObject(this GameObject gameObject, out IObject obj)
        {
            return gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSceneObject(this Component component, out IObject obj)
        {
            return component.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSceneObject(this Collision2D collision2D, out IObject obj)
        {
            return collision2D.gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSceneObject(this Collision collision, out IObject obj)
        {
            return collision.gameObject.TryGetComponent(out obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddElement(this IObject obj, int id, ILogic element)
        {
            if (obj.AddValue(id, element))
            {
                obj.AddLogic(element);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelElement(this IObject obj, int id)
        {
            if (obj.DelValue(id, out var removed))
            {
                obj.DelLogic(removed as ILogic);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetElement(this IObject obj, int id, ILogic element)
        {
            obj.DelElement(id);
            obj.AddElement(id, element);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IObject Install(this IObject obj, IObjectInstaller installer)
        {
            installer.Install(obj);
            return obj;
        }
    }
}