#if UNITY_ECS
using Unity.Entities;
#endif
using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class AuthoringBehaviourBase<T> : MonoBehaviour
#if UNITY_ECS
        , IConvertGameObjectToEntity where T : struct, IComponentData
#endif
    {
        [SerializeField] protected T _data;

        public T Data => _data;

#if UNITY_ECS
        public virtual void Convert(Entity entity, EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            if (enabled)
            {
                dstManager.AddComponentData(entity, _data);
            }
        }
#endif
    }
}