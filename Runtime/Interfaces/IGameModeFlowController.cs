namespace d4160.GameFramework
{
  public interface IGameModeFlowController
  {
    void MoveRestart(bool useLoadingScreen = false, int playLauncherIndex = 0);

    void MoveNextPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nextNodeIndex = -1);

    void MovePreviousPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int previousNodeIndex = -1);

    void MoveToPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nodeIndex = 0);
  }
}