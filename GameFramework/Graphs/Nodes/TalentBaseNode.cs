namespace d4160.GameFramework
{
    using GraphProcessor;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [System.Serializable]
	public abstract class TalentBaseNode : GameFrameworkBaseNode<TalentBaseNode>
    {
        public virtual void Unprocess() { }
    }
}