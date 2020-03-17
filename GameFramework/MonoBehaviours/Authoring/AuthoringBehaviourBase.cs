using Unity.Entities;
using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class AuthoringBehaviourBase<T> : MonoBehaviour, IConvertGameObjectToEntity where T : struct, IComponentData
    {
        [SerializeField] protected T _data;

        public T Data => _data;

        public virtual void Convert(Entity entity, EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            if (enabled)
            {
                dstManager.AddComponentData(entity, _data);
            }
        }
    }
}