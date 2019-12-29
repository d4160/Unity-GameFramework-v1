namespace d4160.Systems.SceneManagement
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System;
    using System.Collections;
    using d4160.Loops;
    using d4160.Core;

    public delegate void AsyncOperationProgress(float progress);

    public class SceneManagementSingleton : Singleton<SceneManagementSingleton>
    {
        #region Fields
        protected AsyncOperation m_asyncLoadOperation;
        #endregion

        #region Action and Events
        /// <summary>
        /// To store scene load callbacks to start after the current ends
        /// </summary>
        protected event Action m_onAsyncLoadOperationHelper;
        protected event AsyncOperationProgress m_onAsyncLoadOperationProgress;
        #endregion

        #region Unity Callbacks
        protected virtual void OnEnable()
        {
            UpdateLoop.OnUpdate += OnUpdate;
        }

        protected virtual void OnDisable()
        {
            UpdateLoop.OnUpdate -= OnUpdate;
        }
        #endregion

        #region Loop Methods
        protected void OnUpdate(float deltaTime)
        {
            if (m_asyncLoadOperation != null)
            {
                Debug.Log($"AsyncOp allowed?{m_asyncLoadOperation.allowSceneActivation}");
                m_onAsyncLoadOperationProgress?.Invoke(m_asyncLoadOperation.progress);
            }
        }
        #endregion

        #region Load Methods
        public static void LoadScene(
            int sceneBuildIdx,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (!IsSceneLoadedOrInBackground(sceneBuildIdx))
            {
                Instance.LoadSceneInternal(sceneBuildIdx, mode);
            }
        }

        public static void LoadScene(
            string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (!IsSceneLoadedOrInBackground(sceneName))
            {
                Instance.LoadSceneInternal(sceneName, mode);
            }
        }

        protected virtual void LoadSceneInternal(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, mode);
        }

        protected virtual void LoadSceneInternal(int sceneBuildIdx, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneBuildIdx, mode);
        }

        /// <summary>
        /// Force to load the scene, so used to call more than one scene at same time
        /// </summary>
        /// <param name="sceneBuildIdx"></param>
        /// <param name="mode"></param>
        /// <param name="onComplete"></param>
        /// <param name="allowSceneActivation"></param>
        public static void ForceLoadSceneAsync(
            int sceneBuildIdx,
            LoadSceneMode mode = LoadSceneMode.Single,
            Action<AsyncOperation> onComplete = null,
            bool allowSceneActivation = true)
        {
            if (!IsSceneLoadedOrInBackground(sceneBuildIdx))
            {
                var ao = Instance.LoadSceneAsyncInternal(sceneBuildIdx, mode);
                ao.allowSceneActivation = allowSceneActivation;

                if (onComplete != null)
                    ao.completed += onComplete;
            }
            else
            {
                onComplete?.Invoke(null);
            }
        }

        /// <summary>
        /// Force to load the scene, so used to call more than one scene at same time
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="mode"></param>
        /// <param name="onComplete"></param>
        /// <param name="allowSceneActivation"></param>
        public static void ForceLoadSceneAsync(
            string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single,
            Action<AsyncOperation> onComplete = null,
            bool allowSceneActivation = true)
        {
            if (!IsSceneLoadedOrInBackground(sceneName))
            {
                var ao = Instance.LoadSceneAsyncInternal(sceneName, mode);
                ao.allowSceneActivation = allowSceneActivation;

                if (onComplete != null)
                    ao.completed += onComplete;
            }
            else
            {
                onComplete?.Invoke(null);
            }
        }

        protected virtual AsyncOperation LoadSceneAsyncInternal(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(sceneName, mode);
        }

        protected virtual AsyncOperation LoadSceneAsyncInternal(int sceneBuildIdx, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(sceneBuildIdx, mode);
        }

        public virtual bool LoadSceneAsync(
            int sceneBuildIdx,
            LoadSceneMode mode = LoadSceneMode.Single,
            Action<AsyncOperation> onStarted = null,
            Action onComplete = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null)
        {
            if (!IsSceneLoadedOrInBackground(sceneBuildIdx))
            {
                Debug.Log($"Try to load scene {sceneBuildIdx}. AsynOp null? {m_asyncLoadOperation == null}");
                if (m_asyncLoadOperation == null)
                {
                    StartCoroutine(LoadSceneCo(sceneBuildIdx, mode));

                    LoadSceneAsync(onStarted, onComplete, allowSceneActivation, onProgress);

                    m_onAsyncLoadOperationHelper = null;

                    return true;
                }
                else
                {
                    m_onAsyncLoadOperationHelper += () => {
                        LoadSceneAsync(sceneBuildIdx, mode, onStarted, onComplete, allowSceneActivation, onProgress);
                    };
                }
            }
            else
            {
                onComplete?.Invoke();
            }

            return false;
        }

        protected virtual IEnumerator LoadSceneCo(int sceneBuildIdx, LoadSceneMode mode)
        {
            yield return m_asyncLoadOperation = LoadSceneAsyncInternal(sceneBuildIdx, mode);
        }

        public virtual bool LoadSceneAsync(
            string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single,
            Action<AsyncOperation> onStarted = null,
            Action onComplete = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null)
        {
            if (!IsSceneLoadedOrInBackground(sceneName))
            {
                if (m_asyncLoadOperation == null)
                {
                    StartCoroutine(LoadSceneCo(sceneName, mode));

                    LoadSceneAsync(onStarted, onComplete, allowSceneActivation, onProgress);

                    m_onAsyncLoadOperationHelper = null;

                    return true;
                }
                else
                {
                    m_onAsyncLoadOperationHelper += () => {
                        LoadSceneAsync(sceneName, mode, onStarted, onComplete, allowSceneActivation, onProgress);
                    };
                }
            }
            else
            {
                onComplete?.Invoke();
            }

            return false;
        }

        protected virtual IEnumerator LoadSceneCo(string sceneName, LoadSceneMode mode)
        {
            yield return m_asyncLoadOperation = LoadSceneAsyncInternal(sceneName, mode);
        }

        int _buildIndexTemp;

        public virtual bool LoadSceneAsync(
            int buildIndex,
            bool setActiveAsMainScene = false,
            Action<AsyncOperation> onStarted = null,
            Action onCompleted = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null,
            LoadSceneMode mode = LoadSceneMode.Additive)
        {
            _buildIndexTemp = buildIndex;
            Debug.Log($"Load {buildIndex} index scene as mainscene: {setActiveAsMainScene}. Allow activation: {allowSceneActivation}");
            if (setActiveAsMainScene)
            {
                return LoadSceneAsync(buildIndex, mode, onStarted, () =>
                {
                    if (setActiveAsMainScene)
                        SceneManagementSingleton.SetActiveScene(buildIndex);

                    onCompleted?.Invoke();
                },
                allowSceneActivation, onProgress);
            }
            else
            {
                return LoadSceneAsync(buildIndex, mode, onStarted,
                    () => onCompleted?.Invoke(),
                    allowSceneActivation, onProgress);
            }
        }

        public virtual bool LoadSceneAsync(
            string sceneName,
            bool setActiveAsMainScene = false,
            Action<AsyncOperation> onStarted = null,
            Action onCompleted = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null,
            LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (setActiveAsMainScene)
            {
                return LoadSceneAsync(sceneName, mode, onStarted, () =>
                {
                    if (setActiveAsMainScene)
                        SceneManagementSingleton.SetActiveScene(sceneName);

                    onCompleted?.Invoke();
                },
                allowSceneActivation, onProgress);
            }
            else
            {
                return LoadSceneAsync(sceneName, mode, onStarted,
                    () => onCompleted?.Invoke(),
                    allowSceneActivation, onProgress);
            }
        }

        protected virtual void LoadSceneAsync(
            Action<AsyncOperation> onStarted = null,
            Action onComplete = null,
            bool allowSceneActivation = true,
            AsyncOperationProgress onProgress = null)
        {
            m_asyncLoadOperation.allowSceneActivation = allowSceneActivation;

            if (onComplete != null)
            {
                m_asyncLoadOperation.completed += (ao) =>
                {
                    Debug.Log($"Scene {_buildIndexTemp} is loaded.");
                    m_asyncLoadOperation = null;
                    m_onAsyncLoadOperationProgress = null;

                    m_onAsyncLoadOperationHelper?.Invoke();
                    onComplete.Invoke();
                };
            }

            if (onProgress != null)
                m_onAsyncLoadOperationProgress += onProgress;

            onStarted?.Invoke(m_asyncLoadOperation);
        }
        #endregion

        #region Unload Methods
        public static void UnloadSceneAsync(
            int sceneBuildIdx,
            Action onComplete = null)
        {
            if (IsSceneLoaded(sceneBuildIdx))
            {
                Instance.StartCoroutine(UnloadSceneCo(sceneBuildIdx, onComplete));
            }
        }

        public static void UnloadSceneAsync(
            string sceneName,
            Action onComplete = null)
        {
            if (IsSceneLoaded(sceneName))
            {
                Instance.StartCoroutine(UnloadSceneCo(sceneName, onComplete));
            }
        }

        protected static IEnumerator UnloadSceneCo(
            int sceneBuildIdx,
            Action onComplete = null)
        {
            var ao = SceneManager.UnloadSceneAsync(sceneBuildIdx);

            if (onComplete != null)
                ao.completed += (param) => onComplete?.Invoke();

            yield return ao;
        }

        protected static IEnumerator UnloadSceneCo(
            string sceneName,
            Action onComplete = null)
        {
            var ao = SceneManager.UnloadSceneAsync(sceneName);

            if (onComplete != null)
                ao.completed += (param) => onComplete?.Invoke();

            yield return ao;
        }
        #endregion

        #region Active Scene Methods
        public static void SetActiveScene(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            if (scene.isLoaded)
                SetActiveScene(scene);
        }

        public static void SetActiveScene(string sceneName)
        {
            var scene = GetScene(sceneName);

            if (scene.isLoaded)
                SetActiveScene(scene);
        }

        public static void SetActiveScene(Scene scene)
        {
            if (string.IsNullOrEmpty(scene.name)) return;

            SceneManager.SetActiveScene(scene);
        }

        public static bool IsSceneLoadedOrInBackground(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            return scene.IsValid();
        }

        public static bool IsSceneLoadedOrInBackground(string sceneName)
        {
            var scene = GetScene(sceneName);

            return scene.IsValid();
        }

        public static bool IsSceneLoaded(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            return scene.isLoaded;
        }

        public static bool IsSceneLoaded(string sceneName)
        {
            var scene = GetScene(sceneName);

            return scene.isLoaded;
        }

        public static bool IsSceneInBackground(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            return scene.IsValid() && !scene.isLoaded;
        }

        public static bool IsSceneInBackground(string sceneName)
        {
            var scene = GetScene(sceneName);

            return scene.IsValid() && !scene.isLoaded;
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Get loaded scene by build index
        /// </summary>
        /// <param name="buildIdx"></param>
        /// <returns></returns>
        public static Scene GetScene(int buildIdx)
        {
            if (buildIdx == -1) return default;

            var scene = SceneManager.GetSceneByBuildIndex(buildIdx);

            return scene;
        }

        public static Scene GetScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return default;

            var scene = SceneManager.GetSceneByName(sceneName);

            return scene;
        }

        public static string GetSceneName(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            return scene.name;
        }

        public static string GetScenePath(int buildIdx)
        {
            var scene = GetScene(buildIdx);

            return scene.path;
        }
        #endregion
    }
}
