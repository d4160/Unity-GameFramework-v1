namespace d4160.Levels
{
    using d4160.GameFramework;
    using d4160.Core.Attributes;

    [System.Serializable]
    public struct LevelScene
    {
        [Dropdown(ValuesProperty = "LevelCategoryNames", IncludeNone = true)]
        public int levelCategory;
        [Dropdown(ValuesProperty = "LevelSceneNames")]
        public int levelScene;
    }
}
