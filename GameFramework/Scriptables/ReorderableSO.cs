namespace d4160.GameFramework
{
    using d4160.Core;
    using Malee;
    using UnityEngine;
    using d4160.DataPersistence;

    public abstract class ReorderableSO<T1, T2, T3> : ScriptableObjectBase<T3>, IElementGetter<T2>
        where T1 : ReorderableArray<T2>
        where T3 : BaseSerializableData
    {
        [SerializeField][Reorderable(paginate = true, pageSize = 10)]
        protected T1 m_elements;

        public virtual T1 Elements => m_elements;

        public int ElementsCount => m_elements.Length;

        public virtual T2 GetElementAt(int idx)
        {
            return m_elements.IsValidIndex(idx) ? m_elements[idx] : default;
        }

        public virtual T2 GetElementWith(int id)
        {
            return default;
        }

        public virtual void Add(T2 element)
        {
            m_elements.Add(element);
        }

        public virtual void Remove(T2 element)
        {
            m_elements.Remove(element);
        }

        public virtual void Clear()
        {
            m_elements.Clear();
        }

        public virtual T2[] GetAll()
        {
            return m_elements.ToArray();
        }

        public virtual void SetAll(T2[] elements)
        {
            m_elements.CopyFrom(elements);
        }
    }
}