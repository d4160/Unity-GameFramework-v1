namespace d4160.GameFramework
{
    using d4160.Core;
    using Malee;
    using UnityEngine;

    public abstract class ReorderableSO<T1, T2, T3> : ScriptableObjectBase<T3>, IElementGetter<T2>  where T1 : Malee.ReorderableArray<T2> where T3 : BaseSerializableData
    {
        [SerializeField][Reorderable(paginate = true, pageSize = 10)]
        protected T1 m_elements;

        public virtual T1 Elements => m_elements;

        public virtual T2 GetElementAt(int idx)
        {
            return m_elements.IsValidIndex(idx) ? m_elements[idx] : default;
        }

        public virtual T2 GetElementWith(int id)
        {
            return default;
        }
    }
}