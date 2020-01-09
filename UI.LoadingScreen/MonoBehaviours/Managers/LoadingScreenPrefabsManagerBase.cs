namespace d4160.UI.Loading
{
    using Core;

    /// <summary>
    /// Manage prefabs of loading objects, like loading screens and loaders, can implement IFactory or IPool for creational purposes
    /// </summary>
    public abstract class LoadingScreenPrefabsManagerBase : PrefabManagerBase<LoadingScreenPrefabsManagerBase, LoadingScreenBase>
    {
    }
}