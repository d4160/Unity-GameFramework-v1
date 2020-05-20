//using System.Collections;
//using System.Collections.Generic;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Transforms;
//using UnityEngine;

//[AlwaysSynchronizeSystem]
//public class ExampleMovementSystem : JobComponentSystem
//{
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        float deltaTime = Time.DeltaTime;

//        Entities
//            .WithAll<__Tag>()
//            .ForEach((ref Translation trans, in Movement2D mov) =>
//                {
//                    trans.Value.xy += (mov.direction * mov.speed * deltaTime);
//                }).Run();

//        return default;
//    }
//}

//public class ExampleDestroySystem : JobComponentSystem
//{
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

//        Entities.ForEach((Entity entity, in Limit limits, in Translation trans) =>
//        {
//            float3 pos = trans.Value;
//            switch (limits.side)
//            {
//                case LimitSide.Right:
//                    if (pos.y < limits.limitValue)
//                    {
//                        ecb.DestroyEntity(entity);
//                    }
//                    break;
//                case LimitSide.Left:
//                    if (pos.y > limits.limitValue)
//                    {
//                        ecb.DestroyEntity(entity);
//                    }
//                    break;
//            }

//        }).Run();

//        ecb.Playback(EntityManager);
//        ecb.Dispose();

//        return default;
//    }
//}

//[AlwaysSynchronizeSystem]
//public class ExampleInputSystem : JobComponentSystem
//{
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
//        Vector2 direction = manager.PlayerDirection;
//        Entities
//            .WithAll<PlayerTag>()
//            .WithoutBurst()
//            .ForEach((ref Movement2D mov, in Translation trans) =>
//                {
//                    mov.direction = direction;
//                    manager.PlayerPosition = new Vector2(trans.Value.x, trans.Value.y);
//                }
//            ).Run();

//        return default;
//    }
//}
