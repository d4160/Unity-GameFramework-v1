namespace d4160.UI
{
    using d4160.Core;
    using d4160.GameFramework;
    using d4160.UI;
    using UnityEngine;
    using UnityExtensions;

    /// <summary>
    /// Manage prefabs of loading objects, like loading screens and loaders, can implement IFactory or IPool for creational purposes
    /// </summary>
    public abstract class LoadingPrefabsManagerBase : PrefabManagerBase<LoadingPrefabsManagerBase, LoaderBase>
    {
    }
}