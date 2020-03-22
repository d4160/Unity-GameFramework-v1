using GraphProcessor;

namespace d4160.GameFramework
{
    using UnityEngine;

    [System.Serializable, NodeMenuItem("Talent/Default Talent")]
	public class DefaultTalentNode : TalentBaseNode
    {
        [SerializeField] protected bool _actived;

        [SerializeField] protected DefaultTalentDefinition _definition;

        public virtual DefaultTalentDefinition Definition
        {
            get => _definition;
            set => _definition = value;
        }

        public override string name
        {
            get
            {
                if (_definition)
                {
                    return $"Talent '{_definition.Name}'"; ;
                }

                return "Talent ??"; ;
            }
        }

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