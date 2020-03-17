namespace d4160.GameFramework.Editor
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
            "ArchetypeEnum.cs", "GameModeEnum.cs", "WorldEnum.cs", "LevelCategoryEnum.cs", "PlayersEnum.cs", "LeaderboardsEnum.cs"
        };

        [MenuItem("Window/Game Framework/Tools/Generate GameData Constants", priority = 2000)]
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

                    if (i == 0)
                    {
                        ConstantsGeneratorKit.RebuildConstantsClass(ConstantsFiles[i], names, TotalConstantsFiles[i], AdditionalArchetypesContent());
                    }
                    else
                    {
                        ConstantsGeneratorKit.RebuildConstantsClass(ConstantsFiles[i], names, TotalConstantsFiles[i]);
                    }
                }
            }
        }

        static string AdditionalArchetypesContent()
        {
            return $@"
        internal static int GetFixed(int entity)
        {{
            if (entity >= 0 && entity < 4)
            {{
                return entity + 1;
            }}

            if (entity == 7)
                return 5;

            return 0;
        }}";
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