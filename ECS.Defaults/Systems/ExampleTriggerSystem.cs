//using Unity.Physics.Systems;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Burst;

//[UpdateBefore(typeof(BuildPhysicsWorld))]
//public class ExampleTriggerSystem : ComponentSystem
//{
//    EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;
//    EntityQuery m_OverlappingGroup;

//    protected override void OnCreate()
//    {
//        m_EntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
//        m_OverlappingGroup = GetEntityQuery(new EntityQueryDesc
//        {
//            All = new ComponentType[]
//            {
//                typeof(OverlappingTriggerVolume),
//                typeof(EnemyOverlappingTriggerVolume),
//            }
//        });
//    }

//    [BurstCompile]
//    protected override void OnUpdate()
//    {
//        var overlappingComponents = GetComponentDataFromEntity<OverlappingTriggerVolume>();
//        var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer();

//        using (var overlappingEntities = m_OverlappingGroup.ToEntityArray(Allocator.TempJob))
//        {
//            foreach (var overlappingEntity in overlappingEntities)
//            {
//                var overlapComponent = overlappingComponents[overlappingEntity];
//                if (overlapComponent.HasJustEntered)
//                {
//                    var volumeEntity = overlapComponent.VolumeEntity;

//                    if (EntityManager.HasComponent<PlayerTag>(overlappingEntity))
//                    {
//                        Health health = EntityManager.GetComponentData<Health>(overlappingEntity);
//                        health.healthValue -= 1;

//                        commandBuffer.SetComponent(overlappingEntity, health);
//                        commandBuffer.DestroyEntity(volumeEntity);
//                    }
//                    else if (EntityManager.HasComponent<LaserTag>(overlappingEntity))
//                    {
//                        commandBuffer.DestroyEntity(volumeEntity);
//                        commandBuffer.DestroyEntity(overlappingEntity);
//                    }
//                }
//            }
//        }
//    }


//    ////[BurstCompile]
//    //struct ChangeMaterialJob : IJobForEachWithEntity<ExitedTriggerVolume>
//    //{
//    //    public EntityCommandBuffer CommandBuffer;
//    //    public EntityManager EntityManager;

//    //    public void Execute(Entity entity, int index, ref ExitedTriggerVolume exitedTrigger)
//    //    {
//    //        if (exitedTrigger.volumeId < 10)
//    //        {
//    //            var volumeEntity = exitedTrigger.volumeEntity;

//    //            var volumeRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(volumeEntity);
//    //            var overlappingRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(entity);
//    //            overlappingRenderMesh.material = volumeRenderMesh.material;

//    //            CommandBuffer.SetSharedComponent<RenderMesh>(entity, overlappingRenderMesh);
//    //        }
//    //    }
//    //}
    
//    //protected override JobHandle OnUpdate(JobHandle inputDeps)
//    //{
//    //    var job = new ChangeMaterialJob
//    //    {
//    //        CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
//    //        EntityManager = EntityManager,
//    //    }.ScheduleSingle(this, inputDeps);

//    //    m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

//    //    return job;
//    //}
//}
