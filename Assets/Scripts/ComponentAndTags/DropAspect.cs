using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace ComponentAndTags
{
    /// <summary>
    /// Allows access to some entity data
    /// </summary>
    public readonly partial struct DropAspect : IAspect
    {
        // Reference to this exact entity
        public readonly Entity DropEntity;
        // Reference to an aspect for when we want to reference the transform
        private readonly TransformAspect _transformAspect;
        // Allows access to our custom entity manager, read only
        private readonly RefRO<EntityManagerProperties> _entityManagerProperties;
        private readonly RefRW<EnemySpawnPoints> _enemySpawnPoints;
        private readonly RefRW<EnemySpawnTimer> _enemySpawnTimer;
        public NativeArray<float3> EnemySpawnPoints
        {
            get => _enemySpawnPoints.ValueRO.Value;
            set => _enemySpawnPoints.ValueRW.Value = value;
        }

        // public getter for the number on the manager
        public int NumberDropPointsToSpawn => _entityManagerProperties.ValueRO.NumberOfDropPoints;
        // public getter for the drop prefab
        public Entity DropPointPrefab => _entityManagerProperties.ValueRO.DropPointPrefab;

        public LocalTransform GetRandomDropTransform(ref Random random)
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(ref random),
                Rotation = quaternion.identity,
                Scale = 0f,
            };
        }

        private float3 GetRandomPosition(ref Random random)
        {
            float3 randomPosition;
            randomPosition = random.NextFloat3(MinCorner,MaxCorner);
            return randomPosition;
        }

        private float3 MinCorner => _transformAspect.LocalPosition - HalfDimensions;
        private float3 MaxCorner => _transformAspect.LocalPosition + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x =_entityManagerProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z =_entityManagerProperties.ValueRO.FieldDimensions.y * 0.5f
        };

        public float EnemySpawnTimer
        {
            get => _enemySpawnTimer.ValueRO.Value;
            set => _enemySpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnEnemy => EnemySpawnTimer <= 0f;

        public float EnemySpawnRate => _entityManagerProperties.ValueRO.EnemySpawnRate;

        public Entity EnemyPrefab => _entityManagerProperties.ValueRO.EnemyPrefab;
    }
}