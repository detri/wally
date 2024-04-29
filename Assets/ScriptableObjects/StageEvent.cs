using UnityEngine;

public enum StageEventType
{
    MonsterFormation,
    StageBoss,
    Treasure,
    SpawnRate,
    AddEnemyType
}

[CreateAssetMenu]
public class StageEvent : ScriptableObject
{
    public StageEventType eventType;
    public GameObject spawnObject;
    public int spawnCount;
    public int spawnTime;
}