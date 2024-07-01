using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI
{
    internal sealed class ViewUpdateManager : MonoBehaviour
    {
        private static ViewUpdateManager _instance;

        private readonly List<(IView, IUpdateViewBehaviour)> updateBehaviours = new();
        private readonly List<(IView, IUpdateViewBehaviour)> updateCache = new();

        private static ViewUpdateManager instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("ViewManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<ViewUpdateManager>();
                }

                return _instance;
            }
        }

        internal static void AddBehaviour(IView view, IUpdateViewBehaviour behaviour)
        {
            if (behaviour != null)
            {
                instance.updateBehaviours.Add((view, behaviour));
            }
        }

        internal static void RemoveBehaviour(IView view, IUpdateViewBehaviour behaviour)
        {
            if (behaviour != null)
            {
                instance.updateBehaviours.Remove((view, behaviour));
            }
        }

        private void Update()
        {
            int count = this.updateBehaviours.Count;
            if (count == 0)
            {
                return;
            }

            this.updateCache.Clear();
            this.updateCache.AddRange(this.updateBehaviours);

            float deltaTime = Time.deltaTime;
            
            for (int i = 0; i < count; i++)
            {
                (IView view, IUpdateViewBehaviour behaviour) = this.updateCache[i];
                behaviour.Update(view, deltaTime);
            }
        }
    }
}