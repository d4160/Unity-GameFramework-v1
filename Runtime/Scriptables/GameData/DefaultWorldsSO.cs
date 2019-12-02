namespace d4160.GameFramework
{
  using System.Collections.Generic;
  using d4160.Core;
    using Malee;

    //[CreateAssetMenu(fileName = "New Worlds_SO", menuName = "Game Framework/Game Data/Worlds")]
    public abstract class DefaultWorldsSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>, ISceneNamesGetter
        where T1 : ReorderableArray<T2>
        where T2 : IArchetype, ILevelCategory, new()
        where T3 : BaseSerializableData
    {
        public string[] GetSceneNames(int worldIdx)
        {
            if (m_elements.IsValidIndex(worldIdx))
            {
                var element = m_elements[worldIdx];
                return element.SceneNames;
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
                    scenes.Add(new CategoryAndScene(){
                        category = element.Name,
                        scenePath = scene.ScenePath
                    });
                }
            }
            return scenes.ToArray();
        }

        public int GetSceneBuildIndex(WorldScene wScene)
        {
            var worldScene = GetSceneReference(wScene);

            return worldScene == null ? -1 : worldScene.SceneBuildIndex;
        }

        public string GetSceneName(WorldScene wScene)
        {
            var worldScene = GetSceneReference(wScene);

            return worldScene == null ? string.Empty : worldScene;
        }

        public string GetScenePath(WorldScene wScene)
        {
            var worldScene = GetSceneReference(wScene);

            return worldScene == null ? string.Empty : worldScene.ScenePath;
        }

        private SceneReference GetSceneReference(WorldScene wScene)
        {
            var world = GetElementAt(wScene.world);
            var worldScene = world?.GetScene(wScene.worldScene);

            return worldScene;
        }
    }
}