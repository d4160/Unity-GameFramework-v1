#if UNITY_ECS
using d4160.Core;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace d4160.GameFramework
{
    public class EntityFactory : MonoBehaviour, IFactory<Entity>, IInitializable
    {
        [Header("PREFAB OPTIONS")]
        [SerializeField] protected GameObject _prefab;

        protected EntityManager _entityManager;
        protected GameObjectConversionSettings _conversionSettings;
        protected BlobAssetStore _assetStore;
        protected Entity _entityPrefab;

        public virtual GameObject Prefab => _prefab;
        public EntityManager EntityManager => _entityManager;

        protected virtual void Awake()
        {
            Initialize();

            ConvertGameObjectPrefab();
        }

        protected virtual void OnDestroy()
        {
            Deinitialize();
        }

        public virtual void Initialize()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _assetStore = new BlobAssetStore();
            _conversionSettings =
                GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
        }

        public virtual void Deinitialize()
        {
            _assetStore?.Dispose();
        }

        public virtual void ConvertGameObjectPrefab()
        {
            if (Prefab)
            {
                _entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, _conversionSettings);
            }
        }

        public virtual Entity Fabricate(Vector3 position, Quaternion rotation)
        {
            Entity instance = _entityManager.Instantiate(_entityPrefab);
            _entityManager.SetComponentData(instance, new Translation { Value = position });
            _entityManager.SetComponentData(instance, new Rotation{ Value = rotation });

            return instance;
        }
    }
}
#endif