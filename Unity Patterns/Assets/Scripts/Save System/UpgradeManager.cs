using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour, IBind<UpgradesSaveData>
{
    public List<UpgradeConfig> upgradeConfigs;
    [SerializeField]
    public List<Upgrade> upgrades;

    [SerializeField]
    public string Id { get; set; } = "UpgradeManager";
    [SerializeField] public UpgradesSaveData data;

    private void Start()
    {
        InitializeUpgrades();
        Bind(data);
    }
    private void InitializeUpgrades()
    {
        upgrades.Clear();
        foreach (var config in upgradeConfigs)
        {
            upgrades.Add(new Upgrade(config));
        }
    }

    public bool TryPurchaseUpgrade(Upgrade upgradeToBuy, ref float gold)
    {
        if (gold >= upgradeToBuy.currentCost)
        {
            upgradeToBuy.PurchaseUpgrade(ref gold);
            return true;
        }
        return false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            // foreach (var upgrade in upgrades)
            // {
            //     data.upgrades.Add(new UpgradeSaveData
            //     {
            //         Id = upgrade.Id,
            //         upgradeId = upgrade.Id,
            //         currentLevel = upgrade.currentLevel,
            //         currentCost = upgrade.currentCost
            //     });
            // }
            data.upgrades = upgrades.Select(u => u.ToSaveData()).ToList();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Bind(data);
        }
    }

    public void Bind(UpgradesSaveData data)
    {
        if (data == null || data.upgrades == null)
        {
            Debug.LogWarning("No save data to bind");
            return;
        }

        this.data = data;
        this.data.Id = Id;

        Debug.Log($"Binding {data.upgrades.Count} upgrades");

        foreach (var upgrade in upgrades)
        {
            var savedData = data.upgrades.FirstOrDefault(u => u.upgradeId == upgrade.upgradeConfig.upgradeId);
            if (savedData != null)
            {
                Debug.Log($"Found saved data for upgrade {upgrade.upgradeConfig.upgradeId} with level {savedData.currentLevel}");
                upgrade.LoadFromSaveData(savedData);
            }
            else
            {
                Debug.LogWarning($"No saved data found for upgrade {upgrade.upgradeConfig.upgradeId}");
            }
        }
    }
}

[System.Serializable]
public class UpgradesSaveData : ISaveable
{
    public string Id { get; set; }
    public List<UpgradeSaveData> upgrades = new List<UpgradeSaveData>();

}
