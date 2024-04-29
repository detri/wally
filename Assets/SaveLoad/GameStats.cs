using System;
using System.Collections.Generic;

[Serializable]
public class GameStats
{
    public string playerName;
    public string characterName;
    public string stageName;
    public bool stageSuccess;
    public float timeSurvived;
    public string[] weaponsHeld;
    public string[] itemsHeld;
    public int enemiesKilled;
    public int damageDealt;
    public int damageTaken;
    public float distanceMoved;
    public string utcDateAchieved;

    public GameStats(string playerName, string characterName, string stageName, float timeSurvived, List<string> weaponsHeld, List<string> itemsHeld, int enemiesKilled, int damageDealt, int damageTaken, int distanceMoved, DateTime dateAchieved)
    {
        this.playerName = playerName;
        this.characterName = characterName;
        this.stageName = stageName;
        this.timeSurvived = timeSurvived;
        this.weaponsHeld = weaponsHeld.ToArray();
        this.itemsHeld = itemsHeld.ToArray();
        this.enemiesKilled = enemiesKilled;
        this.damageDealt = damageDealt;
        this.damageTaken = damageTaken;
        this.distanceMoved = distanceMoved;
        utcDateAchieved = dateAchieved.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
    }

    public void SetDateAchieved(DateTime dateAchieved)
    {
        utcDateAchieved = dateAchieved.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
    }
}
