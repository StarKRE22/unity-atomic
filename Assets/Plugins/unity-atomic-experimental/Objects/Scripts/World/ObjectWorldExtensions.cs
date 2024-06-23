using UnityEngine;

namespace Atomic.Objects
{
    public static class ObjectWorldExtensions
    {
        public static SceneObject Instantiate(
            this SceneObjectWorld world,
            SceneObject prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent
        )
        {
            SceneObject sceneObject = GameObject.Instantiate(prefab, position, rotation, parent);
            world.RunObject(sceneObject);
            return sceneObject;
        }

        public static GameObject InstantiateGameObject(
            this SceneObjectWorld world,
            GameObject prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent
        )
        {
            GameObject gameObject = GameObject.Instantiate(prefab, position, rotation, parent);
            world.RunObject(gameObject);
            return gameObject;
        }

        public static void RunObject(this SceneObjectWorld world, GameObject gameObject)
        {
            SceneObject[] sceneObjects = gameObject.GetComponentsInChildren<SceneObject>();

            int count = sceneObjects.Length;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                world.InstallObject(sceneObjects[i]);
            }

            for (int i = 0; i < count; i++)
            {
                world.ConstructObject(sceneObjects[i]);
            }

            for (int i = 0; i < count; i++)
            {
                world.OnEnableObject(sceneObjects[i]);
            }
        }

        public static void DestroyGameObject(this SceneObjectWorld world, GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            
            SceneObject[] sceneObjects = gameObject.GetComponentsInChildren<SceneObject>();

            int count = sceneObjects.Length;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                SceneObject sceneObject = sceneObjects[i];
                if (sceneObject != null)
                {
                    world.UnspawnObject(sceneObject);
                }
            }

            GameObject.Destroy(gameObject);
        }

        public static void Destroy(this SceneObjectWorld world, SceneObject sceneObject, bool destroyGO = true)
        {
            if (sceneObject == null)
            {
                return;
            }
            
            world.UnspawnObject(sceneObject);
            
            if (destroyGO)
            {
                GameObject.Destroy(sceneObject.gameObject);
            }
            else
            {
                GameObject.Destroy(sceneObject);
            }
        }
    }
}