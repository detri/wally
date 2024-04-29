using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text levelInfo;
    public Image icon;
    public UpgradeManager upgradeManager;
    public WeaponManager weaponManager;

    private GameObject itemToUpgrade;
    private UpgradeData upgradeData;
    private PlayerController player;

    private void Start()
    {
        player = WallyGame.CurrentPlayer();
    }

    public void SetUpgradeData(UpgradeData data)
    {
        upgradeData = data;
        title.text = upgradeData.objectName;
        levelInfo.text = $"Level {upgradeData.upgradeLevel}";
        description.text = upgradeData.description;
        icon.sprite = upgradeData.icon;
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                itemToUpgrade = GameObject.Find($"{upgradeData.objectName}(Clone)");
                break;
            default:
                return;
        }
    }

    private void StatUpgrade(string stat)
    {
        switch (stat)
        {
            case "MaxHP":
                player.SetMaxHealthMod(player.maxHealthMod + 0.05f);
                break;
        }
    }

    public void OnPress()
    {
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                itemToUpgrade.GetComponent<Weapon>().ChangeLevel(upgradeData.upgradeLevel);
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponPrefab);
                break;
            case UpgradeType.StatUpgrade:
                StatUpgrade(upgradeData.objectName);
                break;
        }
        upgradeManager.Resume(upgradeData);
    }
}
