namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultArchetypes_SO.asset", menuName = "Game Framework/Game Data/Default Archetypes")]
    public class DefaultArchetypesSO : ArchetypesSOBase<ArchetypesReorderableArray, DefaultArchetype>
    {
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