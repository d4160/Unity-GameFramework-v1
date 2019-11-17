namespace d4160.Levels
{
    public interface IPlayLevel : ILevel
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
