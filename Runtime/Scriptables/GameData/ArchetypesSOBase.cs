namespace d4160.GameFramework
{
    using d4160.Core;
    using Malee;
    using System.Linq;

    public abstract class ArchetypesSOBase<TReorderableArray, TArchetype>
        : ReorderableSO<TReorderableArray, TArchetype>,
//#if UNITY_EDITOR
        IArchetypeNames, IArchetypeOperations,
//#endif
        IArchetypeGetter<TArchetype>
        where TArchetype : IArchetype, new()
        where TReorderableArray : ReorderableArray<TArchetype>
    {
        public override TArchetype GetElementWith(int id)
        {
            for (var i = 0; i < m_elements.Length; i++)
            {
                if (m_elements[i].ID == id)
                    return m_elements[i];
            }

            return default;
        }

        #region IArchetypeNames Implementation
        public virtual string[] ArchetypeNames
        {
            get
            {
                var names = new string[m_elements.Count];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = m_elements[i].Name;
                }

                return names;
            }
        }
        #endregion

        #region IArchetypeOperations Implementation

        public int Count => m_elements.Count;

        public virtual void AddRange(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                var element = AddNew();
                element.Name = names[i];
            }
        }

        public virtual void Clear()
        {
            m_elements.Clear();
        }

        protected virtual IArchetype AddNew()
        {
            var newElement = new TArchetype();
            m_elements.Add(newElement);

            IterateIds();

            return newElement;
        }

        protected virtual void IterateIds()
        {
            for (var i = 0; i < m_elements.Length; i++)
            {
                if (m_elements[i] != null)
                    m_elements[i].ID = i + 1;
            }
        }
        #endregion

#if UNITY_EDITOR
        #region Unity Callbacks
        /// <summary>
        /// Called on any change in the editor
        /// </summary>
        protected virtual void OnValidate()
        {
            IterateIds();
        }
        #endregion

        #region Other Methods
        protected virtual void Add(TArchetype archetype)
        {
            m_elements.Add(archetype);

            IterateIds();
        }

        protected virtual void CopyLast()
        {
            m_elements.Add(m_elements.LastOrDefault());

            IterateIds();
        }

        protected virtual void RemoveLast()
        {
            m_elements.RemoveAt(m_elements.LastIndex());

            IterateIds();
        }
        #endregion
#endif
    }
}