using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    // Actual entity data for our custom scene/game manager entity
    public struct EntityManagerProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberOfDropPoints;
        public Entity DropPointPrefab;
    }
}