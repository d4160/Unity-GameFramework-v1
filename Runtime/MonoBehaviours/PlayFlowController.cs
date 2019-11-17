namespace d4160.Levels
{
  using d4160.GameFramework;
  using UnityEngine;

    public class PlayFlowController : MonoBehaviour, IPlayFlowController
    {
        [SerializeField] private bool _useLoadingScreen;

        public bool UseLoadingScreen { get => _useLoadingScreen; set => _useLoadingScreen = value; }

        public PlayState GetPlayState(int playLauncherIndex)
        {
            return GameManager.Instance.GetPlayState(playLauncherIndex);
        }

        public PlayResult GetPlayResult(int playLauncherIndex)
        {
            return GameManager.Instance.GetPlayResult(playLauncherIndex);
        }

        /// <summary>
        /// Toggle with PausePlay
        /// </summary>
        /// <param name="playLauncherIndex"></param>
        public virtual void StartPlay(int playLauncherIndex)
        {
            GameManager.Instance.StartPlay(playLauncherIndex);
        }

        public virtual void SwitchPausePlay(int playLauncherIndex)
        {
            GameManager.Instance.SwitchPausePlay(playLauncherIndex);
        }

        public virtual void PausePlay(int playLauncherIndex)
        {
            GameManager.Instance.PausePlay(playLauncherIndex);
        }

        public virtual void RestartPlay(bool useLoadingScreen = false, int playLauncherIndex = 0)
        {
            GameManager.Instance.RestartPlay(useLoadingScreen, playLauncherIndex);
        }

        public virtual void RestartPlay(int playLauncherIndex = 0)
        {
            GameManager.Instance.RestartPlay(_useLoadingScreen, playLauncherIndex);
        }

        public void SetGameOver(PlayResult gameResult, int playLauncherIndex = 0)
        {
            GameManager.Instance.SetGameOver(gameResult, playLauncherIndex);
        }
    }
}