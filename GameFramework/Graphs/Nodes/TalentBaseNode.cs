namespace d4160.GameFramework
{
    using UnityEngine;

    [System.Serializable]
	public abstract class TalentBaseNode : GameFrameworkBaseNode<TalentBaseNode>
	{
        [SerializeField] protected bool _actived;

        public bool Actived
        {
            get => _actived;
            set => _actived = value;
        }

        protected override void Process()
        {
            // if actived 
            base.Process();
        }

        public virtual void Unprocess()
        {
            
        }
    }
}