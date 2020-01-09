namespace d4160.SceneManagement
{
    using Core;

    public class DefaultSceneLoaderFactory : Singleton<DefaultSceneLoaderFactory>, IFactory<IAsyncSceneLoader>
    {
        public virtual IAsyncSceneLoader Create(SceneLoaderType option)
        {
            return Create((int)option);
        }

        public virtual IAsyncSceneLoader Create(int option = 0)
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