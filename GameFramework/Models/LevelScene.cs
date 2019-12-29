namespace d4160.GameFramework
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

    [System.Serializable]
    public struct SerializableScene
    {
        public int buildIndex;
        public string sceneName;
        public string scenePath;
        /// <summary>
        /// If this scene belongs to a World definition
        /// </summary>
        public int worldIdx;
    }
}
