using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeConfig", menuName = "UpgradeConfig")]
public class UpgradeConfig : ScriptableObject
{
    public string upgradeName;
    public string upgradeId;
    public float upgradeBaseCost;
    public float upgradeCostMultiplier;

}
