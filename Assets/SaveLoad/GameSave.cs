using System;
using System.Collections.Generic;

[Serializable]
public class GameSave
{
    public string name;
    public int dollars;
    public List<GameStats> gameHistory;
    
    public GameSave(string name)
    {
        this.name = name;
        dollars = 0;
        gameHistory = new List<GameStats>();
    }
}