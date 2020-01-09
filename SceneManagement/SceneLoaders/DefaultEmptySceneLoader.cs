namespace d4160.SceneManagement
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class DefaultEmptySceneLoader : IAsyncSceneLoader
    {
        public virtual bool NetworkingSyncLoad { get; } = false;

        protected List<int> m_loadedScenes = new List<int>();

        public virtual void LoadSceneAsync(
            int buildIdx,
            bool setActiveAsMainScene = false,
            Action<AsyncOperation> onStarted = null,
            Action onCompleted = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null)
        {
            if (buildIdx == -1)
                return;

            if (!m_loadedScenes.Contains(buildIdx))
                m_loadedScenes.Add(buildIdx);
            else
                return;

            //Debug.Log($"Networking load: {NetworkingSyncLoad}");
            SceneManagementSingleton.Instance.NetworkingSyncLoad = NetworkingSyncLoad;
            SceneManagementSingleton.Instance.LoadSceneAsync(
                buildIdx, setActiveAsMainScene, onStarted,
                onCompleted, allowSceneActivation, onProgress);
        }

        public virtual void UnloadSceneAsync(
            int buildIdx,
            Action onCompleted = null)
        {
            if (buildIdx == -1)
                return;

            if (SceneManagementSingleton.IsSceneInBackground(buildIdx))
                return;

            m_loadedScenes.Remove(buildIdx);

            SceneManagementSingleton.UnloadSceneAsync(buildIdx, onCompleted);
        }

        public virtual void UnloadAllLoadedScenes(Action onCompleted = null)
        {
            if (m_loadedScenes.Count == 0)
            {
                onCompleted?.Invoke();
                return;
            }

            for (int i = m_loadedScenes.Count - 1; i >= 0; i--)
            {
                if (onCompleted != null && i == 0)
                    UnloadSceneAsync(m_loadedScenes[i], onCompleted);
                else
                    UnloadSceneAsync(m_loadedScenes[i]);
            }
        }
    }
}