namespace d4160.Levels
{
    public enum LevelType
    {
        None,
        General,
        GameMode
    }

    public enum ChapterType
    {
        Play,
        Cinematic
    }

    public enum PlayResult
    {
        None = 0,
        Win = 1,
        Lose,
        Draw,
        Abandon
    }

    public enum PlayState
    {
        None = 0,
        Loading = 1,
        // After loading, waiting for an input, UI, etc. Non interactive tutorial. Character select UI and so on.
        Waiting,
        Playing,
        Paused,
        GameOver
    }
}