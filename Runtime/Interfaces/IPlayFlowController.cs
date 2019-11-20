namespace d4160.GameFramework
{
  public interface IPlayFlowController
  {
    PlayState GetPlayState(int playLauncherIndex);

    PlayResult GetPlayResult(int playLauncherIndex);

    void StartPlay(int playLauncherIndex);

    void SwitchPausePlay(int playLauncherIndex);

    void PausePlay(int playLauncherIndex);

    void RestartPlay(bool useLoadingScreen = false, int playLauncherIndex = 0);

    void SetGameOver(PlayResult result, int playLauncherIndex = 0);
  }
}