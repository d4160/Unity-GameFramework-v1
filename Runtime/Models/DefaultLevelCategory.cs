namespace d4160.Levels
{
    using d4160.GameFramework;
    using d4160.Core;
    using UnityEngine;
    using UnityExtensions;
    using UnityEngine.Serialization;

    [System.Serializable]
    public class DefaultLevelCategory : DefaultArchetype
    {
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        [FormerlySerializedAs("scenesSO")]
        [SerializeField] protected ScriptableObject m_scenesSO;

        public ScriptableObject ScenesSO => m_scenesSO;

        public string[] SceneNames
        {
            get
            {
                var iNames = ScenesSO as IArchetypeNames;
                if (iNames != null)
                    return iNames.ArchetypeNames;

                return new string[0];
            }
        }

        public SceneReference GetScene(int index)
        {
            var eGetter = ScenesSO as IElementGetter<SceneReference>;
            if (eGetter != null)
            {
                return eGetter.GetElementAt(index);
            }
            else
            {
                var wGetter = ScenesSO as IElementGetter<Worlds.DefaultWorld>;
                if (wGetter != null)
                {
                    var world = wGetter.GetElementAt(index);
                    if (world != null)
                    {
                        return world.GetScene(0);
                    }
                }
            }

            return null;
        }
    }
}