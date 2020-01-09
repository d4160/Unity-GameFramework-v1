using d4160.Loops;

namespace d4160.UI.Loading
{
    using System;
    using Core;
    using SceneManagement;
    using UnityEngine;

    public abstract class LoadingScreenBase : Singleton<LoadingScreenBase>, ILoadingScreen
    {
        public static event AsyncOperationProgress OnLoadingProgress;
        public static event Action OnLoadCompleted;

        protected float m_elapsedLoadingTime;
        protected float m_sceneAsyncLoadingProgress;
        protected bool m_sceneAsyncLoadCompleted;
        protected bool m_loading;

        protected virtual bool ReadyToContinue => m_sceneAsyncLoadCompleted;

        protected virtual void OnEnable()
        {
            UpdateLoop.OnUpdate += UpdateCallback;
        }

        protected virtual void OnDisable()
        {
            UpdateLoop.OnUpdate -= UpdateCallback;
        }

        public virtual void StartLoad()
        {
            m_loading = true;
        }

        protected virtual void UpdateCallback(float dt)
        {
            if (!m_loading) return;
            
            UpdateElapsedLoadingTime();
        }

        protected void UpdateElapsedLoadingTime()
        {
            m_elapsedLoadingTime += Time.deltaTime;
        }

        protected void InvokeOnLoadCompletedEvent()
        {
            OnLoadCompleted?.Invoke();
        }

        protected void InvokeOnLoadingProgressEvent(float progress)
        {
            OnLoadingProgress?.Invoke(progress);
        }

        protected float GetProgress(float targetTime)
        {
            if (m_elapsedLoadingTime < targetTime)
            {
                return m_elapsedLoadingTime / targetTime;
            }
            else if (!m_sceneAsyncLoadCompleted)
            {
                return m_sceneAsyncLoadingProgress;
            }
            else
            {
                return 1f;
            }
        }

        public virtual void SetLoadingProgress(float progress)
        {
            //Debug.Log($"{(int)(progress * 100)}%", gameObject);

            m_sceneAsyncLoadingProgress = progress;
        }

        public virtual void SetAsLoadCompleted()
        {
            //Debug.Log("Load complete!");

            m_sceneAsyncLoadCompleted = true;
        }

        protected virtual void FinishAndContinue()
        {
            m_loading = false;

            m_sceneAsyncLoadCompleted = false;
            m_elapsedLoadingTime = 0f;

            InvokeOnLoadCompletedEvent();
        }
    }
}