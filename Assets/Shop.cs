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
        if (xpManager.GetLevel() >= defender.unlockLevel && xpManager.totalXP >= defender.cost)
        {
            xpManager.totalXP -= defender.cost;
            placeDefender.SetDefenderToBuild(defender.defenderPrefab);
        }
    }

    void TryBuyUpgrade(UpgradeData upgrade)
    {
        if (xpManager.GetLevel() >= upgrade.unlockLevel && xpManager.totalXP >= upgrade.cost)
        {
            xpManager.totalXP -= upgrade.cost;
            ApplyUpgrade(upgrade);
        }
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        var turrets = FindObjectsOfType<Turret>();
        foreach (var t in turrets)
            t.range *= upgrade.statMultiplier;
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