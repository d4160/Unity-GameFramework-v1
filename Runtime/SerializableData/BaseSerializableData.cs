namespace d4160.GameFramework
{
    using UnityEngine.GameFoundation.DataPersistence;

    [System.Serializable]
    public abstract class BaseSerializableData : ISerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public BaseSerializableData()
        {
        }
    }
}