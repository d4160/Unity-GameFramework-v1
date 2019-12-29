namespace d4160.GameFramework
{
    using d4160.Core;
    using UnityEngine;
    using UnityExtensions;
    using UnityEngine.Serialization;
    using System.Collections.Generic;

    [System.Serializable]
    public class DefaultLevelCategory : DefaultArchetype, ILevelCategory
    {
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        [FormerlySerializedAs("scenesSO")]
        [SerializeField] protected ScriptableObject m_scenesSO;

        public ScriptableObject ScenesSO => m_scenesSO;

        public int SceneCount => SceneNames.Length;

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
            /*else
            {
                var wGetter = ScenesSO as IElementGetter<DefaultWorld>;
                if (wGetter != null)
                {
                    var world = wGetter.GetElementAt(index);
                    if (world != null)
                    {
                        return world.GetScene(0);
                    }
                }
            }*/

            return null;
        }
    }

    [System.Serializable]
    public class DefaultSerializableLevelCategory : DefaultArchetype
    {
        public SerializableScene[] scenes;
    }
}