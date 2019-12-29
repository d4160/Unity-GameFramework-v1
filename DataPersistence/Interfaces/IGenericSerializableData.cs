namespace d4160.Systems.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;

    public interface  IGenericSerializableData : ISerializableData
    {
        BaseSerializableData[] SerializableData { get; set; }
    }
}