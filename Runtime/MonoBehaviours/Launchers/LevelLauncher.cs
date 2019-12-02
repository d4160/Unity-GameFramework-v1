namespace d4160.GameFramework
{
    using Malee;
    using UnityEngine;

    public abstract class LevelLauncher : MonoBehaviour, ILevelLauncher
    {
        /* The index in the GameManager */
        public int LauncherIndex { get; set; } = 0;

        public abstract void Load(System.Action onCompleted = null);

        public abstract void Unload(System.Action onCompleted = null);

        public virtual void ActivateScenes(){}
    }

    [System.Serializable]
    public class LevelLaunchersReorderableArray : ReorderableArrayForUnityObject<LevelLauncher>
    { }
}