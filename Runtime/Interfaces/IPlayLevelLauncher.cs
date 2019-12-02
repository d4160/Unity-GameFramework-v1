namespace d4160.GameFramework
{
    public interface IPlayLevelLauncher : ILevelLauncher
    {
        PlayResult PlayResult { get; }
        PlayState PlayState { get; }

        void SetReadyToPlay();

        void Play();

        void Pause();

        void SwitchPause();

        void Restart(bool useLoadingScreen = false);

        void SetGameOver(PlayResult result);
    }
}
