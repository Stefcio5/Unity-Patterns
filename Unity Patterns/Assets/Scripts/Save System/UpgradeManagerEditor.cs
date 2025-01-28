
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeManager))]
public class UpgradeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpgradeManager upgradeManager = (UpgradeManager)target;

        // try purchase upgrade from list
        if (GUILayout.Button("Try Purchase Upgrade"))
        {
            if (upgradeManager.upgrades.Count > 0)
            {
                Upgrade upgrade = upgradeManager.upgrades[0];
                float gold = GameManager.Instance.gold;
                upgradeManager.TryPurchaseUpgrade(upgrade, ref gold);
            }
        }
    }
}