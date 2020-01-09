namespace d4160.DataPersistence
{
    public interface IAuthenticator
    {
        string Id { get; }
        /// <summary>

        void Login();

        void Logout();
    }
}