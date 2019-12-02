namespace d4160.GameFramework.Editors
{
    using Prime31Editor;
    using UnityEditor;

    /* Use a class like this to generate a custom constants file */
    /* TODO change to scriptable object editor script */

    public static class DefaultArchetypesCreator
    {
        public static string[][] DefaultGameData => new string[][]{
            new string[]{
                "GameMode", "World", "Level", "Player",
                "Camera", "Character", "UI", "Leaderboard"
            },
            new string[]{
                "Default Mode"
            },
            new string[]{
                "World 1"
            },
            new string[]{
                "BootLoader", "LoadingScreenLogic", "MenuLogic",
                "CreditsLogic", "World", "CinematicsLogic", "PlayLogic"
            },
            new string[]{
                "Player1", "Player2"
            },
            new string[]{
                "Highscore", "Best time"
            }
        };

        [MenuItem("Window/Game Framework/Tools/Create Default GameData", priority = 2000)]
        public static void CreateDefaultGameData()
        {
            var gameDatabase = GameFrameworkSettings.GameDatabase.GameData;
            for (int i = 0; i < DefaultGameData.Length; i++)
            {
                var scriptable = gameDatabase[i];

                if (scriptable is IArchetypeOperations)
                {
                    var data = DefaultGameData[i];
                    var ops = scriptable as IArchetypeOperations;
                    ops.Clear();
                    ops.AddRange(data);
                }
            }
        }
    }
}