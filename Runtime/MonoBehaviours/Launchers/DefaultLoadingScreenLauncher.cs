namespace d4160.Levels
{
    using d4160.GameFramework;
    using d4160.UI;
    using d4160.Worlds;
    using UnityEngine;
    using d4160.Systems.SceneManagement;
    using NaughtyAttributes;

    [RequireComponent(typeof(UniRxAsyncEmptySceneLoader))]
    public class DefaultLoadingScreenLauncher : LevelLauncher
#if UNITY_EDITOR
        , IWorldSceneNames, ILevelSceneNames
#endif
    {   [Header("LOADING SCREEN LEVEL")]
        [SerializeField] protected WorldScene m_worldScene;
        [SerializeField] protected LevelScene m_loadingScreenScene;

        [Header("LOAD LEVEL")]
        [SerializeField] protected LevelType m_levelTypeToLoad;
        [ShowIf("IsCommonLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown("BootLoader", "PlayLogic", ValuesProperty = "LevelCategoryNames")]
        [SerializeField] protected int m_generalLevelToLoad;
        [ShowIf("IsGameModeLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown(ValuesProperty = "GameModeCategoriesNames")]
        [SerializeField] protected int m_gameModeLevelToLoad;

        protected UniRxAsyncEmptySceneLoader m_sceneLoader;

#if UNITY_EDITOR
        #region IWorldScene Implementation
        public string[] WorldNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultWorldsSO>(2).ArchetypeNames;
        public string[] WorldSceneNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultWorldsSO>(2).GetSceneNames(m_worldScene.world);
        #endregion

        #region ILevelScene Implementation
        public string[] LevelCategoryNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).ArchetypeNames;
        public string[] LevelSceneNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).GetSceneNames(m_loadingScreenScene.levelCategory);
        #endregion

        #region Other Editor Members
        protected string[] GameModeCategoriesNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultArchetypesSO>(1).ArchetypeNames;

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
        protected virtual void Awake()
        {
            m_sceneLoader = GetComponent<UniRxAsyncEmptySceneLoader>();
        }

        protected virtual void OnEnable()
        {
            LoadingScreen.OnLoadCompleted += LoadingScreen_OnLoadCompleted;
        }

        protected virtual void OnDisable()
        {
            LoadingScreen.OnLoadCompleted -= LoadingScreen_OnLoadCompleted;
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

        public override async void Load(System.Action onCompleted = null)
        {
            var buildIndex = GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).GetSceneBuildIndex(m_loadingScreenScene);

            await m_sceneLoader.LoadSceneAsync(
                buildIndex, true, null, () => onCompleted?.Invoke(), true, null);

            var level = m_levelTypeToLoad == LevelType.General ? m_generalLevelToLoad : m_gameModeLevelToLoad;
            var loadinScreen = LoadingScreen.Instance;

            GameManager.Instance.LoadLevel(m_levelTypeToLoad, level);

            loadinScreen.StartLoad();
        }

        public override async void Unload(System.Action onCompleted = null)
        {
            await m_sceneLoader.UnloadAllLoadedScenes(onCompleted);
        }

        public virtual void SetLevelToLoad(LevelType levelType, int level)
        {
            m_levelTypeToLoad = levelType;
            if (levelType == LevelType.GameMode)
                m_gameModeLevelToLoad = level;
            else if (levelType == LevelType.General)
                m_generalLevelToLoad = level;
        }

        /* TODO Set world and logic scenes methods */
    }
}