using UnityEngine;

[CreateAssetMenu(fileName = "New Defender", menuName = "Shop/Defender")]
public class DefenderData : ScriptableObject
{
    public string defenderName;
    public GameObject defenderPrefab;
    public int unlockLevel = 1;
    public int cost = 10;
    public Sprite icon;
}
