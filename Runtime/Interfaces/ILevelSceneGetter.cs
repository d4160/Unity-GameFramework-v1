namespace d4160.GameFramework
{
    public interface ILevelSceneGetter
    {
        int GetSceneBuildIndex(LevelScene lScene);

        string GetSceneName(LevelScene lScene);

        string GetScenePath(LevelScene lScene);

        SceneReference GetSceneReference(LevelScene lScene);
    }
}