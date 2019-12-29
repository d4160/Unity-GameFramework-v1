namespace d4160.GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using d4160.Systems.SceneManagement;
    using d4160.UI;
    using d4160.Systems.DataPersistence;

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
                m_worldLoadingAsyncOp.allowSceneActivation = true;
            }

            if (m_levelLoadingAsyncOp != null)
            {
                m_levelLoadingAsyncOp.allowSceneActivation = true;
            }
        }

        public override void Load(System.Action onCompleted = null)
        {
            LoadWorldScene(onCompleted);
        }

        protected virtual void LoadWorldScene(System.Action onCompleted = null)
        {
            var buildIndex = (GameFrameworkSettings.GameDatabase[2] as IWorldSceneGetter).GetSceneBuildIndex(m_worldScene);
            if (buildIndex == -1)
            {
                LoadLevelScene(true, onCompleted);
                return;
            }

            //Debug.Log($"WorldSceneName null?: {worldSceneName}");
            m_sceneLoader.LoadSceneAsync(
                buildIndex,
                true,
                (ao) => m_worldLoadingAsyncOp = ao,
                null,
                false,
                (p) => {
                    if (p >= 0.9f)
                    {
                        if (!m_levelSceneLoading)
                        {
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
            var buildIndex = (GameFrameworkSettings.GameDatabase[3] as ILevelSceneGetter).GetSceneBuildIndex(m_levelScene);

            m_sceneLoader.LoadSceneAsync(
                buildIndex,
                setActiveAsMainScene,
                (ao) => m_levelLoadingAsyncOp = ao,
                () => onCompleted?.Invoke(),
                false,
                (p) => {
                    if (p < 0.9f)
                    {
                        if (LoadingScreenBase.Instanced)
                            LoadingScreenBase.Instance.SetLoadingProgress(p);
                    }
                    else
                    {
                        if (!m_loadingCompleted)
                        {
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