namespace d4160.GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using d4160.Systems.SceneManagement;

    [RequireComponent(typeof(UniRxAsyncEmptySceneLoader))]
    public abstract class DefaultLevelLauncher : LevelLauncher
#if UNITY_EDITOR
        , IWorldSceneNames, ILevelSceneNames
#endif
    {
        [Header("LAUNCHER SCENES")]
        [SerializeField] protected WorldScene m_worldScene;
        [SerializeField] protected LevelScene m_levelScene;

        protected AsyncOperation m_worldLoadingAsyncOp, m_menuLoadingAsyncOp;
        protected bool m_menuSceneLoading, m_loadingCompleted;

#if UNITY_EDITOR
        #region IWorldScene Implementation
        public abstract string[] WorldNames { get; }
        public abstract string[] WorldSceneNames { get; }
        #endregion

        #region ILevelScene Implementation
        public abstract string[] LevelCategoryNames { get; }
        public abstract string[] LevelSceneNames { get; }
        #endregion
#endif

        public override void ActivateScenes()
        {
            if (m_worldLoadingAsyncOp != null)
            {
                m_worldLoadingAsyncOp.allowSceneActivation = true;
            }

            if (m_menuLoadingAsyncOp != null)
            {
                m_menuLoadingAsyncOp.allowSceneActivation = true;
            }
        }
    }
}