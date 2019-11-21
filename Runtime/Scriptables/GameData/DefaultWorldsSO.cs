namespace d4160.Worlds
{
    using d4160.GameFramework;
    using d4160.Core;
    using UnityEngine;
    using Malee;
  using UnityEngine.GameFoundation.DataPersistence;

  [CreateAssetMenu(fileName = "New DefaultWorlds_SO", menuName = "Game Framework/Game Data/DefaultWorlds")]
    public class DefaultWorldsSO : ArchetypesSOBase<WorldsReorderableArray, DefaultWorld>
    {
#if UNITY_EDITOR
        public string[] GetSceneNames(int worldIdx)
        {
            if (m_elements.IsValidIndex(worldIdx))
            {
                var element = m_elements[worldIdx];
                return element.SceneNames;
            }

            return new string[0];
        }
#endif

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

        public override void FillFromSerializableData(ISerializableData data)
        {
        }

        public override ISerializableData GetSerializableData()
        {
            return null;
        }

        public override void InitializeData(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }
    }
}