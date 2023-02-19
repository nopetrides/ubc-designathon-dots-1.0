using ComponentAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace AuthoringAndMono
{
    /// <summary>
    /// Exposes serialized fields into the editor for us to modify
    /// </summary>
    public class EntityManagerMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberOfDropPoints;
        public GameObject DropPointsPrefab;
        public GameObject EnemyPrefab;
        public float EnemySpawnRate;
    }
    
    /// <summary>
    /// Bakes the serialized fields into entities
    /// </summary>
    public class EntityManagerBaker : Baker<EntityManagerMono>
    {
        public override void Bake(EntityManagerMono authoring)
        {
            AddComponent(new EntityManagerProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberOfDropPoints = authoring.NumberOfDropPoints,
                DropPointPrefab = GetEntity(authoring.DropPointsPrefab),
                EnemyPrefab = GetEntity(authoring.EnemyPrefab),
                EnemySpawnRate = authoring.EnemySpawnRate
            });
            AddComponent<EnemySpawnPoints>();
            AddComponent<EnemySpawnTimer>();
        }
    }
}