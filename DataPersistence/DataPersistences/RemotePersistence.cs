namespace d4160.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class RemoteDataPersistence : BaseDataPersistence
    {
        protected string m_authenticationId;

        public string AuthenticationId => m_authenticationId;

        public RemoteDataPersistence(IDataSerializer serializer, string authenticationId) : base(serializer)
        {
            m_authenticationId = authenticationId;
        }
    }
}