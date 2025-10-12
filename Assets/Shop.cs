using UnityEngine;

public class Shop : MonoBehaviour
{
    PlaceDefender PlaceDefender;

    private void Start()
    {
        PlaceDefender = PlaceDefender.instance;
    }
    public void BuyWeaponOne()
    {
        Debug.Log("WeaponOneBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender1);
    }

    public void BuyWeaponTwo()
    {
        Debug.Log("WeaponTwoBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender2);
    }

    public void BuyWeaponThree()
    {
        Debug.Log("WeaponThreeBought");
        PlaceDefender.SetDefenderToBuild(PlaceDefender.Defender3);
    }
}
