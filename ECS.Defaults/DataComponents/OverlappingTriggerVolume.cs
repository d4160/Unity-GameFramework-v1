using Unity.Entities;

namespace d4160.ECS
{
    public struct OverlappingTriggerVolume : IComponentData
    {
        public Entity VolumeEntity;
        public int VolumeType;
        public int CreatedFrame;
        public int CurrentFrame;

        public bool HasJustEntered
        {
            get { return (CurrentFrame - CreatedFrame) == 0; }
        }
    }
}