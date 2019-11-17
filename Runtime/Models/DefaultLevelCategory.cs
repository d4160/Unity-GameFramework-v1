namespace d4160.Levels
{
    using d4160.GameFramework;
    using d4160.Core;
    using UnityEngine;
    using UnityExtensions;

    [System.Serializable]
    public class DefaultLevelCategory : DefaultArchetype
    {
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        public ScriptableObject scenesSO;

        public string[] SceneNames
        {
            get
            {
                var iNames = scenesSO as IArchetypeNames;
                if (iNames != null)
                    return iNames.ArchetypeNames;

                return new string[0];
            }
        }

        public SceneReference GetScene(int index)
        {
            var eGetter = scenesSO as IElementGetter<SceneReference>;
            if (eGetter != null)
            {
                return eGetter.GetElementAt(index);
            }
            else
            {
                var wGetter = scenesSO as IElementGetter<Worlds.DefaultWorld>;
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