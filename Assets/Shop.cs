using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<DefenderData> defenders;
    public List<UpgradeData> upgrades;
    public Transform defenderShopParent;
    public GameObject defenderButtonPrefab;
    public Transform upgradeShopParent;
    public GameObject upgradeButtonPrefab;

    private XPManager xpManager;
    private GameManager gameManager;
    private PlaceDefender placeDefender;

    private void Start()
    {
        xpManager = FindObjectOfType<XPManager>();
        gameManager = FindObjectOfType<GameManager>();
        placeDefender = FindObjectOfType<PlaceDefender>();

        PopulateDefenderShop();
        PopulateUpgradeShop();
    }

    void TryBuyDefender(DefenderData defender)
    {
        if (xpManager.GetLevel() >= defender.unlockLevel && gameManager.points >= defender.cost)
        {
            gameManager.points -= defender.cost;
            placeDefender.SetDefenderToBuild(defender.defenderPrefab);
        }
    }

    void TryBuyUpgrade(UpgradeData upgrade)
    {
        if (xpManager.GetLevel() >= upgrade.unlockLevel && gameManager.points >= upgrade.cost)
        {
            gameManager.points -= upgrade.cost;
            ApplyUpgrade(upgrade);
        }
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        string target = upgrade.targetType;
        string stat = upgrade.statType;
        float mult = upgrade.statMultiplier;

        // Tree Upgrades
        if (target == "Tree")
        {
            var tree = FindObjectOfType<HomeBase>();
            if (tree != null && stat == "Health")
            {
                tree.maxHealth *= mult;
                tree.GetComponent<HealthBar>()?.UpdateHealthBar(tree.maxHealth, tree.CurrentHealth);

                // ✅ VISUAL GLOW FOR TREE
                if (tree is IUpgradeVisual treeVisual)
                    treeVisual.UpgradeVisual();
            }
            return; // Early exit if tree upgrade
        }

        // Defender Upgrades
        var turrets = FindObjectsOfType<Turret>();
        foreach (var t in turrets)
        {
            if (t.defenderType == target)
            {
                switch (stat)
                {
                    case "Range":
                        t.range *= mult;
                        break;
                    case "FireRate":
                        t.fireRate *= mult;
                        break;
                    case "Health":
                        t.maxHealth *= mult;
                        t.currentHealth *= mult;
                        t.GetComponent<HealthBar>()?.UpdateHealthBar(t.maxHealth, t.currentHealth);
                        break;
                }

                // ✅ FIXED VISUAL UPGRADE
                if (t is IUpgradeVisual visual)
                    visual.UpgradeVisual();
            }
        }
    }

    void PopulateDefenderShop()
    {
        foreach (var d in defenders)
        {
            GameObject btn = Instantiate(defenderButtonPrefab, defenderShopParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = d.defenderName;
            Image img = btn.GetComponentInChildren<Image>();
            if (img && d.icon) img.sprite = d.icon;
            btn.GetComponent<Button>().onClick.AddListener(() => TryBuyDefender(d));
        }
    }

    void PopulateUpgradeShop()
    {
        foreach (var u in upgrades)
        {
            GameObject btn = Instantiate(upgradeButtonPrefab, upgradeShopParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = u.upgradeName;
            Image img = btn.GetComponentInChildren<Image>();
            if (img && u.icon) img.sprite = u.icon;
            btn.GetComponent<Button>().onClick.AddListener(() => TryBuyUpgrade(u));
        }
    }


}