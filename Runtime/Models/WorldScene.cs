namespace d4160.GameFramework
{
    using d4160.Core.Attributes;

    [System.Serializable]
    public struct WorldScene
    {
        [Dropdown(ValuesProperty = "WorldNames", IncludeNone = true)]
        public int world;
        [Dropdown(ValuesProperty = "WorldSceneNames")]
        public int worldScene;
    }
}
