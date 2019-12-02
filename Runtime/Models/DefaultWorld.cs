namespace d4160.GameFramework
{
    using d4160.GameFramework;
    using d4160.Core;
    using Malee;
    using UnityEngine;

    [System.Serializable]
    public class DefaultWorld : DefaultArchetype, ILevelCategory
    {
        [Reorderable(paginate = true, pageSize = 10)]
        [SerializeField] protected ScenesReorderableArray m_scenes;

        public ScenesReorderableArray Scenes => m_scenes;

        public int SceneCount => m_scenes.Length;

        public string[] SceneNames {
            get {
                var names = new string[m_scenes.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = m_scenes[i].SceneAsset.name;
                }
                return names;
            }
        }

        public SceneReference GetScene(int index)
        {
            if (m_scenes.IsValidIndex(index))
            {
                return m_scenes[index];
            }

            return null;
        }
    }
}