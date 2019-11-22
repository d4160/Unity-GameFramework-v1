namespace d4160.GameFramework
{
    using d4160.Core;
    using Malee;
    using UnityEngine;

    public abstract class ReorderableSO<TReorderableArray, TReorderableElement> : ScriptableObjectBase, IElementGetter<TReorderableElement>  where TReorderableArray : ReorderableArray<TReorderableElement>
    {
        [SerializeField][Reorderable(paginate = true, pageSize = 10)]
        protected TReorderableArray m_elements;

        public virtual TReorderableArray Elements => m_elements;

        public virtual TReorderableElement GetElementAt(int idx)
        {
            return m_elements.IsValidIndex(idx) ? m_elements[idx] : default;
        }

        public virtual TReorderableElement GetElementWith(int id)
        {
            return default;
        }
    }
}