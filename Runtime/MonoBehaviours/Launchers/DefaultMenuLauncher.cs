namespace DefaultGame
{
    using d4160.GameFramework;
    using d4160.Levels;
    using d4160.Worlds;
    using UniRx.Async;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using d4160.Systems.SceneManagement;
    using d4160.UI;

    [RequireComponent(typeof(UniRxAsyncEmptySceneLoader))]
    public class DefaultMenuLauncher : LevelLauncher
#if UNITY_EDITOR
        , IWorldSceneNames, ILevelSceneNames
#endif
    {
        [SerializeField] protected WorldScene m_worldScene;
        [SerializeField] protected LevelScene m_menuScene;

        protected UniRxAsyncEmptySceneLoader m_sceneLoader;
        protected AsyncOperation m_worldLoadingAsyncOp, m_menuLoadingAsyncOp;
        protected bool m_menuSceneLoading, m_loadingCompleted;

#if UNITY_EDITOR
        #region IWorldScene Implementation
        public string[] WorldNames => GameFrameworkSettings.Database.GetGameData<DefaultWorldsSO>(2).ArchetypeNames;
        public string[] WorldSceneNames => GameFrameworkSettings.Database.GetGameData<DefaultWorldsSO>(2).GetSceneNames(m_worldScene.world);
        #endregion

        #region ILevelScene Implementation
        public string[] LevelCategoryNames => GameFrameworkSettings.Database.GetGameData<DefaultLevelCategoriesSO>(3).ArchetypeNames;
        public string[] LevelSceneNames => GameFrameworkSettings.Database.GetGameData<DefaultLevelCategoriesSO>(3).GetSceneNames(m_menuScene.levelCategory);
        #endregion
#endif

        protected virtual void Awake()
        {
            m_sceneLoader = GetComponent<UniRxAsyncEmptySceneLoader>();
        }

        public override async void Load(System.Action onCompleted = null)
        {
            await LoadWorldScene(onCompleted);
        }

        protected virtual async UniTask LoadWorldScene(System.Action onCompleted = null)
        {
            var buildIndex = GameFrameworkSettings.Database.GetGameData<DefaultWorldsSO>(2).GetSceneBuildIndex(m_worldScene);
            if (buildIndex == -1)
            {
                LoadMenuScene(true, onCompleted);
                return;
            }

            //Debug.Log($"WorldSceneName null?: {worldSceneName}");
            await m_sceneLoader.LoadSceneAsync(
                buildIndex,
                true,
                (ao) => m_worldLoadingAsyncOp = ao,
                null,
                false,
                (p) => {
                    if (p >= 0.9f)
                    {
                        if (!m_menuSceneLoading)
                        {
                            LoadMenuScene(false, onCompleted);
                            m_menuSceneLoading = true;
                        }
                    }
                    else
                    {
                        if (LoadingScreen.Instanced)
                            LoadingScreen.Instance.SetLoadingProgress(p);
                    }
                }
            );
        }

        protected virtual async void LoadMenuScene(bool setActiveAsMainScene = false, System.Action onCompleted = null)
        {
            var buildIndex = GameFrameworkSettings.Database.GetGameData<DefaultLevelCategoriesSO>(3).GetSceneBuildIndex(m_menuScene);

            await m_sceneLoader.LoadSceneAsync(
                buildIndex,
                setActiveAsMainScene,
                (ao) => m_menuLoadingAsyncOp = ao,
                () => onCompleted?.Invoke(),
                false,
                (p) => {
                    if (p < 0.9f)
                    {
                        if (LoadingScreen.Instanced)
                            LoadingScreen.Instance.SetLoadingProgress(p);
                    }
                    else
                    {
                        if (!m_loadingCompleted)
                        {
                            if (LoadingScreen.Instanced)
                                LoadingScreen.Instance.SetAsLoadCompleted();
                            else
                                ActivateScenes();

                            m_loadingCompleted = true;
                        }
                    }
                }
            );
        }

        public override async void Unload(System.Action onCompleted = null)
        {
            await m_sceneLoader.UnloadAllLoadedScenes(onCompleted);

            m_loadingCompleted = false;
            m_menuSceneLoading = false;

            m_worldLoadingAsyncOp = null;
            m_menuLoadingAsyncOp = null;
        }

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