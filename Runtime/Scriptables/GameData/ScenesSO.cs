namespace d4160.Levels
{
    using d4160.Core;
    using d4160.GameFramework;
    using Malee;
    using UnityEngine;
  using UnityEngine.GameFoundation.DataPersistence;

  [CreateAssetMenu(fileName = "New Scenes_SO", menuName = "Game Framework/Game Data/Scenes", order = -1)]
    public class ScenesSO : ReorderableSO<ScenesReorderableArray, SceneReference>
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
        public override void FillFromSerializableData(ISerializableData data)
        {
            throw new System.NotImplementedException();
        }

        public override ISerializableData GetSerializableData()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeData(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }
    }

    [System.Serializable]
    public class ScenesReorderableArray : ReorderableArray<SceneReference>
    {
    }
}