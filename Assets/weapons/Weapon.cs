using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public float cooldown = 2.0f;
    public float projectileLifetime = 5.0f;
    public int numProjectiles = 1;
    public PlayerController player;
    public GameObject projectile;
    public int weaponLevel = 1;
    public int projectileBaseDmg = 15;
    public float projectileSpeedMod = 1.0f;
    public float cooldownMod = 1.0f;

    private float currentCooldown = 0.0f;

    void Start()
    {
        player = WallyGame.CurrentPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldown * cooldownMod)
        {
            FireWeapon();
            currentCooldown = 0.0f;
        }
    }

    public abstract void FireWeapon();
    public abstract void ChangeLevel(int level);
}
