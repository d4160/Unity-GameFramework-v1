using System;
using d4160.DataPersistence;

namespace UnityEngine.GameFoundation.DataPersistence
{
    /// <summary>
    /// Base persistence class derived from IDataPersistence
    /// </summary>
    public abstract class GenericBaseDataPersistence_prev : IGenericDataPersistence_prev
    {
        IDataSerializer m_Serializer;

        /// <summary>
        /// The serialization layer used by the processes of this persistence.
        /// </summary>
        protected IDataSerializer serializer
        {
            get { return m_Serializer; }
        }

        /// <summary>
        /// Basic constructor that takes in a data serializer which this will use.
        /// </summary>
        /// <param name="serializer">The data serializer to use.</param>
        public GenericBaseDataPersistence_prev(IDataSerializer serializer)
        {
            m_Serializer = serializer;
        }

        /// <inheritdoc />
        public abstract void Load(string identifier, Action<ISerializableData> onLoadCompleted = null, Action<Exception> onLoadFailed = null);

        /// <inheritdoc />
        public abstract void Save(string identifier, ISerializableData content, Action onSaveCompleted = null, Action<Exception> onSaveFailed = null);
    }
}