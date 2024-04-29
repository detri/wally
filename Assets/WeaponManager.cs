using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> currentWeapons;
    public int maxWeapons = 6;
    
    public void AddWeapon(GameObject weaponPrefab)
    {
        if (!CanAddWeapon()) return;
        var weapon = Instantiate(weaponPrefab);
        currentWeapons.Add(weapon);
    }

    public bool CanAddWeapon()
    {
        return currentWeapons.Count < maxWeapons;
    }

    public bool HasWeapon(UpgradeData weapon)
    {
        return currentWeapons.Exists(gameObj => gameObj.name.Contains(weapon.objectName));
    }
}
