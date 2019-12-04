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
        public override void Set(ScenesSerializableData data)
        {

        }

        public override ScenesSerializableData Get()
        {
            return null;
        }
    }
}