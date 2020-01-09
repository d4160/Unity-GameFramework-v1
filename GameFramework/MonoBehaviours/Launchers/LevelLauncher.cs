namespace d4160.GameFramework
{
    using d4160.SceneManagement;
    using Malee;
    using UnityEngine;

    public abstract class LevelLauncher : MonoBehaviour, ILevelLauncher
    {
        [SerializeField] protected SceneLoaderType m_sceneLoaderType;

        protected IAsyncSceneLoader m_sceneLoader;

        /* The index in the GameManager */
        public int LauncherIndex { get; set; } = 0;
        public IAsyncSceneLoader SceneLoader => m_sceneLoader;

        protected virtual void Awake()
        {
            m_sceneLoader = DefaultSceneLoaderFactory.Instance.Create(m_sceneLoaderType);
        }

        public abstract void Load(System.Action onCompleted = null);

        public abstract void Unload(System.Action onCompleted = null);

        public virtual void ActivateScenes(){}
    }

    [System.Serializable]
    public class LevelLaunchersReorderableArray : ReorderableArrayForUnityObject<LevelLauncher>
    { }
}