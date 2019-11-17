namespace d4160.Levels
{
    using d4160.GameFramework;
    using UnityEngine;

    public class LevelLoader : MonoBehaviour, ILevelLoader
    {
        [SerializeField] private bool _useLoadingScreen;

        public bool UseLoadingScreen { get => _useLoadingScreen; set => _useLoadingScreen = value; }

        public void LoadLevel(LevelType levelType, int level)
        {
            LoadLevel(levelType, level, null);
        }

        /// <summary>
        /// Load the level launcher (level index) of the assigned type.
        /// </summary>
        /// <param name="levelType"></param>
        /// <param name="level"></param>
        public void LoadLevel(LevelType levelType, int level, System.Action onCompleted)
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

        public void UnloadLevel(LevelType levelType, int level)
        {
            GameManager.Instance.UnloadLevel(levelType, level, null);
        }

        public void UnloadLevel(LevelType levelType, int level, System.Action onCompleted)
        {
            GameManager.Instance.UnloadLevel(levelType, level, onCompleted);
        }

        public void UnloadAllStartedLevels()
        {
            GameManager.Instance.UnloadAllStartedLevels(null);
        }

        public void UnloadAllStartedLevels(System.Action onCompleted)
        {
            GameManager.Instance.UnloadAllStartedLevels(onCompleted);
        }

        public void UnloadLevelsExcept(LevelType levelType, params int[] levelsToIgnore)
        {
            GameManager.Instance.UnloadLevelsExcept(levelType, levelsToIgnore);
        }

        public void LoadAfterUnloadLevel(LevelType unloadLevelType, int unloadLevel, LevelType loadLevelType, int loadLevel)
        {
            UnloadLevel(unloadLevelType, unloadLevel, () =>{
                LoadLevel(loadLevelType, loadLevel);
            });
        }

        public void UnloadAfterLoadLevel(LevelType loadLevelType, int loadLevel, LevelType unloadLevelType, int unloadLevel)
        {
            LoadLevel(loadLevelType, loadLevel, () =>{
                UnloadLevel(unloadLevelType, unloadLevel);
            });
        }
    }
}