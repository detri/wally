using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    public float lengthSeconds = 1200;
    public string stageName = "Weenie Forest";
    public List<GameObject> monsters = new List<GameObject>();
    public float monsterSpawnInterval = 5.0f;
    public int monsterSpawnAmount = 1;
    public Vector2 maxSpawnRange;
    public Vector2 minSpawnRange;
    public Timer timer;
    public List<StageEvent> stageEvents;
    public GameStats gameStats;

    public float ProgressMultiplier => 1.0f + timer.Seconds / lengthSeconds;

    private PlayerController player;

    private void OnTick(int seconds)
    {
        PerformStageEvent(seconds);
        if (seconds % monsterSpawnInterval == 0)
        {
            SpawnRandomMonsters(monsterSpawnAmount);
        }
    }

    private void PerformStageEvent(int seconds)
    {
        var events = stageEvents.FindAll(stageEvent => stageEvent.spawnTime == seconds);
        foreach (var stageEvent in events)
        {
            var spawnObject = stageEvent.spawnObject;
            switch (stageEvent.eventType)
            {
                case StageEventType.MonsterFormation:
                    SpawnInCircle(spawnObject, stageEvent.spawnCount, 10.0f);
                    break;
                case StageEventType.SpawnRate:
                    monsterSpawnInterval = stageEvent.spawnCount;
                    break;
            }
        }
    }

    private void SpawnInCircle(GameObject monster, int amount, float radius)
    {
        for (var i = 0; i < amount; i++)
        {
            var radians = 2 * Mathf.PI / amount * i;
            var direction = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0);
            var pos = player.Center + direction * radius;
            var monsterInstance = SpawnMonster(monster, pos);
            monsterInstance.moveSpeed *= 0.5f;
        }
    }

    private void SpawnRandomMonsters(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var monster = monsters[Random.Range(0, monsters.Count)];
            var randX = Random.Range(-maxSpawnRange.x, maxSpawnRange.x);
            var randY = Random.Range(-maxSpawnRange.y, maxSpawnRange.y);
            var minSpawn = new Vector3(randX < 0 ? -minSpawnRange.x : minSpawnRange.x,
                randY < 0 ? -minSpawnRange.y : minSpawnRange.y, 0);
            var position = player.Center + new Vector3(randX, randY, 0) + minSpawn;
            SpawnMonster(monster, position);
        }
    }

    private EnemyInfo SpawnMonster(GameObject monster, Vector3 position)
    {
        var monsterInstance = Instantiate(monster, position, Quaternion.identity).GetComponent<EnemyInfo>();
        monsterInstance.maxHealth *= ProgressMultiplier;
        monsterInstance.currentHealth = monsterInstance.maxHealth;
        return monsterInstance;
    }

    public void OnEnd()
    {
        gameStats.timeSurvived = timer.ElapsedTime;
        if (gameStats.timeSurvived >= lengthSeconds)
        {
            gameStats.stageSuccess = true;
        }
        gameStats.SetDateAchieved(DateTime.Now);
        SaveSystem.SavedGame.gameHistory.Add(gameStats);
        SaveSystem.SaveData();
    }

    private void Start()
    {
        player = WallyGame.CurrentPlayer();
        gameStats = new GameStats("Player", "Wally", stageName, 0, new List<string>(), new List<string>(), 0, 0, 0, 0,
            new DateTime());
        timer.Tick += OnTick;
    }
}