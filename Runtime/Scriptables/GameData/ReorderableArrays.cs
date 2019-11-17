namespace d4160.GameFramework
{
    using d4160.Levels;
    using d4160.Worlds;
    using Malee;

    [System.Serializable]
    public class ArchetypesReorderableArray : ReorderableArray<DefaultArchetype>
    {
    }

    [System.Serializable]
    public class LevelCategoriesReorderableArray : ReorderableArray<DefaultLevelCategory>
    {
    }

    [System.Serializable]
    public class WorldsReorderableArray : ReorderableArray<DefaultWorld>
    {
    }
}
