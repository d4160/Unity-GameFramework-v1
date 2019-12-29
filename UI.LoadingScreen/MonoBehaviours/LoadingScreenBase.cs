namespace d4160.UI
{
    using System;
    using System.Collections;
    using d4160.Core;
    using d4160.Systems.SceneManagement;
    using UniRx;
    using UnityEngine;

    public abstract class LoadingScreenBase : Singleton<LoadingScreenBase>, ILoadingScreen
    {
        public static event AsyncOperationProgress OnLoadingProgress;
        public static event Action OnLoadCompleted;

        protected float m_elapsedLoadingTime;
        protected float m_sceneAsyncLoadingProgress;
        protected bool m_sceneAsyncLoadCompleted;
        protected IDisposable m_updateCallbackRegister;

        protected virtual bool ReadyToContinue => m_sceneAsyncLoadCompleted;

        public virtual void StartLoad()
        {
            m_updateCallbackRegister = MainThreadDispatcher.UpdateAsObservable().Subscribe(
                (u) => UpdateCallback());
        }

        protected virtual void UpdateCallback()
        {
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
            m_updateCallbackRegister.Dispose();

            m_sceneAsyncLoadCompleted = false;
            m_elapsedLoadingTime = 0f;

            InvokeOnLoadCompletedEvent();
        }
    }
}