using d4160.Core;

namespace d4160.UI.Progress
{
    public abstract class ProgressBarLoopBase : EntityBehaviourBase, IProgressBarLoop
    {
        protected virtual void Awake()
        {
            ProgressPrefabsManagerBase.Instance.SetInstanced(this);
        }

        public abstract void StartLoop();

        public abstract void StopLoop();
    }
}