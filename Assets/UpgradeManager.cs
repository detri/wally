using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    public GameObject uiLevelUp;
    public PauseScript pauseManager;
    public WeaponManager weaponManager;
    private List<UpgradeData> allUpgrades;
    private List<UpgradeData> availableUpgrades;
    public int numberOfUpgradesOnLevel = 1;

    private void Start()
    {
        allUpgrades = Resources.LoadAll<UpgradeData>("Upgrades").ToList();
        availableUpgrades = allUpgrades.Where(upgrade =>
        {
            var isStartingWeapon =
                WallyGame.CurrentPlayer().characterData.startingWeapon.name.Contains(upgrade.objectName);
            var isDefaultUpgrade = upgrade.upgradeType is UpgradeType.WeaponUnlock or UpgradeType.StatUpgrade
                or UpgradeType.ItemUnlock;
            var isStartingWeaponUnlock =
                isStartingWeapon &&
                upgrade.upgradeType is UpgradeType.WeaponUnlock;
            var isStartingWeaponUpgrade = isStartingWeapon && upgrade.upgradeType is UpgradeType.WeaponUpgrade &&
                                          upgrade.upgradeLevel == 2;
            return isStartingWeaponUpgrade || (isDefaultUpgrade && !isStartingWeaponUnlock);
        }).ToList();
    }

    public void LevelUp()
    {
        pauseManager.Pause();
        var numRandomUpgrades = Mathf.Min(numberOfUpgradesOnLevel, availableUpgrades.Count);
        var upgrades = RandomUpgrades(numRandomUpgrades);
        var upgradeButtons = uiLevelUp.GetComponentsInChildren<UpgradeButton>();
        if (upgrades.Count > 0)
        {
            for (var i = 0; i < numRandomUpgrades; i++)
            {
                upgradeButtons[i].SetUpgradeData(upgrades[i]);
            }
        }
        uiLevelUp.SetActive(true);
    }

    public void Resume(UpgradeData selectedUpgrade)
    {
        // add next weapon upgrade to the pool of available upgrades
        if (selectedUpgrade.upgradeType is UpgradeType.WeaponUpgrade or UpgradeType.WeaponUnlock)
        {
            var nextUpgrade = allUpgrades.Find(data => data.objectName == selectedUpgrade.objectName && data.upgradeLevel == selectedUpgrade.upgradeLevel + 1);
            if (nextUpgrade != null)
            {
                availableUpgrades.Add(nextUpgrade);
            }
        }

        // remove the selected upgrade from the pool of available upgrades.
        // stat upgrades are never removed, you can keep growing those forever
        if (selectedUpgrade.upgradeType != UpgradeType.StatUpgrade)
        {
            availableUpgrades.Remove(selectedUpgrade);
        }
        
        // if the player has max weapons, remove weapon unlocks from the pool
        if (!weaponManager.CanAddWeapon())
        {
            var unlocks = availableUpgrades.Where((upgrade) => upgrade.upgradeType == UpgradeType.WeaponUnlock);
            var upgradeData = unlocks.ToList();
            foreach (var upgrade in upgradeData)
            {
                availableUpgrades.Remove(upgrade);
            }
        }
        
        pauseManager.Resume();
        uiLevelUp.SetActive(false);
    }

    private List<UpgradeData> RandomUpgrades(int count)
    {
        var upgrades = new List<UpgradeData>();
        var numAvailableUpgrades = availableUpgrades.Count;
        var availableCount = count;
        if (numAvailableUpgrades < count)
        {
            availableCount = numAvailableUpgrades;
        }
        while (upgrades.Count < availableCount)
        {
            var randomUpgrade = availableUpgrades[Random.Range(0, numAvailableUpgrades)];
            if (upgrades.Contains(randomUpgrade))
            {
                continue;
            }
            upgrades.Add(randomUpgrade);
        }
        return upgrades;
    }
}
