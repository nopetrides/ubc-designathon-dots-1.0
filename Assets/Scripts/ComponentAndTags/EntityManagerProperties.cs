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
        public Entity EnemyPrefab; // single enemy, for testing
        public float EnemySpawnRate;
    }
    
    public struct EnemySpawnTimer : IComponentData
    {
        public float Value;
    }
}