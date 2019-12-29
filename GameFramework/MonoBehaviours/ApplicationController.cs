namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.Diagnostics;

    public class ApplicationController : MonoBehaviour
    {
        public virtual void Quit()
        {
            if (Application.isPlaying)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        public virtual void ForceCrash(ForcedCrashCategory category = ForcedCrashCategory.Abort)
        {
    #if UNITY_EDITOR
    #else
            Utils.ForceCrash(category);
    #endif
        }
    }
}