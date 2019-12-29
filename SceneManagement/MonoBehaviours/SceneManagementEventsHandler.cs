namespace d4160.Systems.SceneManagement
{
    using UnityEngine;
    using System;
    using UnityEngine.Events;
    using UnityEngine.SceneManagement;

    public class SceneManagementEventsHandler : MonoBehaviour
    {
        public SceneLoadedEvent onSceneLoaded;
        public SceneUnloadedEvent onSceneUnloaded;
        public ActiveSceneChangedEvent onActiveSceneChanged;

        #region Methods: Unity
        protected virtual void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += SceneUnloaded;
            SceneManager.activeSceneChanged += ActiveSceneChanged;
        }

        protected virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.sceneUnloaded -= SceneUnloaded;
            SceneManager.activeSceneChanged -= ActiveSceneChanged;
        }
        #endregion

        #region Methods: Callbacks
        protected virtual void SceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            onSceneLoaded?.Invoke(scene, loadMode);
        }

        protected virtual void SceneUnloaded(Scene scene)
        {
            onSceneUnloaded?.Invoke(scene);
        }

        protected virtual void ActiveSceneChanged(Scene scene1, Scene scene2)
        {
            onActiveSceneChanged?.Invoke(scene1, scene2);
        }
        #endregion
    }

    #region Events Declarations
    [Serializable]
    public class SceneLoadedEvent : UnityEvent<Scene, LoadSceneMode>
    { }

    [Serializable]
    public class SceneUnloadedEvent : UnityEvent<Scene>
    { }

    [Serializable]
    public class ActiveSceneChangedEvent : UnityEvent<Scene, Scene>
    { }
    #endregion
}

