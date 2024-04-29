using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUnlock,
    ItemUnlock,
    WeaponUpgrade,
    ItemUpgrade,
    StatUpgrade
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;
    public UpgradeType upgradeType = UpgradeType.WeaponUpgrade;
    public string objectName = "Knife";
    public string description;
    public int upgradeLevel = 1;
    public GameObject weaponPrefab;
}
