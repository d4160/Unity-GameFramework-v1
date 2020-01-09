namespace d4160.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;

    public interface  IGenericSerializableData : ISerializableData
    {
        BaseSerializableData[] SerializableData { get; set; }
    }
}