namespace d4160.GameFramework
{
    public interface ILevelCategory
    {
        string[] SceneNames { get; }

        SceneReference GetScene(int index);

        int SceneCount { get; }
    }
}
