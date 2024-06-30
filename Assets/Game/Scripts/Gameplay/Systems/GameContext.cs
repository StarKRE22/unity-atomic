using Atomic.Contexts;
using UnityEngine;

namespace SampleGame
{
    public sealed class GameContext : MonoBehaviour
    {
        public static IContext Instance
        {
            get { return _instance ??= GameObject.FindWithTag("GameContext").GetComponent<IContext>(); }
        }

        private static IContext _instance;
    }
}