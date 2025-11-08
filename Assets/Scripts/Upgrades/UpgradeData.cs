using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Shop/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int unlockLevel = 10;
    public int cost = 50;  // XP cost
    public float statMultiplier = 1.2f;
    public Sprite icon;
}