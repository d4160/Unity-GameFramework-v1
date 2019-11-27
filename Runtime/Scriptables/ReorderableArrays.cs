﻿namespace d4160.GameFramework
{
    using d4160.Levels;
    using d4160.Worlds;
    using Malee;

    [System.Serializable]
    public class ArchetypesReorderableArray : Malee.ReorderableArray<DefaultArchetype>
    {
    }

    [System.Serializable]
    public class LevelCategoriesReorderableArray : Malee.ReorderableArray<DefaultLevelCategory>
    {
    }

    [System.Serializable]
    public class WorldsReorderableArray : Malee.ReorderableArray<DefaultWorld>
    {
    }

    [System.Serializable]
    public class LeaderboardsReorderableArray : Malee.ReorderableArray<DefaultLeaderboard>
    {
    }

    [System.Serializable]
    public class PlayersReorderableArray : Malee.ReorderableArray<DefaultPlayer>
    {
    }
}
