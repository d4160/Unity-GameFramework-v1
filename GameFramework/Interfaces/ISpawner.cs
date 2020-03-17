using UnityEngine;

namespace d4160.GameFramework
{
    public interface ISpawner
    {
        void StartSpawn();

        void StopSpawn();

        void ResumeSpawn();
    }

    public interface ISpawnProvider
    {
        int SelectedSourceIndex { get; set; }
        bool CanSpawn { get; }

        void Spawn(int sourceIndex = -1);
    }
}

