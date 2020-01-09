namespace d4160.GameFramework
{
    using d4160.UI.Loading;
    using UnityEngine;
#if NAUGHTY_ATTRIBUTES
    using NaughtyAttributes;
#endif

    public abstract class DefaultLoadingScreenLauncher : DefaultLevelLauncher
    {
        [Header("LOAD LEVEL")]
        [SerializeField] protected LevelType m_levelTypeToLoad;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsCommonLevelTypeSelected")]
#endif
        [d4160.Core.Attributes.Dropdown("BootLoader", "PlayLogic", ValuesProperty = "LevelCategoryNames")]
        [SerializeField] protected int m_generalLevelToLoad;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsGameModeLevelTypeSelected")]
#endif
        [d4160.Core.Attributes.Dropdown(ValuesProperty = "GameModeCategoriesNames")]
        [SerializeField] protected int m_gameModeLevelToLoad;

#if UNITY_EDITOR
#region Other Editor Members
        protected virtual string[] GameModeCategoriesNames => (GameFrameworkSettings.GameDatabase[1] as IArchetypeNames).ArchetypeNames;

        protected bool IsCommonLevelTypeSelected()
        {
            return m_levelTypeToLoad == LevelType.General;
        }

        protected bool IsGameModeLevelTypeSelected()
        {
            return m_levelTypeToLoad == LevelType.GameMode;
        }
#endregion
#endif

#region Unity Callbacks

        protected virtual void OnEnable()
        {
            LoadingScreenBase.OnLoadCompleted += LoadingScreen_OnLoadCompleted;
        }

        protected virtual void OnDisable()
        {
            LoadingScreenBase.OnLoadCompleted -= LoadingScreen_OnLoadCompleted;
        }
#endregion

#region Events Callbacks
        protected virtual void LoadingScreen_OnLoadCompleted()
        {
            var level = m_levelTypeToLoad == LevelType.General ? m_generalLevelToLoad : m_gameModeLevelToLoad;
            GameManager.Instance.ActivateScenes(m_levelTypeToLoad, level);

            // LoadingScreen level is the first (0)
            GameManager.Instance.UnloadLevel(LevelType.General, 0);
        }
#endregion

        public virtual void SetLevelToLoad(LevelType levelType, int level)
        {
            m_levelTypeToLoad = levelType;
            if (levelType == LevelType.GameMode)
                m_gameModeLevelToLoad = level;
            else if (levelType == LevelType.General)
                m_generalLevelToLoad = level;
        }

        protected override void LoadWorldScene(System.Action onCompleted = null)
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
                null,
                () => LoadLevelScene(false, onCompleted),
                true,
                null
            );
        }

        protected override void LoadLevelScene(bool setActiveAsMainScene = false, System.Action onCompleted = null)
        {
            var buildIndex = (GameFrameworkSettings.GameDatabase[3] as ILevelSceneGetter).GetSceneBuildIndex(m_levelScene);

            m_sceneLoader.LoadSceneAsync(
                buildIndex,
                setActiveAsMainScene,
                null,
                () => {
                    var level = m_levelTypeToLoad == LevelType.General ? m_generalLevelToLoad : m_gameModeLevelToLoad;
                    var loadinScreen = LoadingScreenBase.Instance;
                    GameManager.Instance.LoadLevel(m_levelTypeToLoad, level);
                    loadinScreen.StartLoad();

                    onCompleted?.Invoke();
                },
                true,
                null
            );
        }

        public override void Unload(System.Action onCompleted = null)
        {
            m_sceneLoader.UnloadAllLoadedScenes(onCompleted);
        }
    }
}