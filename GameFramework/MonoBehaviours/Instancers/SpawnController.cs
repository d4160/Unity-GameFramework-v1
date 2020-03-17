using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class SpawnController : MonoBehaviour
    {
        public abstract void WaveSpawnStartAction(int waveIndex);

        public abstract void WaveSpawnCompleteAction(int waveIndex);
    }
}
