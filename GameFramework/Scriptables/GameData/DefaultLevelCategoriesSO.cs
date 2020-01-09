namespace d4160.GameFramework
{
    using d4160.Core;
    using Malee;
    using System.Collections.Generic;
    using d4160.DataPersistence;

    //[CreateAssetMenu(fileName = "New LevelCategories_SO", menuName = "Game Framework/Game Data/LevelCategories")]
    public abstract class DefaultLevelCategoriesSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>, ISceneNamesGetter, ILevelSceneGetter
        where T1 : ReorderableArray<T2>
        where T2 : ILevelCategory, IArchetype, IArchetypeName, new()
        where T3 : BaseSerializableData
    {
        #region Editor Members
        public string[] GetSceneNames(int categoryIdx)
        {
            if (m_elements.IsValidIndex(categoryIdx))
            {
                var element = m_elements[categoryIdx];
                return m_elements[categoryIdx].SceneNames;
            }

            return new string[0];
        }

        public CategoryAndScene[] GetCategorizedScenes()
        {
            List<CategoryAndScene> scenes = new List<CategoryAndScene>();
            for(int i = 0; i < m_elements.Length; i++)
            {
                var element = m_elements[i];
                for(int j = 0; j < element.SceneCount; j++)
                {
                    var scene = element.GetScene(j);
                    if (scene == null) continue;

                    scenes.Add(new CategoryAndScene(){
                        category = element.Name,
                        scenePath = scene.ScenePath
                    });
                }
            }
            return scenes.ToArray();
        }
        #endregion

        public int GetSceneBuildIndex(LevelScene lScene)
        {
            var levelScene = GetSceneReference(lScene);

            return levelScene == null ? -1 : levelScene.SceneBuildIndex;
        }

        public string GetSceneName(LevelScene lScene)
        {
            var levelScene = GetSceneReference(lScene);

            return levelScene == null ? string.Empty : levelScene;
        }

        public string GetScenePath(LevelScene lScene)
        {
            var levelScene = GetSceneReference(lScene);

            return levelScene == null ? string.Empty : levelScene.ScenePath;
        }

        public SceneReference GetSceneReference(LevelScene lScene)
        {
            var levelCategory = GetElementAt(lScene.levelCategory);
            var levelScene = levelCategory?.GetScene(lScene.levelScene);

            return levelScene;
        }
    }
}