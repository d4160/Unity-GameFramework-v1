namespace d4160.SceneManagement
{
    using Core;

    public class DefaultSceneLoaderFactory : Singleton<DefaultSceneLoaderFactory>, IClassFactory<IAsyncSceneLoader>
    {
        public virtual IAsyncSceneLoader Create(SceneLoaderType option)
        {
            return Fabricate((int)option);
        }

        public virtual IAsyncSceneLoader Fabricate(int option = 0)
        {
            switch((SceneLoaderType)option)
            {
                case SceneLoaderType.UnityBuiltIn:
                    return new DefaultEmptySceneLoader();

                default: return default;
            }
        }
    }
}