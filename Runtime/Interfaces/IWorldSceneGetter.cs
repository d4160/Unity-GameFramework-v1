namespace d4160.GameFramework
{
    public interface IWorldSceneGetter
    {
        int GetSceneBuildIndex(WorldScene wScene);

        string GetSceneName(WorldScene wScene);

        string GetScenePath(WorldScene wScene);

        SceneReference GetSceneReference(WorldScene wScene);
    }
}