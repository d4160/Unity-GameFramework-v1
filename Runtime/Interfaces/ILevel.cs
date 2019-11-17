namespace d4160.Levels
{
    public interface ILevel
    {
        void Load(System.Action onCompleted = null);

        void Unload(System.Action onCompleted = null);

        void ActivateScenes();
    }
}
