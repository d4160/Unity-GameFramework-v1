namespace d4160.GameFramework
{
    using d4160.GameFramework;
    using d4160.UI;
    using UnityEngine;
    using d4160.Systems.SceneManagement;
    using NaughtyAttributes;

    [RequireComponent(typeof(UniRxAsyncEmptySceneLoader))]
    public abstract class DefaultLoadingScreenLauncher : DefaultLevelLauncher
    {
        [Header("LOAD LEVEL")]
        [SerializeField] protected LevelType m_levelTypeToLoad;
        [ShowIf("IsCommonLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown("BootLoader", "PlayLogic", ValuesProperty = "LevelCategoryNames")]
        [SerializeField] protected int m_generalLevelToLoad;
        [ShowIf("IsGameModeLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown(ValuesProperty = "GameModeCategoriesNames")]
        [SerializeField] protected int m_gameModeLevelToLoad;

#if UNITY_EDITOR
        #region Other Editor Members
        protected abstract string[] GameModeCategoriesNames { get; }

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

        /* TODO Set world and logic scenes methods */
    }
}