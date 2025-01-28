using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public UpgradeConfig upgradeConfig;
    public int currentLevel;
    public float currentCost;
    public string Id { get; set; }

    public Upgrade(UpgradeConfig config)
    {
        upgradeConfig = config;
        Id = config.upgradeId;
        currentLevel = 0;
        currentCost = config.upgradeBaseCost;
    }

    public void PurchaseUpgrade(ref float gold)
    {
        if (gold >= currentCost)
        {
            gold -= currentCost;
            currentLevel++;
            currentCost = upgradeConfig.upgradeBaseCost * Mathf.Pow(upgradeConfig.upgradeCostMultiplier, currentLevel);
        }
    }

    public UpgradeSaveData ToSaveData()
    {
        return new UpgradeSaveData
        {
            upgradeId = upgradeConfig.upgradeId,
            currentLevel = currentLevel,
            currentCost = currentCost
        };
    }

    public void LoadFromSaveData(UpgradeSaveData saveData)
    {
        currentLevel = saveData.currentLevel;
        currentCost = saveData.currentCost;
    }
}

[System.Serializable]
public class UpgradeSaveData : ISaveable
{
    public string Id { get; set; }
    public string upgradeId;
    public int currentLevel;
    public float currentCost;
}



