namespace d4160.Systems.SceneManagement
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class DefaultEmptySceneLoader : IAsyncSceneLoader
    {
        private List<int> _loadedScenes = new List<int>();

        public void LoadSceneAsync(
            int buildIdx,
            bool setActiveAsMainScene = false,
            Action<AsyncOperation> onStarted = null,
            Action onCompleted = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null)
        {
            if (buildIdx == -1)
                return;

            if (!_loadedScenes.Contains(buildIdx))
                _loadedScenes.Add(buildIdx);
            else
                return;

            SceneManagementSingleton.Instance.LoadSceneAsync(
                buildIdx, setActiveAsMainScene, onStarted,
                onCompleted, allowSceneActivation, onProgress);
        }

        public void UnloadSceneAsync(
            int buildIdx,
            Action onCompleted = null)
        {
            if (buildIdx == -1)
                return;

            if (SceneManagementSingleton.IsSceneInBackground(buildIdx))
                return;

            _loadedScenes.Remove(buildIdx);

            SceneManagementSingleton.UnloadSceneAsync(buildIdx, onCompleted);
        }

        public void UnloadAllLoadedScenes(Action onCompleted = null)
        {
            for (int i = _loadedScenes.Count - 1; i >= 0; i--)
            {
                if (onCompleted != null && i == 0)
                    UnloadSceneAsync(_loadedScenes[i], onCompleted);
                else
                    UnloadSceneAsync(_loadedScenes[i]);
            }
        }
    }
}