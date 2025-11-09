using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Shop/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;      // e.g., "Thorn Reach"
    public string description;      // e.g., "Dryad spikes hit farther"
    public int unlockLevel = 1;
    public int cost = 100;          // XP cost

    [Header("Target & Stat")]
    public string targetType = "Dryad";     // "Tree", "Dryad", "Dragon", "Griffon"
    public string statType = "Range";       // "Range", "FireRate", "Health"
    public float statMultiplier = 1.2f;     // +20%

    public Sprite icon;
}