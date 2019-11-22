namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Levels;
    using NaughtyAttributes;
    using Malee;
    using UnityEngine;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;

    public class GameManager : Singleton<GameManager>, ILevelLoader, ILevelScenesActiver, IGameModeFlowController, IPlayFlowController
    {
        #region Serialized Fields
        [SerializeField] protected bool m_startLevelAtStart;
        [ShowIf("m_startLevelAtStart")]
        [SerializeField] protected LevelType m_levelTypeToStart;
        [ShowIf(ConditionOperator.And, "m_startLevelAtStart", "IsCommonLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown("BootLoader", "PlayLogic",ValuesProperty = "LevelCategoryNames")]
        [SerializeField] protected int m_generalLevelToStart;
        [ShowIf(ConditionOperator.And, "m_startLevelAtStart", "IsGameModeLevelTypeSelected")]
        [d4160.Core.Attributes.Dropdown(ValuesProperty = "GameModeCategoryNames")]
        [SerializeField] protected int m_gameModeLevelToStart;

        [BoxGroup("General Launchers")]
        [Reorderable]
        [SerializeField] protected LevelLaunchersReorderableArray m_levelLaunchers;

        [BoxGroup("GameMode Launchers")]
        [Reorderable]
        [SerializeField] protected PlayLevelLauncherReorderableArray m_playLaunchers;
        #endregion

        #region Protected Fields and Properties
        protected List<LevelReference> m_startedLevels = new List<LevelReference>();

        public List<LevelReference> StartedLevels => m_startedLevels;
        #endregion

#if UNITY_EDITOR
        #region Editor Use Members
        protected string[] LevelCategoryNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).ArchetypeNames;
        protected string[] GameModeCategoryNames => GameFrameworkSettings.GameDatabase.GetGameData<DefaultArchetypesSO>(1).ArchetypeNames;

        protected bool IsCommonLevelTypeSelected()
        {
            return m_levelTypeToStart == LevelType.General;
        }

        protected bool IsGameModeLevelTypeSelected()
        {
            return m_levelTypeToStart == LevelType.GameMode;
        }

        private bool _firstLoad = true;
        #endregion
#endif

        #region Unity Callbacks
        protected virtual void Start()
        {
            SetLauncherIndexes();

            if (m_startLevelAtStart)
            {
                var level = m_levelTypeToStart == LevelType.General ? m_generalLevelToStart : m_gameModeLevelToStart;
                LoadLevel(m_levelTypeToStart, level);
            }
        }

        protected void SetLauncherIndexes()
        {
            for (int i = 0; i < m_levelLaunchers.Length; i++)
            {
                m_levelLaunchers[i].LauncherIndex = i;
            }

            for (int i = 0; i < m_playLaunchers.Length; i++)
            {
                m_playLaunchers[i].LauncherIndex = i;
            }
        }
        #endregion

        #region Get Methods
        public T GetLevelLauncher<T>(int index) where T : LevelLauncher
        {
            if (m_levelLaunchers.IsValidIndex(index))
            {
                return m_levelLaunchers[index] as T;
            }

            return null;
        }

        public LevelLauncher GetLevelLauncher(int index)
        {
            if (m_levelLaunchers.IsValidIndex(index))
            {
                return m_levelLaunchers[index];
            }

            return null;
        }

        public T GetPlayLevelLauncher<T>(int index) where T : PlayLevelLauncher
        {
            if (m_playLaunchers.IsValidIndex(index))
            {
                return m_playLaunchers[index] as T;
            }

            return null;
        }

        public PlayLevelLauncher GetPlayLevelLauncher(int index)
        {
            if (m_playLaunchers.IsValidIndex(index))
            {
                return m_playLaunchers[index];
            }

            return null;
        }
        #endregion

        #region ILevelLoader implementation
        public virtual void LoadLevel(LevelType levelType, int level, System.Action onCompleted = null)
        {
            switch (levelType)
            {
                case LevelType.General:
                    if (m_levelLaunchers.IsValidIndex(level))
                    {
#if UNITY_EDITOR
                        if (_firstLoad)
                        {
                            UnloadAllLoadedScenesExceptBootloader(() => {
                                AddStartedLevel(levelType, level);

                                m_levelLaunchers[level].Load();
                            });

                            _firstLoad = false;

                            return;
                        }
#endif
                        AddStartedLevel(levelType, level);

                        m_levelLaunchers[level].Load(onCompleted);
                    }
                    break;
                case LevelType.GameMode:
                    if (m_playLaunchers.IsValidIndex(level))
                    {
#if UNITY_EDITOR
                        if (_firstLoad)
                        {
                            UnloadAllLoadedScenesExceptBootloader(() => {
                                AddStartedLevel(levelType, level);

                                m_playLaunchers[level].Load();
                            });

                            _firstLoad = false;

                            return;
                        }
#endif
                        AddStartedLevel(levelType, level);

                        m_playLaunchers[level].Load(onCompleted);
                    }
                    break;
                default:
                    break;
            }
        }

        protected virtual void AddStartedLevel(LevelType levelType, int level)
        {
            var instance = new LevelReference(){
                            levelType = levelType,
                            level = level
                        };
            if (!m_startedLevels.Contains(instance))
            {
                m_startedLevels.Add(instance);
            }
        }

        public virtual void UnloadLevel(LevelType levelType, int level, System.Action onCompleted = null)
        {
            switch (levelType)
            {
                case LevelType.General:
                    if (m_levelLaunchers.IsValidIndex(level))
                    {
                        m_levelLaunchers[level].Unload(onCompleted);

                        RemoveStartedLevel(levelType, level);
                    }
                    break;
                case LevelType.GameMode:
                    if (m_playLaunchers.IsValidIndex(level))
                    {
                        m_playLaunchers[level].Unload(onCompleted);

                        RemoveStartedLevel(levelType, level);
                    }
                    break;
                default:
                    break;
            }
        }

#if UNITY_EDITOR
        protected void UnloadAllLoadedScenesExceptBootloader(System.Action onCompleted = null)
        {
            // First scene (bootloader)
            var bootloaderScene = SceneManager.GetSceneByBuildIndex(0);
            var sceneCount = SceneManager.sceneCount;

            // If there is a GameManager, so the unique loaded scene is the Bootloader
            if (sceneCount <= 1)
            {
                onCompleted?.Invoke();
                return;
            }

            List<int> buildIndexes = new List<int>();
            for (int i = 0; i <  sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if(scene.buildIndex == 0)
                    continue;

                buildIndexes.Add(scene.buildIndex);
            }

            UnloadSceneRecursive(buildIndexes, onCompleted);
        }

        protected void UnloadSceneRecursive(List<int> buildIndexes, System.Action onCompleted = null)
        {
            var asyncOp = SceneManager.UnloadSceneAsync(buildIndexes[0]);

            buildIndexes.RemoveAt(0);

            if (buildIndexes.Count == 0)
            {
                asyncOp.completed += (ao) => onCompleted?.Invoke();
            }
            else
            {
                asyncOp.completed += (ao) => {
                    UnloadSceneRecursive(buildIndexes, onCompleted);
                };
            }
        }
#endif

        protected virtual void RemoveStartedLevel(LevelType levelType, int level)
        {
            var instance = new LevelReference(){
                            levelType = levelType,
                            level = level
                        };
            if (m_startedLevels.Contains(instance))
            {
                m_startedLevels.Remove(instance);
            }
        }

        public virtual void UnloadLevelsExcept(LevelType levelType, params int[] levelIdxsToIgnore)
        {
            switch (levelType)
            {
                case LevelType.General:
                    for (int i = 0; i < m_levelLaunchers.Length; i++)
                    {
                        for (int j = 0; j < levelIdxsToIgnore.Length; j++)
                        {
                            if (levelIdxsToIgnore[j] == i)
                            {
                                break;
                            }

                            if (levelIdxsToIgnore.IsLast(j))
                            {
                                m_levelLaunchers[i].Unload();
                            }
                        }
                    }
                    break;
                case LevelType.GameMode:
                    for (int i = 0; i < m_playLaunchers.Length; i++)
                    {
                        for (int j = 0; j < levelIdxsToIgnore.Length; j++)
                        {
                            if (levelIdxsToIgnore[j] == i)
                            {
                                break;
                            }

                            if (levelIdxsToIgnore.IsLast(j))
                            {
                                m_playLaunchers[i].Unload();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public virtual void UnloadAllStartedLevels(System.Action onCompleted = null)
        {
            for (int i = 0; i < m_startedLevels.Count; i++)
            {
                if (onCompleted != null && i == 0)
                    UnloadLevel(m_startedLevels[i].levelType, m_startedLevels[i].level, onCompleted);
                else
                    UnloadLevel(m_startedLevels[i].levelType, m_startedLevels[i].level);
            }
        }
        #endregion

        #region ILevelScenesActiver Implemetation
        public virtual void ActivateScenes(LevelType levelType, int level)
        {
            switch (levelType)
            {
                case LevelType.General:
                    if (m_levelLaunchers.IsValidIndex(level))
                    {
                        m_levelLaunchers[level].ActivateScenes();
                    }
                    break;
                case LevelType.GameMode:
                    if (m_playLaunchers.IsValidIndex(level))
                    {
                        m_playLaunchers[level].ActivateScenes();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region IPlayFlowController Implementation
        public virtual PlayState GetPlayState(int playLauncherIndex)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                return m_playLaunchers[playLauncherIndex].PlayState;
            }

            return default;
        }

        public virtual PlayResult GetPlayResult(int playLauncherIndex)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                return m_playLaunchers[playLauncherIndex].PlayResult;
            }

            return default;
        }

        /// <summary>
        /// Toggle with PausePlay
        /// </summary>
        /// <param name="playLauncherIndex"></param>
        public virtual void StartPlay(int playLauncherIndex)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].Play();
            }
        }

        public virtual void PausePlay(int playLauncherIndex)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].Pause();
            }
        }

        public virtual void SwitchPausePlay(int playLauncherIndex)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].SwitchPause();
            }
        }

        public virtual void RestartPlay(bool useLoadingScreen = false, int playLauncherIndex = 0)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].Restart(useLoadingScreen);
            }
        }

        public virtual void SetGameOver(PlayResult gameResult, int playLauncherIndex = 0)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].SetGameOver(gameResult);
            }
        }
        #endregion

        #region IGameModeFlowController Implementation
        public virtual void MoveRestart(bool useLoadingScreen = false, int playLauncherIndex = 0)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].MoveRestart(useLoadingScreen);
            }
        }

        public virtual void MoveNextPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nextNodeIndex = -1)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].MoveNext(useLoadingScreen, nextNodeIndex);
            }
        }

        public virtual void MovePreviousPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int previousNodeIndex = -1)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].MovePrevious(useLoadingScreen, previousNodeIndex);
            }
        }

        public virtual void MoveToPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nodeIndex = 0)
        {
            if (m_playLaunchers.IsValidIndex(playLauncherIndex))
            {
                m_playLaunchers[playLauncherIndex].MoveTo(useLoadingScreen, nodeIndex);
            }
        }
        #endregion
    }
}