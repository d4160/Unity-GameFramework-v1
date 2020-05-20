namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.UI.Loading;
    using d4160.DataPersistence;

    public abstract class DefaultLevelLauncher : LevelLauncher
#if UNITY_EDITOR
        , IWorldSceneNames, ILevelSceneNames
#endif
    {
        [Header("LAUNCHER SCENES")]
        [SerializeField] protected WorldScene m_worldScene;
        [SerializeField] protected LevelScene m_levelScene;

        protected AsyncOperation m_worldLoadingAsyncOp, m_levelLoadingAsyncOp;
        protected bool m_levelSceneLoading, m_loadingCompleted;

#if UNITY_EDITOR
        #region IWorldScene Implementation
        public virtual string[] WorldNames => (GameFrameworkSettings.GameDatabase[2] as IArchetypeNames).ArchetypeNames;
        public virtual string[] WorldSceneNames => (GameFrameworkSettings.GameDatabase[2] as ISceneNamesGetter).GetSceneNames(m_worldScene.world);
        #endregion

        #region ILevelScene Implementation
        public virtual string[] LevelCategoryNames => (GameFrameworkSettings.GameDatabase[3] as IArchetypeNames).ArchetypeNames;
        public virtual string[] LevelSceneNames => (GameFrameworkSettings.GameDatabase[3] as ISceneNamesGetter).GetSceneNames(m_levelScene.levelCategory);
        #endregion
#endif

        public override void ActivateScenes()
        {
            if (m_worldLoadingAsyncOp != null)
            {
                //Debug.Log($"(Level Launcher) Activating World Scene.");
                m_worldLoadingAsyncOp.allowSceneActivation = true;
            }

            if (m_levelLoadingAsyncOp != null)
            {
                //Debug.Log($"(Level Launcher) Activating Level Scene.");
                m_levelLoadingAsyncOp.allowSceneActivation = true;
            }
        }

        public override void Load(System.Action onCompleted = null)
        {
            LoadWorldScene(onCompleted);
        }

        protected virtual void LoadWorldScene(System.Action onCompleted = null)
        {
            var buildIndex = (GameFrameworkSettings.GameDatabase[2] as IWorldSceneGetter)?.GetSceneBuildIndex(m_worldScene);

            //Debug.Log($"(Level Launcher) Try to Load World Scene: {buildIndex} index");

            if (!buildIndex.HasValue || buildIndex == -1)
            {
                //Debug.Log($"(Level Launcher) Loading World Scene: No Scene. Go to Load LevelScene");
                LoadLevelScene(true, onCompleted);
                return;
            }

            //Debug.Log($"(Level Launcher) Loading World Scene: {buildIndex} index");
            m_sceneLoader.LoadSceneAsync(
                buildIndex.Value,
                true,
                (ao) => m_worldLoadingAsyncOp = ao,
                null,
                false,
                (p) => {
                    if (p >= 0.9f)
                    {
                        if (!m_levelSceneLoading)
                        {
                            //Debug.Log($"(Level Launcher) World Scene loaded: {buildIndex} index");
                            LoadLevelScene(false, onCompleted);
                            m_levelSceneLoading = true;
                        }
                    }
                    else
                    {
                        if (LoadingScreenBase.Instanced)
                            LoadingScreenBase.Instance.SetLoadingProgress(p);
                    }
                }
            );
        }

        protected virtual void LoadLevelScene(bool setActiveAsMainScene = false, System.Action onCompleted = null)
        {
            var buildIndex = (GameFrameworkSettings.GameDatabase[3] as ILevelSceneGetter)?.GetSceneBuildIndex(m_levelScene);

            //Debug.Log($"(Level Launcher) Try to Load Level Scene: {buildIndex} index");

            if (!buildIndex.HasValue || buildIndex == -1)
            {
                //Debug.Log($"(Level Launcher) Loading Level Scene: No Scene.");
                onCompleted?.Invoke();

                ActivateScenes();

                return;
            }

            m_sceneLoader.LoadSceneAsync(
                buildIndex.Value,
                setActiveAsMainScene,
                (ao) => m_levelLoadingAsyncOp = ao,
                () => onCompleted?.Invoke(),
                false,
                (p) => {
                    if (p < 0.9f)
                    {
                        //Debug.Log($"(Level Launcher) Loading Level Scene: {(int)(p * 100)}%");
                        if (LoadingScreenBase.Instanced)
                            LoadingScreenBase.Instance.SetLoadingProgress(p);
                    }
                    else
                    {
                        if (!m_loadingCompleted)
                        {
                            //Debug.Log($"(Level Launcher) Level Scene loaded: {buildIndex} index");

                            //Debug.Log($"(Level Launcher) LoadingScreenBase Instanced?: {LoadingScreenBase.Instanced}");

                            if (LoadingScreenBase.Instanced)
                                LoadingScreenBase.Instance.SetAsLoadCompleted();
                            else
                                ActivateScenes();

                            m_loadingCompleted = true;
                        }
                    }
                }
            );
        }

        public override void Unload(System.Action onCompleted = null)
        {
            m_sceneLoader.UnloadAllLoadedScenes(() => {

                m_loadingCompleted = false;
                m_levelSceneLoading = false;

                m_worldLoadingAsyncOp = null;
                m_levelLoadingAsyncOp = null;

                onCompleted?.Invoke();
            });
        }
    }
}