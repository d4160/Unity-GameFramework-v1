namespace d4160.GameFramework
{
    using Malee;

    public abstract class PlayLevelLauncher : LevelLauncher, IPlayLevelLauncher, IGameModeFlow
    {
        protected PlayResult m_playResult = PlayResult.None;
        protected PlayState m_playState = PlayState.None;

        public PlayResult PlayResult => m_playResult;
        public PlayState PlayState => m_playState;

        public abstract void SetReadyToPlay();

        public abstract void Play();

        public abstract void SwitchPause();

        public abstract void Pause();

        public abstract void Restart(bool useLoadingScreen = false);

        public abstract void SetGameOver(PlayResult result);

        public abstract void MoveRestart(bool useLoadingScreen = false);

        public abstract void MoveNext(bool useLoadingScreen = false, int index = -1);

        public abstract void MovePrevious(bool useLoadingScreen = false, int index = -1);

        public abstract void MoveTo(bool useLoadingScreen = false, int index = 0);
    }

    [System.Serializable]
    public class PlayLevelLauncherReorderableArray : ReorderableArrayForUnityObject<PlayLevelLauncher>
    { }
}