namespace d4160.UI
{
    using UnityEngine;

    public abstract class LoaderBase : MonoBehaviour, ILoader
    {
        protected virtual void Awake()
        {
            LoadingPrefabsManagerBase.Instance.SetInstanced(this);
        }

        public abstract void StartLoader();

        public abstract void StopLoader();
    }
}