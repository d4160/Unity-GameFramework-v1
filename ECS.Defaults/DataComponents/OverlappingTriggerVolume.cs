#if UNITY_ECS
using Unity.Entities;
#endif

namespace d4160.ECS
{
    public struct OverlappingTriggerVolume
#if UNITY_ECS
        : IComponentData
#endif
    {
#if UNITY_ECS
        public Entity VolumeEntity;
#endif
        public int VolumeType;
        public int CreatedFrame;
        public int CurrentFrame;

        public bool HasJustEntered
        {
            get { return (CurrentFrame - CreatedFrame) == 0; }
        }
    }
}