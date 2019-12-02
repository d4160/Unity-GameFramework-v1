namespace d4160.GameFramework.Editors
{
    using Prime31Editor;
    using UnityEditor;

    /* Use a class like this to generate a custom constants file */
    /* TODO change to scriptable object editor script */

    public static class DefaultConstantsGenerator
    {
        public static string[] ConstantsFiles => new string[]{
            "Archetypes.cs", "GameModes.cs", "Worlds.cs", "LevelCategories.cs", "Leaderboards.cs", "Players.cs"
        };

        public static string[] TotalConstantsFiles => new string[]{
            "Total", "Total", "Total", "Total", "Total", "Total"
        };

        public static string[] EnumFiles => new string[]{
            "ArchetypeEnum.cs", "GameModeEnum.cs", "WorldEnum.cs", "LevelCategoryEnum.cs", "LeaderboardsEnum.cs", "PlayersEnum.cs"
        };

        [MenuItem("Window/Game Framework/Tools/Generate Game Data Constants", priority = 2000)]
        public static void GenerateConstantsAndEnums()
        {
            GenerateConstants();
            GenerateEnums();
        }

        static void GenerateConstants()
        {
            var data = GameFrameworkSettings.GameDatabase.GameData;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] is IArchetypeNames)
                {
                    var archetypeInterface = data[i] as IArchetypeNames;
                    var names = archetypeInterface.ArchetypeNames;

                    ConstantsGeneratorKit.RebuildConstantsClass(ConstantsFiles[i], names, TotalConstantsFiles[i]);
                }
            }
        }

        static void GenerateEnums()
        {
            var data = GameFrameworkSettings.GameDatabase.GameData;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] is IArchetypeNames)
                {
                    var archetypeInterface = data[i] as IArchetypeNames;
                    var names = archetypeInterface.ArchetypeNames;

                    ConstantsGeneratorKit.RebuildEnumClass(EnumFiles[i], names);
                }
            }
        }
    }
}