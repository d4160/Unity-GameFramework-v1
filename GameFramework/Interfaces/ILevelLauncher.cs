namespace d4160.GameFramework
{
    public interface ILevelLauncher
    {
        void Load(System.Action onCompleted = null);

        void Unload(System.Action onCompleted = null);

        void ActivateScenes();
    }
}
