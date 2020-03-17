using d4160.Core;

namespace d4160.GameFramework
{
    using UnityEngine;

    [System.Serializable]
    public class DefaultArchetype : IArchetype, IArchetypeName
    {
        [SerializeField] protected string m_name;
        [SerializeField] protected int m_id;
        [SerializeField] protected string[] m_categories;

        public DefaultArchetype()
        {
        }

        public virtual int ID
        {
            get => m_id;
            set => m_id = value;
        }

        public virtual string Name { get => m_name; set => m_name = value; }
    }
}
