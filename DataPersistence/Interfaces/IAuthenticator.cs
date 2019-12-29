namespace d4160.Systems.DataPersistence
{
    public interface IAuthenticator
    {
        string Id { get; }
        /// <summary>

        void Login();

        void Logout();
    }
}