namespace d4160.Levels
{
    public interface ILevelLoader
    {
        void LoadLevel(LevelType levelType, int level, System.Action onCompleted = null);

        void UnloadLevel(LevelType levelType, int level, System.Action onCompleted = null);

        void UnloadLevelsExcept(LevelType levelType, params int[] levelsToIgnore);

        void UnloadAllStartedLevels(System.Action onCompleted = null);
    }
}
