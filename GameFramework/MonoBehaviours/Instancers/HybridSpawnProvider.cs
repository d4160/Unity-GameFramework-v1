using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using d4160.GameFramework;
using Lean.Pool;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityExtensions;

namespace d4160.GameFramework
{
    public class HybridSpawnProvider : SpawnProvider
    {
        [Header("HYBRID OPTIONS")]
        [SerializeField] protected bool _useECS = false;
        [Header("ENTITY OPTIONS")]
        [InspectInline(canEditRemoteTarget = true)]
        [SerializeField] protected EntityFactory[] _factories;

        private Entity _lastEntity;

        public EntityFactory SelectedFactory =>
            _factories.IsValidIndex(SelectedSourceIndex) ? _factories[SelectedSourceIndex] : null;

        public override Vector3 SpawnPosition => _useECS ? SelectedFactory.transform.position : SelectedPool.transform.position;
        public override Quaternion SpawnRotation => _useECS ? SelectedFactory.transform.rotation : SelectedPool.transform.rotation;

        public virtual EntityFactory GetFactory(int sourceIndex) => _factories.IsValidIndex(sourceIndex) ? _factories[sourceIndex] : SelectedFactory;

        public Entity LastEntity => _lastEntity;

        public override void Spawn(int sourceIndex = -1)
        {
            if (!CanSpawn) return;

            if (!_useECS)
            {
                var pool = sourceIndex == -1 ? SelectedPool : GetPool(sourceIndex);
                if (pool)
                {
                    GameObjectSpawn(pool.Spawn(_overrideSpawnPosition ?? SpawnPosition, SpawnRotation));
                }
            }
            else
            {
                var factory = sourceIndex == -1 ? SelectedFactory : GetFactory(sourceIndex);
                if (factory)
                {
                    EntitySpawn(factory.Fabricate(_overrideSpawnPosition ?? SpawnPosition, SpawnRotation));
                }
            }
        }

        protected virtual void EntitySpawn(Entity instance)
        {
            _lastEntity = instance;
        }
    }
}

