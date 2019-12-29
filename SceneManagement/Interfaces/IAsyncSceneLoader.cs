namespace d4160.Systems.SceneManagement
{
    using System;
    using UnityEngine;

    public interface IAsyncSceneLoader
    {
        void LoadSceneAsync(int buildIdx, bool setActiveAsMainScene = false, Action<AsyncOperation> onStarted = null, Action onCompleted = null, bool allowSceneActivation = true, AsyncOperationProgress onProgress = null);

        void UnloadSceneAsync(int buildIdx, Action onCompleted = null);

        void UnloadAllLoadedScenes(Action onCompleted = null);
    }
}
