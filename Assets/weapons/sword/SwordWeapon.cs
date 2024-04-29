
using UnityEngine;

public class SwordWeapon : Weapon
{
    public float scale = 1.0f;
    public float swingArc = 90.0f;
    
    public override void FireWeapon()
    {
        var rotationOffset = player.aimDirection;
        var rotationAmount = 360 / numProjectiles;
        for (var i = 0; i < numProjectiles; i++)
        {
            var attack = Instantiate(projectile, player.Center, Quaternion.Euler(0, 0, rotationOffset), player.transform)
                .GetComponent<SwingAttack>();
            attack.SetScale(scale);
            attack.SetupSwingArc(swingArc);
            attack.damage = projectileBaseDmg;
            rotationOffset += rotationAmount;
        }
    }

    public override void ChangeLevel(int level)
    {
        weaponLevel = level;
        switch (weaponLevel)
        {
            case 1:
                projectileBaseDmg = 10;
                swingArc = 90f;
                cooldownMod = 1.0f;
                swingArc = 90f;
                numProjectiles = 1;
                break;
            case 2:
                projectileBaseDmg = 10;
                scale = 2.0f;
                cooldownMod = 0.9f;
                swingArc = 90f;
                numProjectiles = 1;
                break;
            case 3:
                projectileBaseDmg = 10;
                scale = 2.0f;
                swingArc = 180f;
                cooldownMod = 0.8f;
                numProjectiles = 1;
                break;
            case 4:
                projectileBaseDmg = 10;
                scale = 2.5f;
                swingArc = 180f;
                cooldownMod = 0.7f;
                numProjectiles = 1;
                break;
            case 5:
                projectileBaseDmg = 20;
                scale = 2.5f;
                swingArc = 180f;
                cooldownMod = 0.6f;
                numProjectiles = 1;
                break;
            case 6:
                projectileBaseDmg = 20;
                scale = 2.5f;
                swingArc = 360f;
                cooldownMod = 0.5f;
                numProjectiles = 2;
                break;
        }
    }
}