namespace d4160.GameFramework
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

    public enum LeaderboardAggregationMethod
    {
        // Always update with the new value
        Last,
        // Always use the lowest value
        Minimum,
        // Always use the highest value
        Maximun,
        // Add this valueto the existing value
        Sum
    }

    public enum LeaderboardResetFrequency
    {
        Manually,
        Hourly,
        Daily,
        Weekly,
        Monthly
    }
}