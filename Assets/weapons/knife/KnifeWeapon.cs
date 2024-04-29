
using System;
using UnityEngine;

public class KnifeWeapon : Weapon
{
    public override void FireWeapon()
    {
        FireKnives();
    }

    private void FireKnives()
    {
        var rotationOffset = player.aimDirection;
        var rotationAmount = 360 / numProjectiles;
        for (var i = 0; i < numProjectiles; i++)
        {
            var knife = Instantiate(projectile, player.Center, Quaternion.Euler(0, 0, rotationOffset));
            var knifeProjectile = knife.GetComponent<Projectile>();
            knifeProjectile.damage = projectileBaseDmg;
            knifeProjectile.speedMod = projectileSpeedMod;
            Destroy(knife, projectileLifetime);
            rotationOffset += rotationAmount;
        }
    }

    public override void ChangeLevel(int level)
    {
        weaponLevel = level;
        switch (weaponLevel)
        {
            case 2:
                numProjectiles = 2;
                cooldownMod = 0.9f;
                break;
            case 3:
                projectileSpeedMod = 1.5f;
                cooldownMod = 0.8f;
                break;
            case 4:
                projectileBaseDmg += 5;
                cooldownMod = 0.6f;
                break;
            case 5:
                numProjectiles = 4;
                cooldownMod = 0.5f;
                break;
            case 6:
                numProjectiles = 8;
                cooldownMod = 0.4f;
                break;
        }
    }
}
