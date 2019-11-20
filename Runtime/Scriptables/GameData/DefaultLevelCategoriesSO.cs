namespace d4160.Levels
{
    using d4160.GameFramework;
    using d4160.Core;
    using Malee;
    using UnityEngine;
  using UnityEngine.GameFoundation.DataPersistence;

  [CreateAssetMenu(fileName = "New DefaultLevelCategories_SO", menuName = "Game Framework/Game Data/DefaultLevelCategories")]
    public class DefaultLevelCategoriesSO : ArchetypesSOBase<LevelCategoriesReorderableArray, DefaultLevelCategory>
    {
        #region Editor Members
#if UNITY_EDITOR
        public string[] GetSceneNames(int categoryIdx)
        {
            if (m_elements.IsValidIndex(categoryIdx))
            {
                var element = m_elements[categoryIdx];
                return m_elements[categoryIdx].SceneNames;
            }

            return new string[0];
        }
#endif
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

        private SceneReference GetSceneReference(LevelScene lScene)
        {
            var levelCategory = GetElementAt(lScene.levelCategory);
            var levelScene = levelCategory?.GetScene(lScene.levelScene);

            return levelScene;
        }

        public override void FillFromSerializableData(ISerializableData data)
        {
        }

        public override ISerializableData GetSerializableData()
        {
            return null;
        }

        public override void Initialize(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }
    }
}