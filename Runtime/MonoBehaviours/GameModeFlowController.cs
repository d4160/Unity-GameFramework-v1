namespace d4160.Levels
{
    using d4160.GameFramework;
    using UnityEngine;

    public class GameModeFlowController : MonoBehaviour, IGameModeFlowController
    {
        [SerializeField] private bool _useLoadingScreen;

        public bool UseLoadingScreen { get => _useLoadingScreen; set => _useLoadingScreen = value; }

        public void MoveRestart(bool useLoadingScreen = false, int playLauncherIndex = 0)
        {
            GameManager.Instance.MoveRestart(useLoadingScreen, playLauncherIndex);
        }

        public void MoveNextPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nextNodeIndex = -1)
        {
            GameManager.Instance.MoveNextPlay(useLoadingScreen, playLauncherIndex, nextNodeIndex);
        }

        public void MovePreviousPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int previousNodeIndex = -1)
        {
            GameManager.Instance.MovePreviousPlay(useLoadingScreen, playLauncherIndex, previousNodeIndex);
        }

        public void MoveToPlay(bool useLoadingScreen = false, int playLauncherIndex = 0, int nodeIndex = 0)
        {
            GameManager.Instance.MoveToPlay(useLoadingScreen, playLauncherIndex, nodeIndex);
        }

        public void MoveRestart(int playLauncherIndex = 0)
        {
            GameManager.Instance.MoveRestart(_useLoadingScreen, playLauncherIndex);
        }

        public void MoveNextPlay(int playLauncherIndex = 0, int nextNodeIndex = -1)
        {
            GameManager.Instance.MoveNextPlay(_useLoadingScreen, playLauncherIndex, nextNodeIndex);
        }

        public void MovePreviousPlay(int playLauncherIndex = 0, int previousNodeIndex = -1)
        {
            GameManager.Instance.MovePreviousPlay(_useLoadingScreen, playLauncherIndex, previousNodeIndex);
        }

        public void MoveToPlay(int playLauncherIndex = 0, int nodeIndex = 0)
        {
            GameManager.Instance.MoveToPlay(_useLoadingScreen, playLauncherIndex, nodeIndex);
        }
    }
}