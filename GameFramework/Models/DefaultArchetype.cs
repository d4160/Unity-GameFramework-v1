namespace d4160.GameFramework
{
    using UnityEngine;

    [System.Serializable]
    public class DefaultArchetype : IArchetype
    {
        [SerializeField] protected string m_name;
        [SerializeField] protected int m_id;

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
