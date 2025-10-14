using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class Shop : MonoBehaviour
{
    [Header("Defender Data")]
    public List<DefenderData> defenders;

    [Header("Upgrade Data")]
    public List<UpgradeData> upgrades;

    [Header("UI References")]
    public Transform defenderShopParent; // grid parent for defender buttons
    public GameObject defenderButtonPrefab;
    public Transform upgradeShopParent;
    public GameObject upgradeButtonPrefab;

    private XPManager xpManager;
    private GameManager gameManager;
    private PlaceDefender placeDefender;

    PlaceDefender PlaceDefender;


    private void Start()
    {
        xpManager = FindObjectOfType<XPManager>();
        gameManager = FindObjectOfType<GameManager>();
        placeDefender = PlaceDefender.instance;

        PopulateDefenderShop();
        PopulateUpgradeShop();

       
    }
    /*public void BuyWeaponOne()
    {
        Debug.Log("WeaponOneBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender1);
    }*/

    /*public void BuyWeaponTwo()
    {
        Debug.Log("WeaponTwoBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender2);
    }*/

    /*public void BuyWeaponThree()
    {
        Debug.Log("WeaponThreeBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender3);
    }*/
    void TryBuyDefender(DefenderData defender)
    {
        int playerLevel = xpManager.GetLevel();
        bool unlocked = playerLevel >= defender.unlockLevel;

        if (unlocked && gameManager.points >= defender.cost)
        {
            gameManager.points -= defender.cost;
            placeDefender.SetDefenderToBuild(defender.defenderPrefab);
            Debug.Log($"{defender.defenderName} purchased!");
        }
        else
        {
            Debug.Log($"Locked until level {defender.unlockLevel} or insufficient points!");
        }

       
    }

    void TryBuyUpgrade(UpgradeData upgrade)
    {
        if (xpManager.GetLevel() >= upgrade.unlockLevel && gameManager.points >= upgrade.cost)
        {
            gameManager.points -= upgrade.cost;
            ApplyUpgrade(upgrade);
            Debug.Log($"{upgrade.upgradeName} applied!");
        }
        else
        {
            Debug.Log("Not enough points or not unlocked yet!");
        }

    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        // Example: apply to all turrets
        var turrets = FindObjectsOfType<Turret>();
        foreach (var turret in turrets)
        {
            turret.range *= upgrade.statMultiplier;
        }
    }

    void PopulateDefenderShop()
    {
        foreach (var defender in defenders)
        {
            GameObject buttonObj = Instantiate(defenderButtonPrefab, defenderShopParent);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = defender.defenderName;

            Image iconImage = buttonObj.GetComponentInChildren<Image>();
            if (iconImage != null && defender.icon != null)
                iconImage.sprite = defender.icon;

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(() => TryBuyDefender(defender));
        }
    }
    void PopulateUpgradeShop()
    {
        foreach (var upgrade in upgrades)
        {
            GameObject buttonObj = Instantiate(upgradeButtonPrefab, upgradeShopParent);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.upgradeName;

            Image iconImage = buttonObj.GetComponentInChildren<Image>();
            if (iconImage != null && upgrade.icon != null)
                iconImage.sprite = upgrade.icon;

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(() => TryBuyUpgrade(upgrade));
        }
    }


}
