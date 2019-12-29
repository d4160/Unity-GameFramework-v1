namespace d4160.GameFramework
{
  public interface IGameModeFlow
  {
    void MoveRestart(bool useLoadingScreen = false);

    void MoveNext(bool useLoadingScreen = false, int index = -1);

    void MovePrevious(bool useLoadingScreen = false, int index = -1);

    void MoveTo(bool useLoadingScreen = false, int index = 0);
  }
}