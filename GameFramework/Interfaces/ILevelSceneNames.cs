namespace d4160.GameFramework
{
    public interface ILevelSceneNames
    {
        string[] LevelCategoryNames { get; }
        string[] LevelSceneNames { get; }
    }

    public interface ISceneNamesGetter
    {
        string[] GetSceneNames(int catIdx);

        CategoryAndScene[] GetCategorizedScenes();
    }
}
