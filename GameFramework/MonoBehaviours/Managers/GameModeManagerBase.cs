using d4160.Core;
using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class GameModeManagerBase : Singleton<GameModeManagerBase>
    {
        public abstract void Despawn(GameObject instance, int entity, int poolIndex = 0, float delay = 0f);

        public abstract void Despawn(GameObject instance, int entity, int category, int poolIndex = 0, float delay = 0f);

        /// <summary>
        /// -1 means all
        /// </summary>
        /// <param name="spawnIndex"></param>
        public virtual void StartSpawner(int spawnIndex = -1)
        {
        }

        /// <summary>
        /// -1 means all
        /// </summary>
        /// <param name="spawnIndex"></param>
        public virtual void StopSpawner(int spawnIndex = -1)
        {
        }
    }
}