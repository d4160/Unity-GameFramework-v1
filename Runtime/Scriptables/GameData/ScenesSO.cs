namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.GameFramework;
    using Malee;
    using UnityEngine;
  using UnityEngine.GameFoundation.DataPersistence;

  [CreateAssetMenu(fileName = "New Scenes_SO", menuName = "Game Framework/Game Data/Scenes", order = -1)]
    public class ScenesSO : ReorderableSO<ScenesReorderableArray, SceneReference, ScenesSerializableData>
#if UNITY_EDITOR
        , IArchetypeNames
#endif
    {
#if UNITY_EDITOR
        public string[] ArchetypeNames {
            get {
                var names = new string[m_elements.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = m_elements[i].SceneAsset.name;
                }

                return names;
            }
        }
#endif
        protected override void FillFromSerializableData(ScenesSerializableData data)
        {

        }

        protected override ScenesSerializableData GetSerializableDataGeneric()
        {
            return null;
        }
    }
}