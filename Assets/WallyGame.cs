using UnityEngine;


public static class WallyGame
{
    private static PlayerController player;
    
    public static PlayerController CurrentPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        return player;
    }
}
