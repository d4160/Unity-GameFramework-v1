using Unity.Entities;

namespace d4160.ECS
{
    public struct TriggerVolume : IComponentData
    {
        public int Type;
        public int CurrentFrame;
    }
}
