//using Unity.Entities;
//using UnityEngine;
//using System;

//public class TriggerVolumeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
//{
//    public TriggerVolumeType Type = TriggerVolumeType.None;

//    void OnEnable() { }

//    void IConvertGameObjectToEntity.Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
//    {
//        if (enabled)
//        {
//            dstManager.AddComponentData(entity, new TriggerVolume()
//            {
//                Type = (int)Type,
//                CurrentFrame = 0,
//            });
//        }
//    }
//}