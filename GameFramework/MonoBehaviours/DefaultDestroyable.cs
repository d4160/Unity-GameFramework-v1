using UltEvents;

namespace d4160.GameFramework
{
    using UnityEngine;

    public class DefaultDestroyable : MonoBehaviour
    {
        [SerializeField] protected bool _poolObject;
        [SerializeField] protected UltEvent _onDestroyedInAdvance;
        [SerializeField] protected UltEvent _onDestroyed;

        protected DefaultEntityAuthoring _entity;
        protected EntityCategoryAuthoring _eCategory;

        public int PoolIndex { get; set; } = 0;

        public bool DestroyedState { get; protected set; } = false;

        protected virtual void Awake()
        {
            _entity = GetComponent<DefaultEntityAuthoring>();
            _eCategory = GetComponent<EntityCategoryAuthoring>();
        }

        /// <summary>
        /// Used to set a destroyed state previous Uninstantiation or Despawn
        /// </summary>
        [ContextMenu("DestroyInAdvance")]
        public virtual void DestroyInAdvance()
        {
            DestroyedState = true;

            _onDestroyedInAdvance?.Invoke();
        }

        [ContextMenu("Destroy")]
        public virtual void Destroy(float delay = 0f)
        {
            DestroyedState = false;

            _onDestroyed?.Invoke();

            if (_poolObject && _entity)
            {
                if (!GameModeManagerBase.Instanced) return;

                if (!_eCategory)
                {
                    GameModeManagerBase.Instance.Despawn(this.gameObject, _entity.Entity, PoolIndex, delay);
                }
                else
                {
                    GameModeManagerBase.Instance.Despawn(this.gameObject, _entity.Entity, _eCategory.Category, PoolIndex, delay);
                }
            }
            else
            {
                Destroy(gameObject, delay);
            }
        }
    }

}
