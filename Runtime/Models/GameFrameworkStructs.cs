namespace d4160.GameFramework
{
    [System.Serializable]
    public struct LevelReference
    {
        public LevelType levelType;
        public int level;
    }

    public struct CategoryAndScene
    {
        public string scenePath;
        public string category;
    }
}
