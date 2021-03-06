﻿namespace d4160.GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    public class LevelLoader : MonoBehaviour, ILevelLoader
    {
        [SerializeField] private bool _useLoadingScreen;

        public bool UseLoadingScreen { get => _useLoadingScreen; set => _useLoadingScreen = value; }

        public virtual void LoadLevel(LevelType levelType, int level)
        {
            LoadLevel(levelType, level, null);
        }

        /// <summary>
        /// Load the level launcher (level index) of the assigned type.
        /// </summary>
        /// <param name="levelType"></param>
        /// <param name="level"></param>
        public virtual void LoadLevel(LevelType levelType, int level, System.Action onCompleted)
        {
            if (!_useLoadingScreen)
            {
                GameManager.Instance.LoadLevel(levelType, level, onCompleted);
            }
            else
            {
                var loadingLauncher = GameManager.Instance.GetLevelLauncher<DefaultLoadingScreenLauncher>(0);
                loadingLauncher.SetLevelToLoad(levelType, level);

                // The LoadingScreen level is the first
                GameManager.Instance.LoadLevel(LevelType.General, 0, onCompleted);
            }
        }

        public virtual void UnloadLevel(LevelType levelType, int level)
        {
            GameManager.Instance.UnloadLevel(levelType, level, null);
        }

        public virtual void UnloadLevel(LevelType levelType, int level, System.Action onCompleted)
        {
            GameManager.Instance.UnloadLevel(levelType, level, onCompleted);
        }

        public virtual void UnloadAllStartedLevels()
        {
            GameManager.Instance.UnloadAllStartedLevels(null);
        }

        public virtual void UnloadAllStartedLevels(System.Action onCompleted)
        {
            GameManager.Instance.UnloadAllStartedLevels(onCompleted);
        }

        public virtual void UnloadLevelsExcept(LevelType levelType, params int[] levelsToIgnore)
        {
            GameManager.Instance.UnloadLevelsExcept(levelType, levelsToIgnore);
        }

        public virtual void LoadAfterUnloadLevel(LevelType unloadLevelType, int unloadLevel, LevelType loadLevelType, int loadLevel)
        {
            if (gameObject.transform.parent)
                gameObject.transform.SetParent(null);

            DontDestroyOnLoad(gameObject);

            UnloadLevel(unloadLevelType, unloadLevel, () =>{
                LoadLevel(loadLevelType, loadLevel);
                
                if(gameObject)
                    Destroy(gameObject);
            });
        }

        public virtual void UnloadAfterLoadLevel(LevelType loadLevelType, int loadLevel, LevelType unloadLevelType, int unloadLevel)
        {
            LoadLevel(loadLevelType, loadLevel, () =>{
                UnloadLevel(unloadLevelType, unloadLevel);
            });
        }

        /// <summary>
        /// For cases when don't use LoadingScreen to reload the level, instead use LoadAfterUnload using loading screen
        /// </summary>
        /// <param name="levelType"></param>
        /// <param name="level"></param>
        public virtual void RestartLevel(LevelType levelType, int level)
        {
            GameManager.Instance.RestartLevel(levelType, level);
        }

        /// <summary>
        /// This way ignore the SceneManagement System
        /// </summary>
        public virtual void RestartActivedLevel()
        {
            GameManager.Instance.RestartActivedLevel();
        }
    }
}