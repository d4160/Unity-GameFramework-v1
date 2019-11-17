namespace d4160.GameFramework
{
    using d4160.Levels;

    public interface ILevelScenesActiver
    {
        void ActivateScenes(LevelType levelType, int level);
    }
}